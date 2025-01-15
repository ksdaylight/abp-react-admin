import React, { useState } from "react";
import { Drawer, Descriptions } from "antd";
import { formatToDateTime } from "@/utils/abp";
import { useTranslation } from "react-i18next";
import { SecurityLogDto } from "#/identity";
import { toast } from "sonner";
import { getApi } from "@/api/identity/security-logs";

interface Props {
	visible: boolean;
	onClose: () => void;
	securityLogId?: string;
}

const SecurityLogDrawer: React.FC<Props> = ({ visible, onClose, securityLogId }) => {
	const { t: $t } = useTranslation();
	const [formModel, setFormModel] = useState<SecurityLogDto | null>(null);
	const [loading, setLoading] = useState(false);

	// Fetch Security Log details
	const fetchSecurityLog = async (id: string) => {
		setLoading(true);
		try {
			const data = await getApi(id);
			setFormModel(data);
		} catch (error) {
			toast.error($t("AbpUi.FailedToLoadData"), {
				position: "top-center",
			});
		} finally {
			setLoading(false);
		}
	};

	// Handle Drawer open/close
	const handleOpenChange = async (isOpen: boolean) => {
		if (isOpen && securityLogId) {
			await fetchSecurityLog(securityLogId);
		} else {
			setFormModel(null);
		}
	};

	return (
		<Drawer
			title={$t("AbpAuditLogging.SecurityLog")}
			open={visible}
			onClose={onClose}
			afterOpenChange={handleOpenChange}
			width={800}
			loading={loading}
			destroyOnClose
		>
			<Descriptions bordered size="small" column={2} labelStyle={{ width: "110px" }} colon={false}>
				<Descriptions.Item label={$t("AbpAuditLogging.ApplicationName")}>
					{formModel?.applicationName}
				</Descriptions.Item>
				<Descriptions.Item label={$t("AbpAuditLogging.CreationTime")}>
					{formModel?.creationTime ? formatToDateTime(formModel.creationTime) : ""}
				</Descriptions.Item>
				<Descriptions.Item label={$t("AbpAuditLogging.Identity")}>{formModel?.identity}</Descriptions.Item>
				<Descriptions.Item label={$t("AbpAuditLogging.TenantName")}>{formModel?.tenantName}</Descriptions.Item>
				<Descriptions.Item label={$t("AbpAuditLogging.Actions")}>{formModel?.action}</Descriptions.Item>
				<Descriptions.Item label={$t("AbpAuditLogging.CorrelationId")}>{formModel?.correlationId}</Descriptions.Item>
				<Descriptions.Item label={$t("AbpAuditLogging.UserId")}>{formModel?.userId}</Descriptions.Item>
				<Descriptions.Item label={$t("AbpAuditLogging.UserName")}>{formModel?.userName}</Descriptions.Item>
				<Descriptions.Item label={$t("AbpAuditLogging.ClientId")}>{formModel?.clientId}</Descriptions.Item>
				<Descriptions.Item label={$t("AbpAuditLogging.ClientIpAddress")}>
					{formModel?.clientIpAddress}
				</Descriptions.Item>
				<Descriptions.Item label={$t("AbpAuditLogging.BrowserInfo")} span={2}>
					{formModel?.browserInfo}
				</Descriptions.Item>

				<Descriptions.Item label={$t("AbpAuditLogging.Additional")} span={2}>
					{formModel?.extraProperties ? JSON.stringify(formModel.extraProperties, null, 2) : ""}
				</Descriptions.Item>
			</Descriptions>
		</Drawer>
	);
};

export default SecurityLogDrawer;
