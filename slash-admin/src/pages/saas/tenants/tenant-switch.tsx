import type React from "react";
import { useState } from "react";
import { Button, Input, Modal, Form } from "antd";
import { useTranslation } from "react-i18next";
import { toast } from "sonner";
import { findTenantByNameApi } from "@/api/saas/multi-tenancy";
import useAbpStore from "@/store/abpCoreStore";

const TenantSwitch: React.FC = () => {
	const { t: $t } = useTranslation();
	const abpStore = useAbpStore();
	const currentTenant = abpStore.application?.currentTenant;

	const [visible, setVisible] = useState(false);
	const [form] = Form.useForm();
	const [submitting, setSubmitting] = useState(false);

	const handleOpen = () => {
		form.setFieldsValue({ name: currentTenant?.name });
		setVisible(true);
	};

	const handleSwitch = async () => {
		try {
			const values = await form.validateFields();
			setSubmitting(true);

			let tenantId: string | undefined = undefined;

			if (values.name) {
				const result = await findTenantByNameApi(values.name);

				if (!result.success) {
					toast.warning($t("AbpUiMultiTenancy.GivenTenantIsNotExist", { 0: values.name }));
					setSubmitting(false);
					return;
				}

				if (!result.isActive) {
					toast.warning($t("AbpUiMultiTenancy.GivenTenantIsNotAvailable", { 0: values.name }));
					setSubmitting(false);
					return;
				}
				tenantId = result.tenantId;
			}

			// Logic to actually switch tenant usually involves setting a header/cookie and reloading
			// Assuming setTenantId handles persistence or we trigger a reload
			// Common pattern in ABP React apps:
			// Cookies.set('__tenant', tenantId);
			// window.location.reload();
			// OR use store action if it handles API headers reactively.

			// For this example, we'll assume the store handles it:
			// abpStore.setTenantId(tenantId);
			// toast.success($t("AbpUi.SavedSuccessfully"));
			// setVisible(false);

			// However, usually a reload is safer to ensure all queries refetch with new header:
			// Simulating "emitting" change
			console.log("Switching to tenant:", tenantId);
			// Implementation depends on your auth/request setup

			setVisible(false);
		} catch (e) {
			console.error(e);
		} finally {
			setSubmitting(false);
		}
	};

	return (
		<div className="w-full">
			<Input.Search
				readOnly
				value={currentTenant?.name}
				placeholder={$t("AbpUiMultiTenancy.NotSelected")}
				enterButton={<Button onClick={handleOpen}>({$t("AbpUiMultiTenancy.Switch")})</Button>}
				onSearch={handleOpen}
			/>

			<Modal
				title={$t("AbpUiMultiTenancy.SwitchTenant")}
				open={visible}
				onCancel={() => setVisible(false)}
				onOk={handleSwitch}
				confirmLoading={submitting}
				destroyOnClose
			>
				<Form form={form} layout="vertical">
					<Form.Item name="name" label={$t("AbpUiMultiTenancy.Name")}>
						<Input placeholder={$t("AbpUiMultiTenancy.SwitchTenantHint")} allowClear />
					</Form.Item>
				</Form>
			</Modal>
		</div>
	);
};

export default TenantSwitch;
