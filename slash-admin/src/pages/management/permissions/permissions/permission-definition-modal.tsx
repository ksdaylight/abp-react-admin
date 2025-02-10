import { useEffect, useState } from "react";
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

const { TabPane } = Tabs;
const { Option } = Select;

interface PermissionDefinitionModalProps {
	visible: boolean;
	onClose: () => void;
	onChange: () => void;
	permission?: PermissionDefinitionDto;
	groupName?: string;
}

interface PermissionTreeVo {
	children: PermissionTreeVo[];
	displayName: string;
	groupName: string;
	name: string;
}

type TabKeys = "basic" | "props";

const defaultModel: PermissionDefinitionDto = {} as PermissionDefinitionDto;

const PermissionDefinitionModal: React.FC<PermissionDefinitionModalProps> = ({
	visible,
	onClose,
	onChange,
	permission,
	groupName,
}) => {
	const { t: $t } = useTranslation();
	const [form] = Form.useForm();
	const { multiTenancySideOptions, providerOptions } = useTypesMap($t);
	const [availableGroups, setAvailableGroups] = useState<PermissionGroupDefinitionDto[]>([]);
	const [availablePermissions, setAvailablePermissions] = useState<PermissionTreeVo[]>([]);
	const [activeTab, setActiveTab] = useState<TabKeys>("basic");
	const [formModel, setFormModel] = useState<PermissionDefinitionDto>({ ...defaultModel });
	const [loading, setLoading] = useState(false);
	const { Lr } = useLocalizer();
	const { deserialize } = localizationSerializer();

	useEffect(() => {
		if (visible) {
			setActiveTab("basic");
			form.resetFields();

			if (permission) {
				const initialModel = {
					...permission,
					extraProperties: permission.extraProperties || {},
				};
				setFormModel(initialModel);
				form.setFieldsValue(initialModel);
				fetchGroups(permission.groupName);
				fetchPermissions(permission.groupName);
			} else {
				setFormModel({ ...defaultModel });
				fetchGroups(groupName);
			}
		}
	}, [visible, permission, form.resetFields, form.setFieldsValue, groupName]);

	const fetchGroups = async (groupName?: string) => {
		const { items } = await getGroupsApi({ filter: groupName });
		const groups = items
			.filter((group) => !group.isStatic)
			.map((group) => {
				const localizableGroup = deserialize(group.displayName);
				return {
					...group,
					displayName: Lr(localizableGroup.resourceName, localizableGroup.name),
				};
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
		setAvailablePermissions(listToTree<PermissionTreeVo>(permissions, { id: "name", pid: "parentName" }));
	};

	const handleOk = async () => {
		try {
			setLoading(true);
			const values = await form.validateFields();
			const submitData = {
				...values,
				extraProperties: formModel.extraProperties,
			};

			const api = permission ? updateApi(values.name, submitData) : createApi(submitData);
			await api;
			toast.success($t("AbpUi.SavedSuccessfully"));
			onChange();
			onClose();
		} catch (error) {
			console.error(error);
		} finally {
			setLoading(false);
		}
	};

	const handleGroupChange = (groupName?: string) => {
		setFormModel((prev) => ({ ...prev, groupName: groupName || "" }));
		fetchPermissions(groupName);
	};

	const handlePropChange = (prop: PropertyInfo) => {
		setFormModel((prev) => ({
			...prev,
			extraProperties: {
				...prev.extraProperties,
				[prop.key]: prop.value,
			},
		}));
	};

	const handlePropDelete = (prop: PropertyInfo) => {
		setFormModel((prev) => {
			const newProps = { ...prev.extraProperties };
			delete newProps[prop.key];
			return {
				...prev,
				extraProperties: newProps,
			};
		});
	};

	return (
		<Modal
			open={visible}
			title={
				permission
					? `${$t("AbpPermissionManagement.PermissionDefinitions")} - ${formModel.name}`
					: $t("AbpPermissionManagement.PermissionDefinitions:AddNew")
			}
			onCancel={onClose}
			onOk={handleOk}
			confirmLoading={loading}
			okButtonProps={{ disabled: formModel.isStatic }}
			width="50%"
		>
			<Form form={form} labelCol={{ span: 6 }} wrapperCol={{ span: 18 }}>
				<Tabs activeKey={activeTab} onChange={(key) => setActiveTab(key as TabKeys)}>
					<TabPane tab={$t("AbpPermissionManagement.BasicInfo")} key="basic">
						<Form.Item
							label={$t("AbpPermissionManagement.DisplayName:IsEnabled")}
							name="isEnabled"
							valuePropName="checked"
						>
							<Checkbox disabled={formModel.isStatic}>{$t("AbpPermissionManagement.DisplayName:IsEnabled")}</Checkbox>
						</Form.Item>
						<Form.Item
							label={$t("AbpPermissionManagement.DisplayName:GroupName")}
							name="groupName"
							rules={[{ required: true }]}
						>
							<Select allowClear disabled={formModel.isStatic} onChange={handleGroupChange}>
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
									disabled={formModel.isStatic}
									treeData={availablePermissions}
									fieldNames={{ label: "displayName", value: "name", children: "children" }}
								/>
							</Form.Item>
						)}
						<Form.Item label={$t("AbpPermissionManagement.DisplayName:Name")} name="name" rules={[{ required: true }]}>
							<Input disabled={formModel.isStatic} autoComplete="off" />
						</Form.Item>
						<Form.Item
							label={$t("AbpPermissionManagement.DisplayName:DisplayName")}
							name="displayName"
							rules={[{ required: true }]}
						>
							<LocalizableInput disabled={formModel.isStatic} />
						</Form.Item>
						<Form.Item label={$t("AbpPermissionManagement.DisplayName:MultiTenancySide")} name="multiTenancySide">
							<Select disabled={formModel.isStatic}>
								{multiTenancySideOptions.map((option) => (
									<Option key={option.value} value={option.value}>
										{option.label}
									</Option>
								))}
							</Select>
						</Form.Item>
						<Form.Item label={$t("AbpPermissionManagement.DisplayName:Providers")} name="providers">
							<Select mode="multiple" allowClear disabled={formModel.isStatic}>
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
							data={formModel.extraProperties}
							disabled={formModel.isStatic}
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
