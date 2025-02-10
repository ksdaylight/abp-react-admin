import type React from "react";
import { useState, useEffect } from "react";
import { Modal, Form, Input, Checkbox, Select, Tabs, TreeSelect } from "antd";
import { createApi, getListApi as getPermissionsApi, updateApi } from "@/api/permissions/definitions";
import { getListApi as getGroupsApi } from "@/api/permissions/groups";
import { toast } from "sonner";
import { useTranslation } from "react-i18next";
import { useTypesMap } from "./types";
import type { PermissionDefinitionDto } from "#/permissions/definitions";
import type { PermissionGroupDefinitionDto } from "#/permissions/groups";
import { listToTree } from "@/utils/tree";
import { useLocalizer } from "@/hooks/abp/use-localization";
import { localizationSerializer } from "@/utils/abp/localization-serializer";
import type { PropertyInfo } from "@/components/abp/properties/types";
import LocalizableInput from "@/components/abp/localizable-input/localizable-input";
import PropertyTable from "@/components/abp/properties/property-table";
import { omit } from "ramda";

const { TabPane } = Tabs;
const { Option } = Select;

interface PermissionDefinitionModalProps {
	visible: boolean;
	onClose: () => void;
	onChange: () => void;
	permission?: PermissionDefinitionDto;
}

interface PermissionTreeVo {
	children: PermissionTreeVo[];
	displayName: string;
	groupName: string;
	name: string;
}

type TabKeys = "basic" | "props";

//TODO 还缺一个旧的vue的stateCheckers管理

const PermissionDefinitionModal: React.FC<PermissionDefinitionModalProps> = ({
	visible,
	onClose,
	onChange,
	permission,
}) => {
	const { t: $t } = useTranslation();
	const [form] = Form.useForm();
	const { multiTenancySideOptions, providerOptions } = useTypesMap($t);
	const [availableGroups, setAvailableGroups] = useState<PermissionGroupDefinitionDto[]>([]);
	const [availablePermissions, setAvailablePermissions] = useState<PermissionTreeVo[]>([]);
	const [isEditModel, setIsEditModel] = useState(false);
	const [activeTab, setActiveTab] = useState<TabKeys>("basic");
	const { Lr } = useLocalizer();
	const { deserialize } = localizationSerializer();
	const [extraProperties, setExtraProperties] = useState<Record<string, any>>({});

	useEffect(() => {
		if (visible) {
			setIsEditModel(!!permission);
			setActiveTab("basic");
			form.resetFields();
			if (permission) {
				form.setFieldsValue(permission);
				fetchGroups(permission.groupName);
				fetchPermissions(permission.groupName);
			} else {
				fetchGroups();
			}
			setExtraProperties(permission?.extraProperties || {});
		}
	}, [visible, permission]);

	const fetchGroups = async (groupName?: string) => {
		const { items } = await getGroupsApi({ filter: groupName });
		const groups = items.map((group) => {
			const localizableGroup = deserialize(group.displayName);
			return {
				...group,
				displayName: Lr(localizableGroup.resourceName, localizableGroup.name),
			}; //TODO 是否要去掉 isStatic 的？ 需要把group的完成后
		});
		setAvailableGroups(groups);
		if (groupName) {
			form.setFieldsValue({ groupName });
			fetchPermissions(groupName);
		}
	};

	const fetchPermissions = async (groupName?: string) => {
		const { items } = await getPermissionsApi({ groupName }); //TODO react query 管理
		const permissions = items.map((permission) => {
			const localizablePermission = deserialize(permission.displayName);
			return {
				...permission,
				disabled: permission.name === form.getFieldValue("name"),
				displayName: Lr(localizablePermission.resourceName, localizablePermission.name),
			};
		});
		setAvailablePermissions(listToTree(permissions, { id: "name", pid: "parentName" }));
	};

	const handleOk = async () => {
		try {
			const values = await form.validateFields();
			const api = isEditModel ? updateApi(values.name, values) : createApi(values);
			await api;
			toast.success($t("AbpUi.SavedSuccessfully"));
			onChange();
			onClose();
		} catch (error) {
			console.error(error);
		}
	};

	const handleGroupChange = (groupName?: string) => {
		form.setFieldsValue({ groupName });
		fetchPermissions(groupName);
	};

	const handlePropChange = (prop: PropertyInfo) => {
		const newExtraProperties = { ...extraProperties, [prop.key]: prop.value };
		setExtraProperties(newExtraProperties); //PropertyTable 更新用
		form.setFieldsValue({ extraProperties: newExtraProperties }); //handleOk  更新用 TODO
	};

	const handlePropDelete = (prop: PropertyInfo) => {
		const newExtraProperties = omit([prop.key], extraProperties);
		setExtraProperties(newExtraProperties); //PropertyTable 更新用
		form.setFieldsValue({ newExtraProperties });//handleOk  更新用 TODO
	};

	return (
		<Modal
			open={visible}
			title={
				isEditModel
					? `${$t("AbpPermissionManagement.PermissionDefinitions")} - ${form.getFieldValue("name")}`
					: $t("AbpPermissionManagement.PermissionDefinitions:AddNew")
			}
			onCancel={onClose}
			onOk={handleOk}
			width="50%"
			// draggable
		>
			<Form form={form} labelCol={{ span: 6 }} wrapperCol={{ span: 18 }}>
				<Tabs activeKey={activeTab} onChange={(key) => setActiveTab(key as TabKeys)}>
					<TabPane tab={$t("AbpPermissionManagement.BasicInfo")} key="basic">
						<Form.Item
							label={$t("AbpPermissionManagement.DisplayName:IsEnabled")}
							name="isEnabled"
							valuePropName="checked"
						>
							<Checkbox disabled={form.getFieldValue("isStatic")}>
								{$t("AbpPermissionManagement.DisplayName:IsEnabled")}
							</Checkbox>
						</Form.Item>
						<Form.Item
							label={$t("AbpPermissionManagement.DisplayName:GroupName")}
							name="groupName"
							rules={[{ required: true }]}
						>
							<Select allowClear disabled={form.getFieldValue("isStatic")} onChange={handleGroupChange}>
								{availableGroups.map((group) => (
									<Option key={group.name} value={group.name}>
										{group.displayName}
									</Option>
								))}
							</Select>
						</Form.Item>
						{availablePermissions.length > 0 && (
							<Form.Item label={$t("AbpPermissionManagement.DisplayName:ParentName")} name="parentName">
								<TreeSelect
									allowClear
									disabled={form.getFieldValue("isStatic")}
									treeData={availablePermissions}
									fieldNames={{ label: "displayName", value: "name", children: "children" }}
								/>
							</Form.Item>
						)}
						<Form.Item label={$t("AbpPermissionManagement.DisplayName:Name")} name="name" rules={[{ required: true }]}>
							<Input disabled={form.getFieldValue("isStatic")} autoComplete="off" />
						</Form.Item>
						<Form.Item
							label={$t("AbpPermissionManagement.DisplayName:DisplayName")}
							name="displayName"
							rules={[{ required: true }]}
						>
							<LocalizableInput disabled={form.getFieldValue("isStatic")} />
						</Form.Item>
						<Form.Item label={$t("AbpPermissionManagement.DisplayName:MultiTenancySide")} name="multiTenancySide">
							<Select disabled={form.getFieldValue("isStatic")}>
								{multiTenancySideOptions.map((option) => (
									<Option key={option.value} value={option.value}>
										{option.label}
									</Option>
								))}
							</Select>
						</Form.Item>
						<Form.Item label={$t("AbpPermissionManagement.DisplayName:Providers")} name="providers">
							<Select mode="multiple" allowClear disabled={form.getFieldValue("isStatic")}>
								{providerOptions.map((option) => (
									<Option key={option.value} value={option.value}>
										{option.label}
									</Option>
								))}
							</Select>
						</Form.Item>
					</TabPane>
					<TabPane tab={$t("AbpPermissionManagement.Properties")} key="props">
						<PropertyTable
							data={extraProperties}
							disabled={form.getFieldValue("isStatic")}
							onChange={handlePropChange}
							onDelete={handlePropDelete}
						/>
					</TabPane>
				</Tabs>
			</Form>
		</Modal>
	);
};

export default PermissionDefinitionModal;
