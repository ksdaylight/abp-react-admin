import { useEffect, useState } from "react";
import { Form, Input, Modal, Tabs } from "antd";
import { useTranslation } from "react-i18next";
import type { PermissionGroupDefinitionDto } from "#/permissions/groups";
import type { PropertyInfo } from "@/components/abp/properties/types";
import { createApi, getApi, updateApi } from "@/api/permissions/groups";
import LocalizableInput from "@/components/abp/localizable-input/localizable-input";
import PropertyTable from "@/components/abp/properties/property-table";
import { toast } from "sonner";

interface Props {
	visible: boolean;
	onClose: () => void;
	onChange: (data: PermissionGroupDefinitionDto) => void;
	groupName?: string;
}

type TabKeys = "basic" | "props";

const defaultModel: PermissionGroupDefinitionDto = {} as PermissionGroupDefinitionDto;

const PermissionGroupDefinitionModal: React.FC<Props> = ({ visible, onClose, onChange, groupName }) => {
	const { t: $t } = useTranslation();
	const [form] = Form.useForm();
	const [formModel, setFormModel] = useState<PermissionGroupDefinitionDto>({ ...defaultModel });
	const [isEditModel, setIsEditModel] = useState(false);
	const [activeTab, setActiveTab] = useState<TabKeys>("basic");
	const [loading, setLoading] = useState(false);

	useEffect(() => {
		if (visible) {
			setIsEditModel(false);
			setActiveTab("basic");
			setFormModel({ ...defaultModel });
			form.resetFields();

			if (groupName) {
				fetchGroup(groupName);
			}
		}
	}, [visible, groupName, form.resetFields]);

	const fetchGroup = async (name: string) => {
		try {
			setLoading(true);
			const dto = await getApi(name);
			setIsEditModel(true);
			setFormModel(dto);
			form.setFieldsValue(dto);
		} finally {
			setLoading(false);
		}
	};

	const handleOk = async () => {
		try {
			const values = await form.validateFields();
			setLoading(true);
			const submitData = {
				...values,
				extraProperties: formModel.extraProperties,
			};
			const api = isEditModel ? updateApi(values.name, submitData) : createApi(submitData);
			const res = await api;
			toast.success($t("AbpUi.SavedSuccessfully"));
			onChange(res);
			onClose();
		} catch (error) {
			// Form validation error
		} finally {
			setLoading(false);
		}
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
			title={
				isEditModel
					? `${$t("AbpPermissionManagement.GroupDefinitions")} - ${formModel.name}`
					: $t("AbpPermissionManagement.GroupDefinitions:AddNew")
			}
			open={visible}
			onCancel={onClose}
			onOk={handleOk}
			confirmLoading={loading}
			okButtonProps={{ disabled: formModel.isStatic }}
			width="50%"
		>
			<Form form={form} labelCol={{ span: 6 }} wrapperCol={{ span: 18 }}>
				<Tabs activeKey={activeTab} onChange={(key) => setActiveTab(key as TabKeys)}>
					<Tabs.TabPane key="basic" tab={$t("AbpPermissionManagement.BasicInfo")}>
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
					</Tabs.TabPane>
					<Tabs.TabPane key="props" tab={$t("AbpPermissionManagement.Properties")}>
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

export default PermissionGroupDefinitionModal;
