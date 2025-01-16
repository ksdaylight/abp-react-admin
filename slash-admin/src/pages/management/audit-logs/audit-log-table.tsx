import React, { useRef, useState } from "react";
import { Button, Tag, Modal, message, Space, Card } from "antd";
import { EditOutlined, DeleteOutlined } from "@ant-design/icons";
import { useTranslation } from "react-i18next";
import ProTable, { ProColumns, ActionType } from "@ant-design/pro-table";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { formatToDateTime } from "@/utils/abp";
import { AuditLogDto } from "#/auditing/audit-logs";
import { deleteApi, getPagedListApi } from "@/api/auditing/audit-logs";
import { httpMethodOptions, httpStatusCodeOptions } from "./mapping"; //TODO 
import AuditLogDrawer from "./audit-log-drawer";
import { hasAccessByCodes, withAccessChecker } from "@/utils/abp/access-checker";
import { AuditLogPermissions } from "@/constants/auditing/permissions";
import { useAuditLogs } from "@/hooks/abp/auditing/use-audit-logs";
import { antdOrderToAbpOrder } from "@/utils/abp/sort-order";

const AuditLogTable: React.FC = () => {
	const { t: $t } = useTranslation();
	const actionRef = useRef<ActionType>();
	const queryClient = useQueryClient();
	const [drawerVisible, setDrawerVisible] = useState(false);
	const [selectedLog, setSelectedLog] = useState<AuditLogDto | null>(null);
	const { getHttpMethodColor, getHttpStatusCodeColor } = useAuditLogs();
  
	const openDrawer = (log: AuditLogDto) => {
		setSelectedLog(log);
		setDrawerVisible(true);
	};

	const closeDrawer = () => {
		setDrawerVisible(false);
		setSelectedLog(null);
	};

	const { mutateAsync: deleteAuditLog } = useMutation({
		mutationFn: deleteApi,
		onSuccess: () => {
			message.success($t("AbpUi.SuccessfullyDeleted"));
			queryClient.invalidateQueries({ queryKey: ["auditLogs"] });
		},
	});

	const handleDelete = (log: AuditLogDto) => {
		Modal.confirm({
			title: $t("AbpUi.AreYouSure"),
			content: $t("AbpUi.ItemWillBeDeletedMessage"),
			onOk: async () => {
				await deleteAuditLog(log.id);
				actionRef.current?.reload();
			},
		});
	};

	const columns: ProColumns<AuditLogDto>[] = [
		{
			title: $t("AbpAuditLogging.RequestUrl"),
			dataIndex: "url",
			sorter: true,
			width: 500,
			render: (_, record) => (
				<>
					<Tag color={getHttpStatusCodeColor(record.httpStatusCode)} onClick={() => onFilter("httpStatusCode", record.httpStatusCode)}>
						{record.httpStatusCode}
					</Tag>
					<Tag color={getHttpMethodColor(record.httpMethod)} onClick={() => onFilter("httpMethod", record.httpMethod)}>
						{record.httpMethod}
					</Tag>
					<a onClick={() => onFilter("url", record.url)}>{record.url}</a>
				</>
			),
		},
		{
			title: $t("AbpAuditLogging.UserName"),
			dataIndex: "userName",
			sorter: true,
			width: 120,
		},
		{
			title: $t("AbpAuditLogging.ExecutionTime"),
			dataIndex: "executionTime",
      valueType: "dateRange",
			sorter: true,
			width: 150,
			render: (_, record) => (record.executionTime ? formatToDateTime(record.executionTime) : record.executionTime),
		},
		{
			title: $t("AbpAuditLogging.ExecutionDuration"),
			dataIndex: "executionDuration",
			sorter: true,
			width: 140,
		},
		{
			title: $t("AbpAuditLogging.ClientId"),
			dataIndex: "clientId",
			sorter: true,
			width: 150,
		},
		{
			title: $t("AbpAuditLogging.ClientIpAddress"),
			dataIndex: "clientIpAddress",
			sorter: true,
			width: 150,
			render: (_, record) => (
				<>
					{record.extraProperties?.Location && <Tag color="blue">{record.extraProperties.Location}</Tag>}
					<span>{record.clientIpAddress}</span>
				</>
			),
		},
		{
			title: $t("AbpAuditLogging.ApplicationName"),
			dataIndex: "applicationName",
			sorter: true,
      ellipsis: true,
			width: 160,
		},
		{
			title: $t("AbpAuditLogging.CorrelationId"),
			dataIndex: "correlationId",
			sorter: true,
			width: 160,
		},
		{
			title: $t("AbpAuditLogging.TenantName"),
			dataIndex: "tenantName",
			sorter: true,
			width: 100,
		},
		{
			title: $t("AbpAuditLogging.BrowserInfo"),
			dataIndex: "browserInfo",
			sorter: true,
      ellipsis: true,
			width: 300,
		},
		hasAccessByCodes([AuditLogPermissions.Default, AuditLogPermissions.Delete])
			? {
					title: $t("AbpUi.Actions"),
					key: "actions",
					align: "center",
					fixed: "right",
					width: 220,
					render: (_, record) => (
						<div style={{ display: "flex", gap: "8px" }}>
							{withAccessChecker(
								<Button type="link" icon={<EditOutlined />} onClick={() => openDrawer(record)}>
									{$t("AbpAuditLogging.ShowLogDialog")}
								</Button>,
								[AuditLogPermissions.Default],
							)}
							{withAccessChecker(
								<Button type="link" danger icon={<DeleteOutlined />} onClick={() => handleDelete(record)}>
									{$t("AbpUi.Delete")}
								</Button>,
								[AuditLogPermissions.Delete],
							)}
						</div>
					),
				}
			: {},
	];

	const onFilter = (field: string, value: any) => {
		// actionRef.current?.get({ [field]: value });
		actionRef.current?.reload;
	};



	return (
		<>
			<Space direction="vertical" size="large" className="w-full">
				<Card>
					<ProTable<AuditLogDto>
						headerTitle={$t("AbpAuditLogging.AuditLog")}
						actionRef={actionRef}
						rowKey="id"
						columns={columns}
						request={async (params, sorter) => {
							const { current, pageSize, ...filters } = params;
							const query = await queryClient.fetchQuery({
								queryKey: ["auditLogs", params, sorter],
								queryFn: () =>
									getPagedListApi({
										maxResultCount: pageSize,
										skipCount: ((current || 1) - 1) * (pageSize || 0),
										sorting: sorter
											? Object.keys(sorter)
													.map((key) => `${key} ${antdOrderToAbpOrder(sorter[key])}`)
													.join(", ")
											: undefined,
										...filters,
									}),
							});
							return {
								data: query.items,
								total: query.totalCount,
							};
						}}
						pagination={{
							showSizeChanger: true,
						}}
						scroll={{ x: "max-content" }}
					/>
				</Card>
			</Space>
			{selectedLog&&<AuditLogDrawer visible={drawerVisible} onClose={closeDrawer} auditLog={selectedLog} />}
		</>
	);
};

export default AuditLogTable;
