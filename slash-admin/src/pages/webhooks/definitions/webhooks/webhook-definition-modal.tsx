import React, { useEffect, useMemo, useState } from "react";
import { Modal, Form, Input, Select, Checkbox, TreeSelect, Tabs } from "antd";
import { useTranslation } from "react-i18next";
import { toast } from "sonner";
import { useMutation, useQuery } from "@tanstack/react-query";
import { createApi, getApi, updateApi } from "@/api/webhooks/webhook-definitions";
import { getListApi as getGroupDefinitionsApi } from "@/api/webhooks/webhook-group-definitions";
import { getListApi as getFeaturesApi } from "@/api/management/features/feature-definitions";
import { getListApi as getFeatureGroupsApi } from "@/api/management/features/feature-group-definitions";
import type { WebhookDefinitionDto } from "#/webhooks/definitions";
import type { WebhookGroupDefinitionDto } from "#/webhooks/groups";
import type { FeatureDefinitionDto, FeatureGroupDefinitionDto } from "#/management/features";
import type { PropertyInfo } from "@/components/abp/properties/types";
import LocalizableInput from "@/components/abp/localizable-input/localizable-input";
import PropertyTable from "@/components/abp/properties/property-table";

import { localizationSerializer } from "@/utils/abp/localization-serializer";
import { useLocalizer } from "@/hooks/abp/use-localization";
import { hasAccessByCodes } from "@/utils/abp/access-checker";
import { listToTree } from "@/utils/tree";

interface Props {
  visible: boolean;
  onClose: () => void;
  onChange: (data: WebhookDefinitionDto) => void;
  groupName?: string; // Pre-select group if creating from group menu
  definitionName?: string; // Edit mode
}

interface FeatureTreeData {
  title: string;
  value: string;
  key: string;
  children?: FeatureTreeData[];
  checkable?: boolean;
}

const defaultModel: WebhookDefinitionDto = {
  displayName: "",
  extraProperties: {},
  groupName: "",
  isEnabled: true,
  isStatic: false,
  name: "",
  requiredFeatures: [],
} as WebhookDefinitionDto;

const WebhookDefinitionModal: React.FC<Props> = ({
  visible,
  onClose,
  onChange,
  groupName,
  definitionName,
}) => {
  const { t: $t } = useTranslation();
  const [form] = Form.useForm();
  const { deserialize } = localizationSerializer();
  const { Lr } = useLocalizer();

  const [activeTab, setActiveTab] = useState("basic");
  const [formModel, setFormModel] = useState<WebhookDefinitionDto>({ ...defaultModel });
  const [isEditModel, setIsEditModel] = useState(false);
  const [loading, setLoading] = useState(false);

  // Dropdown Data
  const [webhookGroups, setWebhookGroups] = useState<WebhookGroupDefinitionDto[]>([]);
  const [features, setFeatures] = useState<FeatureDefinitionDto[]>([]);
  const [featureGroups, setFeatureGroups] = useState<FeatureGroupDefinitionDto[]>([]);

  // Initialize Data
  useEffect(() => {
    if (visible) {
      initData();
    }
  }, [visible]);

  const initData = async () => {
    try {
      setLoading(true);
      const [groupRes, featureGroupRes, featureRes] = await Promise.all([
        getGroupDefinitionsApi({ filter: groupName }), // Filter by group if passed, otherwise fetch all? Logic says filter by group if creating inside one
        hasAccessByCodes(["FeatureManagement.GroupDefinitions"]) ? getFeatureGroupsApi() : Promise.resolve({ items: [] }),
        hasAccessByCodes(["FeatureManagement.Definitions"]) ? getFeaturesApi() : Promise.resolve({ items: [] }),
      ]);

      // Format Groups
      const formattedGroups = groupRes.items.map((g) => {
        const d = deserialize(g.displayName);
        return { ...g, displayName: Lr(d.resourceName, d.name) };
      });
      setWebhookGroups(formattedGroups);

      // Format Features
      const formattedFeatures = featureRes.items.filter(f => {
          // Filter boolean features logic from Vue
          // Simplified: assume simple features for now or check validator name
          return true; 
      }).map(f => {
          const d = deserialize(f.displayName);
          return { ...f, displayName: Lr(d.resourceName, d.name) };
      });
      setFeatures(formattedFeatures);

      // Format Feature Groups
      const formattedFeatureGroups = featureGroupRes.items.map(g => {
          const d = deserialize(g.displayName);
          return { ...g, displayName: Lr(d.resourceName, d.name) };
      });
      setFeatureGroups(formattedFeatureGroups);

      // Handle Edit or Create Mode
      if (definitionName) {
        setIsEditModel(true);
        const dto = await getApi(definitionName);
        setFormModel(dto);
        form.setFieldsValue(dto);
      } else {
        setIsEditModel(false);
        const initial = { ...defaultModel };
        // If only one group available or passed via props, select it
        if (groupName) initial.groupName = groupName;
        else if (formattedGroups.length === 1) initial.groupName = formattedGroups[0].name;
        
        setFormModel(initial);
        form.setFieldsValue(initial);
      }
    } finally {
      setLoading(false);
    }
  };

  // Build Feature Tree
  const featureTreeData = useMemo(() => {
    const tree: FeatureTreeData[] = [];
    featureGroups.forEach((group) => {
      const groupFeatures = features.filter((f) => f.groupName === group.name);
      // Using listToTree utility or manual mapping
      // Assuming features are flat list with parentName
      const children = listToTree(groupFeatures, { id: "name", pid: "parentName" });

      tree.push({
        title: group.displayName,
        value: group.name,
        key: group.name,
        checkable: false, // Groups not selectable as features
        children: children as FeatureTreeData[],
      });
    });
    return tree;
  }, [features, featureGroups]);

  const { mutateAsync: createDef, isPending: isCreating } = useMutation({
    mutationFn: createApi,
    onSuccess: (res) => {
      toast.success($t("AbpUi.SavedSuccessfully"));
      onChange(res);
      onClose();
    },
  });

  const { mutateAsync: updateDef, isPending: isUpdating } = useMutation({
    mutationFn: (data: WebhookDefinitionDto) => updateApi(data.name, data),
    onSuccess: (res) => {
      toast.success($t("AbpUi.SavedSuccessfully"));
      onChange(res);
      onClose();
    },
  });

  const handleSubmit = async () => {
    try {
      const values = await form.validateFields();
      const submitData = {
        ...formModel,
        ...values,
        // Antd TreeSelect returns values, we need to ensure it maps to requiredFeatures string array
        // If treeCheckStrictly is true, value is { label, value }.
        // The Vue code maps it.
        requiredFeatures: values.requiredFeatures?.map((v: any) => v.value || v) || [],
      };

      if (isEditModel) {
        await updateDef(submitData);
      } else {
        await createDef(submitData);
      }
    } catch (error) {
      console.error(error);
    }
  };

  const handlePropChange = (prop: PropertyInfo) => {
    setFormModel((prev) => ({
      ...prev,
      extraProperties: { ...prev.extraProperties, [prop.key]: prop.value },
    }));
  };

  const handlePropDelete = (prop: PropertyInfo) => {
    setFormModel((prev) => {
      const next = { ...prev.extraProperties };
      delete next[prop.key];
      return { ...prev, extraProperties: next };
    });
  };

  return (
    <Modal
      title={isEditModel ? `${$t("WebhooksManagement.WebhookDefinitions")} - ${formModel.name}` : $t("WebhooksManagement.Webhooks:AddNew")}
      open={visible}
      onCancel={onClose}
      onOk={handleSubmit}
      confirmLoading={isCreating || isUpdating || loading}
      okButtonProps={{ disabled: formModel.isStatic }}
      width={800}
      destroyOnClose
    >
      <Form form={form} layout="vertical" labelCol={{ span: 6 }} wrapperCol={{ span: 18 }}>
        <Tabs activeKey={activeTab} onChange={setActiveTab}>
          <Tabs.TabPane key="basic" tab={$t("WebhooksManagement.BasicInfo")}>
            <Form.Item name="isEnabled" valuePropName="checked" label={$t("WebhooksManagement.DisplayName:IsEnabled")}>
              <Checkbox disabled={formModel.isStatic}>{$t("WebhooksManagement.DisplayName:IsEnabled")}</Checkbox>
            </Form.Item>

            <Form.Item name="groupName" label={$t("WebhooksManagement.DisplayName:GroupName")} rules={[{ required: true }]}>
              <Select
                disabled={formModel.isStatic}
                options={webhookGroups}
                fieldNames={{ label: "displayName", value: "name" }}
                allowClear
              />
            </Form.Item>

            <Form.Item name="name" label={$t("WebhooksManagement.DisplayName:Name")} rules={[{ required: true }]}>
              <Input disabled={formModel.isStatic || isEditModel} autoComplete="off" />
            </Form.Item>

            <Form.Item name="displayName" label={$t("WebhooksManagement.DisplayName:DisplayName")} rules={[{ required: true }]}>
              <LocalizableInput disabled={formModel.isStatic} />
            </Form.Item>

            <Form.Item name="description" label={$t("WebhooksManagement.DisplayName:Description")}>
              <LocalizableInput disabled={formModel.isStatic} />
            </Form.Item>

            <Form.Item name="requiredFeatures" label={$t("WebhooksManagement.DisplayName:RequiredFeatures")}>
              <TreeSelect
                treeData={featureTreeData}
                disabled={formModel.isStatic}
                allowClear
                treeCheckable
                treeCheckStrictly
                showCheckedStrategy={TreeSelect.SHOW_ALL}
                placeholder={$t("WebhooksManagement.DisplayName:RequiredFeatures")}
              />
            </Form.Item>
          </Tabs.TabPane>

          <Tabs.TabPane key="props" tab={$t("WebhooksManagement.Properties")}>
            <PropertyTable
              data={formModel.extraProperties}
              disabled={formModel.isStatic}
              onChange={handlePropChange}
              onDelete={handlePropDelete}
            />
          </Tabs.TabPane>
        </Tabs>
      </Form>
    </Modal>
  );
};

export default WebhookDefinitionModal;