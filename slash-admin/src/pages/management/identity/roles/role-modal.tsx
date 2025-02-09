import type React from "react";
import { useEffect } from "react";
import { Modal, Form, Input, Checkbox } from "antd";
import { useTranslation } from "react-i18next";
import type { IdentityRoleDto } from "#/identity";
import { useQuery, useMutation } from "@tanstack/react-query";
import { createApi, getApi, updateApi } from "@/api/identity/role";
import { toast } from "sonner";

interface Props {
	visible: boolean;
	onClose: () => void;
	onChange: (data: IdentityRoleDto) => void;
	role?: IdentityRoleDto;
}

const defaultModel: Partial<IdentityRoleDto> = {
	isDefault: false,
	isPublic: true,
	isStatic: false,
};

const RoleModal: React.FC<Props> = ({ visible, onClose, onChange, role }) => {
	const { t: $t } = useTranslation();
	const [form] = Form.useForm<IdentityRoleDto>();

	// Query for getting role details
	const { data: roleData, isLoading: isLoadingRole } = useQuery({
		queryKey: ["role", role?.id],
		queryFn: () => {
			if (!role?.id) {
				return Promise.reject(new Error("role id is undefined"));
			}
			return getApi(role.id);
		},
		enabled: visible && !!role?.id,
	});

	// Mutations for create/update
	const { mutateAsync: createRole, isPending: isCreating } = useMutation({
		mutationFn: createApi,
		onSuccess: (data) => {
			onChange(data);
			toast.success($t("AbpUi.Success"));
			onClose();
		},
	});

	const { mutateAsync: updateRole, isPending: isUpdating } = useMutation({
		mutationFn: ({ id, data }: { id: string; data: IdentityRoleDto }) => updateApi(id, data),
		onSuccess: (data) => {
			onChange(data);
			toast.success($t("AbpUi.Success"));
			onClose();
		},
	});

	useEffect(() => {
		if (visible) {
			form.setFieldsValue(role?.id ? (roleData ?? role) : defaultModel);
		} else {
			form.resetFields();
		}
	}, [visible, roleData, role, form.setFieldsValue, form.resetFields]);

	const handleOk = async () => {
		try {
			const values = await form.validateFields();
			if (role?.id) {
				await updateRole({ id: role.id, data: values });
			} else {
				await createRole(values);
			}
		} catch (error) {
			// Form validation error, no need to handle
		}
	};

	return (
		<Modal
			title={role?.id ? $t("AbpIdentity.RoleSubject", { 0: role.name }) : $t("AbpIdentity.NewRole")}
			open={visible}
			onCancel={onClose}
			onOk={handleOk}
			confirmLoading={isCreating || isUpdating || isLoadingRole}
		>
			<Form form={form} labelCol={{ span: 6 }} wrapperCol={{ span: 18 }}>
				<Form.Item label={$t("AbpIdentity.DisplayName:IsDefault")} name="isDefault" valuePropName="checked">
					<Checkbox>{$t("AbpIdentity.DisplayName:IsDefault")}</Checkbox>
				</Form.Item>
				<Form.Item label={$t("AbpIdentity.DisplayName:IsPublic")} name="isPublic" valuePropName="checked">
					<Checkbox>{$t("AbpIdentity.DisplayName:IsPublic")}</Checkbox>
				</Form.Item>
				<Form.Item label={$t("AbpIdentity.DisplayName:RoleName")} name="name" rules={[{ required: true }]}>
					<Input />
				</Form.Item>
			</Form>
		</Modal>
	);
};

export default RoleModal;
