import React, { useRef, useState } from "react";
import { Button, Tag, Space, Modal } from "antd";
import { EditOutlined, DeleteOutlined, PlusOutlined, CheckOutlined, CloseOutlined } from "@ant-design/icons";
import { useTranslation } from "react-i18next";
import { toast } from "sonner";
import { ProTable, type ProColumns, type ActionType } from "@ant-design/pro-table";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { deleteApi, getListApi as getDefinitionsApi } from "@/api/webhooks/webhook-definitions";
import { getListApi as getGroupsApi } from "@/api/webhooks/webhook-group-definitions";
import type { WebhookDefinitionDto } from "#/webhooks/definitions";
import { WebhookDefinitionsPermissions } from "@/constants/webhooks/permissions"; 
import { hasAccessByCodes, withAccessChecker } from "@/utils/abp/access-checker";
import { localizationSerializer } from "@/utils/abp/localization-serializer";
import { useLocalizer } from "@/hooks/abp/use-localization";
import { antdOrderToAbpOrder } from "@/utils/abp/sort-order";

import WebhookDefinitionModal from "./webhook-definition-modal";

const WebhookDefinitionTable: React.FC = () => {
  const { t: $t } = useTranslation();
  const actionRef = useRef<ActionType>();
  const queryClient = useQueryClient();
  const { deserialize } = localizationSerializer();
  const { Lr } = useLocalizer();

  const [modalVisible, setModalVisible] = useState(false);
  const [selectedDefinitionName, setSelectedDefinitionName] = useState<string | undefined>();

  const { mutateAsync: deleteDef } = useMutation({
    mutationFn: deleteApi,
    onSuccess: () => {
      toast.success($t("AbpUi.DeletedSuccessfully"));
      actionRef.current?.reload();
    },
  });

  const handleCreate = () => {
    setSelectedDefinitionName(undefined);
    setModalVisible(true);
  };

  const handleUpdate = (row: WebhookDefinitionDto) => {
    setSelectedDefinitionName(row.name);
    setModalVisible(true);
  };

  const handleDelete = (row: WebhookDefinitionDto) => {
    Modal.confirm({
      title: $t("AbpUi.AreYouSure"),
      content: $t("AbpUi.ItemWillBeDeletedMessageWithFormat", { 0: row.name }),
      onOk: () => deleteDef(row.name),
    });
  };

  // Grouping logic is typically handled by expanding rows in ProTable or using a TreeData structure.
  // The Vue implementation nests definitions under groups manually.
  // Here we can use `expandable` or simple flat list with grouping column, 
  // OR we can mimic the Vue logic by fetching groups and definitions and merging them into a tree structure for ProTable.

  const columns: ProColumns<any>[] = [
    {
      title: $t("AbpUi.Search"),
      dataIndex: "filter",
      valueType: "text",
      hideInTable: true,
    },
    // Definition Columns
    {
      title: $t("WebhooksManagement.DisplayName:Name"),
      dataIndex: "name",
      width: 250,
      fixed: "left",
    },
    {
      title: $t("WebhooksManagement.DisplayName:DisplayName"),
      dataIndex: "displayName",
      width: 200,
    },
    {
      title: $t("WebhooksManagement.DisplayName:IsEnabled"),
      dataIndex: "isEnabled",
      width: 100,
      align: "center",
      render: (_, val: boolean) => val ? <CheckOutlined className="text-green-500" /> : <CloseOutlined className="text-red-500" />,
    },
    {
      title: $t("WebhooksManagement.DisplayName:Description"),
      dataIndex: "description",
      ellipsis: true,
    },
    {
      title: $t("WebhooksManagement.DisplayName:RequiredFeatures"),
      dataIndex: "requiredFeatures",
      render: (_, row: WebhookDefinitionDto) => (
        <Space wrap>
          {row.requiredFeatures?.map(f => <Tag key={f} color="blue">{f}</Tag>)}
        </Space>
      )
    },
    {
      title: $t("AbpUi.Actions"),
      valueType: "option",
      fixed: "right",
      width: 150,
      render: (_, record: WebhookDefinitionDto) => {
        // Only render actions for actual definitions (leaf nodes)
        // if (record.children) return null; TODO ???

        return (
          <Space>
            {withAccessChecker(
              <Button type="link" icon={<EditOutlined />} onClick={() => handleUpdate(record)}>
                {$t("AbpUi.Edit")}
              </Button>,
              [WebhookDefinitionsPermissions.Update]
            )}
            {!record.isStatic && withAccessChecker(
              <Button type="link" danger icon={<DeleteOutlined />} onClick={() => handleDelete(record)}>
                {$t("AbpUi.Delete")}
              </Button>,
              [WebhookDefinitionsPermissions.Delete]
            )}
          </Space>
        );
      },
    },
  ];

  return (
    <>
      <ProTable
        headerTitle={$t("WebhooksManagement.WebhookDefinitions")}
        actionRef={actionRef}
        rowKey="name" // Use unique key
        columns={columns}
        search={{ labelWidth: "auto" }}
        expandable={{
            defaultExpandAllRows: true, 
            // In ProTable, tree data is auto-detected if 'children' property exists
        }}
        toolBarRender={() => [
          withAccessChecker(
            <Button key="create" type="primary" icon={<PlusOutlined />} onClick={handleCreate}>
              {$t("WebhooksManagement.Webhooks:AddNew")}
            </Button>,
            [WebhookDefinitionsPermissions.Create]
          ),
        ]}
        request={async (params, sorter) => {
          const { current, pageSize, ...filters } = params;
          // 1. Fetch Groups
          const groupRes = await getGroupsApi({ filter: filters.filter });
          // 2. Fetch Definitions
          const defRes = await getDefinitionsApi({ filter: filters.filter });

          // 3. Merge into Tree Structure
          const treeData = groupRes.items.map(group => {
              const groupLocal = deserialize(group.displayName);
              const children = defRes.items
                .filter(d => d.groupName === group.name)
                .map(d => {
                    const dName = deserialize(d.displayName);
                    const dDesc = deserialize(d.description);
                    return {
                        ...d,
                        key: d.name, // unique key
                        displayName: Lr(dName.resourceName, dName.name),
                        description: dDesc ? Lr(dDesc.resourceName, dDesc.name) : "",
                    };
                });
                
              return {
                  ...group,
                  key: `GROUP_${group.name}`,
                  displayName: Lr(groupLocal.resourceName, groupLocal.name),
                  description: "", // Groups don't have desc in this view usually
                  isEnabled: null, // Groups don't have status
                  requiredFeatures: [],
                  children: children.length > 0 ? children : undefined, // Only show if children exist? Or show empty group?
                  // If we want to hide empty groups:
                  // if(children.length === 0) return null;
              };
          }).filter(Boolean); // Filter nulls if hiding empty

          return {
            data: treeData,
            success: true,
            total: treeData.length, // Total groups? or total items? Pagination on tree is tricky.
          };
        }}
        pagination={false} // Tree view usually disables pagination or paginates root nodes
      />

      <WebhookDefinitionModal
        visible={modalVisible}
        definitionName={selectedDefinitionName}
        onClose={() => setModalVisible(false)}
        onChange={() => actionRef.current?.reload()}
      />
    </>
  );
};

export default WebhookDefinitionTable;