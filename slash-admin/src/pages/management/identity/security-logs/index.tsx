import { useRef, useState } from "react";
import { Space, Button, Tag, Popconfirm, Card } from "antd";
import ProTable, { ProColumns, ActionType } from "@ant-design/pro-table";
import { EditOutlined, DeleteOutlined } from "@ant-design/icons";
import { useTranslation } from "react-i18next";
import { formatToDateTime } from "@/utils/abp";
import { SecurityLogDto } from "#/identity";
import { useSecurityLogsApi } from "@/api/identity/use-security-logs-api";
import { antdOrderToAbpOrder } from "@/utils/abp/sort-order";
import { SecurityLogPermissions } from "@/constants/identity/permissions";
import { hasAccessByCodes, withAccessChecker } from "@/utils/abp/access-checker";
import { toast } from "sonner";
import SecurityLogDrawer from "./security-log-drawer";

const SecurityLogs = () => {
	const { t: $t } = useTranslation();
	const actionRef = useRef<ActionType>();
	const { cancel, deleteApi, getPagedListApi } = useSecurityLogsApi();
	//drawer
	const [drawerVisible, setDrawerVisible] = useState(false);
	const [selectedLogId, setSelectedLogId] = useState<string | undefined>();

	const openDrawer = (id: string) => {
		setSelectedLogId(id);
		setDrawerVisible(true);
	};

	const closeDrawer = () => {
		setDrawerVisible(false);
		setSelectedLogId(undefined);
	};

	const handleDelete = async (id: string) => {
		await deleteApi(id);
		toast.success($t("AbpUi.SuccessfullyDeleted"));
		actionRef.current?.reload();
	};

	const columns: ProColumns<SecurityLogDto>[] = [
		{
			title: $t("AbpAuditLogging.CreationTime"),
			dataIndex: "creationTime",
			valueType: "dateRange",
			sorter: true,
			width: 180,
			render: (_, record) => {
				// formatter
				return record.creationTime ? formatToDateTime(record.creationTime) : record.creationTime;
			},
		},
		{
			title: $t("AbpAuditLogging.Identity"),
			dataIndex: "identity",
			sorter: true,
			width: 180,
		},
		{
			title: $t("AbpAuditLogging.UserName"),
			dataIndex: "userName",
			sorter: true,
			width: 150,
		},
		{
			title: $t("AbpAuditLogging.ClientId"),
			dataIndex: "clientId",
			sorter: true,
			width: 200,
		},
		{
			title: $t("AbpAuditLogging.ClientIpAddress"),
			dataIndex: "clientIpAddress",
			sorter: true,
			width: 200,
			hideInSearch: true,
			render: (_, record) => (
				<>
					{/* 展示地址 */}
					{record.extraProperties?.Location && <Tag color="blue">{record.extraProperties.Location}</Tag>}
					<span>{record.clientIpAddress}</span>
				</>
			),
		},
		{
			title: $t("AbpAuditLogging.ApplicationName"),
			dataIndex: "applicationName",
			sorter: true,
			width: 200,
			ellipsis: true,
		},
		{
			title: $t("AbpAuditLogging.TenantName"),
			dataIndex: "tenantName",
			hideInSearch: true,
			sorter: true,
			width: 180,
		},
		{
			title: $t("AbpAuditLogging.Actions"),
			dataIndex: "action",
			sorter: true,
			width: 180,
		},
		{
			title: $t("AbpAuditLogging.CorrelationId"),
			dataIndex: "correlationId",
			sorter: true,
			width: 200,
		},
		{
			title: $t("AbpAuditLogging.BrowserInfo"),
			dataIndex: "browserInfo",
			width: 200,
			sorter: true,
			hideInSearch: true,
			ellipsis: true,
		},
		hasAccessByCodes([SecurityLogPermissions.Default, SecurityLogPermissions.Delete])
			? {
					title: $t("AbpUi.Actions"),
					key: "actions",
					align: "center",
					fixed: "right",
					hideInSearch: true,
					width: 150,
					render: (_, record) => (
						<div style={{ display: "flex", gap: "8px" }}>
							{withAccessChecker(
								<Button type="link" icon={<EditOutlined />} onClick={() => openDrawer(record.id)}>
									{$t("AbpUi.Edit")}
								</Button>,
								[SecurityLogPermissions.Default],
							)}
							{withAccessChecker(
								<Popconfirm
									title={$t("AbpUi.AreYouSure")}
									description={$t("AbpUi.ItemWillBeDeletedMessage")}
									onConfirm={() => handleDelete(record.id)}
									onCancel={() => {
										cancel("User closed cancel delete modal.");
									}}
									okText={$t("AbpUi.Yes")}
									cancelText={$t("AbpUi.No")}
								>
									<Button type="link" danger icon={<DeleteOutlined />}>
										{$t("AbpUi.Delete")}
									</Button>
								</Popconfirm>,
								[SecurityLogPermissions.Delete],
							)}
						</div>
					),
				}
			: {},
	];

	return (
		<>
			<Space direction="vertical" size="large" className="w-full">
				<Card>
					<ProTable<SecurityLogDto>
						headerTitle={$t("AbpAuditLogging.SecurityLog")}
						actionRef={actionRef}
						rowKey="id"
						search={{
							labelWidth: "auto",
							defaultCollapsed: true,
						}}
						columns={columns}
						request={async (params, sorter) => {
							const { creationTime, current, pageSize, ...rest } = params;
							const [startTime, endTime] = creationTime || [];
							const response = await getPagedListApi({
								maxResultCount: pageSize,
								skipCount: ((current || 1) - 1) * (pageSize || 0),
								sorting: sorter
									? Object.keys(sorter)
											.map((key) => `${key} ${antdOrderToAbpOrder(sorter[key])}`)
											.join(", ")
									: undefined,
								startTime: startTime || undefined, // 转换为 startTime 参数
								endTime: endTime || undefined, // 转换为 endTime 参数
								...rest,
							});
							return {
								data: response.items,
								total: response.totalCount,
							};
						}}
						pagination={{
							showSizeChanger: true,
						}}
						scroll={{ x: "max-content" }}
					/>
				</Card>
			</Space>
			<SecurityLogDrawer visible={drawerVisible} onClose={closeDrawer} securityLogId={selectedLogId} />
		</>
	);
};

export default SecurityLogs;
