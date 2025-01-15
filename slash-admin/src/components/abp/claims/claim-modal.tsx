import React, { useEffect, useState } from "react";
import { Modal, Form, Input, Select } from "antd";
import { useTranslation } from "react-i18next";
import { useClaimTypesApi } from "@/api/identity/use-claim-types-api";
import { IdentityClaimCreateDto, IdentityClaimDto, IdentityClaimUpdateDto } from "#/identity/claims";
import { IdentityClaimTypeDto } from "#/identity";

interface ClaimEditModalProps {
	visible: boolean;
	claim?: IdentityClaimDto;
	onClose: () => void;
	onChange: (data: IdentityClaimDto) => void;
	createApi: (input: IdentityClaimCreateDto) => Promise<void>;
	updateApi: (input: IdentityClaimUpdateDto) => Promise<void>;
}

const ClaimModal: React.FC<ClaimEditModalProps> = ({ visible, claim, onClose, onChange, createApi, updateApi }) => {
	const { t: $t } = useTranslation();
	const [form] = Form.useForm();
	const { cancel, getAssignableClaimsApi } = useClaimTypesApi();
	const [loading, setLoading] = useState(false);
	const [assignableClaims, setAssignableClaims] = useState<IdentityClaimTypeDto[]>([]);

	useEffect(() => {
		if (visible) {
			form.resetFields();
			if (claim) {
				form.setFieldsValue({
					...claim,
					newClaimValue: claim.claimValue,
				});
			}
			if (!claim?.id) {
				initAssignableClaims();
			}
		}
	}, [visible, claim]);

	const initAssignableClaims = async () => {
		const { items } = await getAssignableClaimsApi();
		setAssignableClaims(items);
	};

	const handleOk = async () => {
		try {
			setLoading(true);
			const values = await form.validateFields();
			const api = claim?.id
				? updateApi({
						claimType: claim.claimType,
						claimValue: claim.claimValue,
						newClaimValue: values.claimValue,
					})
				: createApi({
						claimType: values.claimType,
						claimValue: values.claimValue,
					});
			await api;// TODO react query integration
			onChange(values as IdentityClaimDto); // 更新列表数据，供父组件使用
			onClose();
		} finally {
			setLoading(false);
		}
	};
  
	return (
		<Modal
			open={visible}
			title={$t("AbpIdentity.ManageClaim")}
			onCancel={onClose}
			onOk={handleOk}
			onClose={() => {
				cancel("Claim modal has closed!");
			}}
			confirmLoading={loading}
		>
			<Form form={form} layout="vertical">
				<Form.Item name="claimType" label={$t("AbpIdentity.DisplayName:ClaimType")} rules={[{ required: true }]}>
					<Select
						disabled={!!claim?.id}
						options={assignableClaims.map((item) => ({ label: item.name, value: item.name }))}
					/>
				</Form.Item>
				<Form.Item name="claimValue" label={$t("AbpIdentity.DisplayName:ClaimValue")} rules={[{ required: true }]}>
					<Input.TextArea />
				</Form.Item>
			</Form>
		</Modal>
	);
};

export default ClaimModal;
