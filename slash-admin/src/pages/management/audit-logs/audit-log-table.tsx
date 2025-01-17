import React, { useRef, useState } from "react";
import { Button, Tag, Modal, message, Space, Card, Checkbox, FormInstance, Tooltip } from "antd";
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
	const formRef = useRef<FormInstance>();
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
			colSize: 2,
			order: 2,
			width: 500,
			ellipsis: true,
			render: (_, record) => (
				<>
					<Tag
						color={getHttpStatusCodeColor(record.httpStatusCode)}
						style={{ cursor: "pointer" }}
						onClick={() => onFilter("httpStatusCode", record.httpStatusCode)} //TODO 这个点击可Filter,以及点击提示
					>
						<Tooltip title={$t("abp.manage.identity.filterHttpStatusCode")}>{record.httpStatusCode}</Tooltip>
					</Tag>
					<Tag
						color={getHttpMethodColor(record.httpMethod)}
						onClick={() => onFilter("httpMethod", record.httpMethod)}
						style={{ cursor: "pointer" }}
					>
						<Tooltip title={$t("abp.manage.identity.filterHttpMethod")}>{record.httpMethod}</Tooltip>
					</Tag>
					<Tooltip title={$t("abp.manage.identity.filterRequestUrl")}>
						<a onClick={() => onFilter("url", record.url)}>{record.url}</a>
					</Tooltip>
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
			order: 1,
			colSize: 2,
			width: 150,
			render: (_, record) => (record.executionTime ? formatToDateTime(record.executionTime) : record.executionTime),
		},
		{
			title: $t("AbpAuditLogging.HttpStatusCode"),
			dataIndex: "httpStatusCode",
			valueType: "select",
			fieldProps: {
				options: httpStatusCodeOptions,
			},
			hideInTable: true, // Only show in search form
		},
		{
			title: $t("AbpAuditLogging.HttpMethod"),
			dataIndex: "httpMethod",
			valueType: "select",
			fieldProps: {
				options: httpMethodOptions,
			},
			hideInTable: true, // Only show in search form
		},
		{
			title: $t("AbpAuditLogging.ExecutionDuration"),
			dataIndex: "executionDuration",
			sorter: true,
			hideInSearch: true,
			width: 140, //最长
		},
		{
			title: $t("AbpAuditLogging.MinExecutionDuration"),
			dataIndex: "minExecutionDuration",
			valueType: "digit",
			hideInTable: true,
		},
		{
			title: $t("AbpAuditLogging.MaxExecutionDuration"),
			dataIndex: "maxExecutionDuration",
			valueType: "digit",
			hideInTable: true,
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
			colSize: 2,
			width: 160,
		},
		{
			title: $t("AbpAuditLogging.TenantName"),
			dataIndex: "tenantName",
			sorter: true,
			width: 100,
			hideInSearch: true,
		},
		{
			title: $t("AbpAuditLogging.BrowserInfo"),
			dataIndex: "browserInfo",
			sorter: true,
			ellipsis: true,
			width: 300,
			hideInSearch: true,
		},
		{
			title: $t("AbpAuditLogging.HasException"),
			dataIndex: "hasException",
			valueType: "checkbox",
			hideInTable: true,
			renderFormItem: (_, { type, defaultRender, ...rest }) => {
				return (
					<Checkbox
						{...rest}
						onChange={(e) => {
							const value = e.target.checked; // 获取 Checkbox 的选中状态
							onFilter("hasException", value, false); 
						}}
					/>
				);
			},
		},
		hasAccessByCodes([AuditLogPermissions.Default, AuditLogPermissions.Delete])
			? {
					title: $t("AbpUi.Actions"),
					key: "actions",
					align: "center",
					fixed: "right",
					hideInSearch: true,
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

	const onFilter = (field: string, value: any, shouldSubmit: boolean = true) => {
		// 使用 formRef 更新查询条件
		if (formRef.current) {
			formRef.current.setFieldsValue({
				[field]: value,
			});
		}
		if (shouldSubmit) {
			// 触发表格刷新
			formRef.current?.submit();
		}
	};

	// const onFilter = (field: string, value: any) => {
	// 	// actionRef.current?.get({ [field]: value });
	// //  const { current, pageSize, ...filters } = actionRef.current?.getTableDataSource() || {};

	//   const reset = actionRef?.current?.setp;
	//   if(reset) reset()
	// };

	return (
		<>
			<Space direction="vertical" size="large" className="w-full">
				<Card>
					<ProTable<AuditLogDto>
						headerTitle={$t("AbpAuditLogging.AuditLog")}
						actionRef={actionRef}
						formRef={formRef} //search form
						rowKey="id"
						columns={columns}
						request={async (params, sorter) => {
							const { current, pageSize, executionTime, ...filters } = params;
							const [startTime, endTime] = executionTime || [];
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
										startTime: startTime || undefined,
										endTime: endTime || undefined,
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
						search={{
							labelWidth: "auto",
							span: 6, //search part , Sets 4 columns layout
							defaultCollapsed: true,
						}}
						scroll={{ x: "max-content" }}
					/>
				</Card>
			</Space>
			{selectedLog && <AuditLogDrawer visible={drawerVisible} onClose={closeDrawer} auditLog={selectedLog} />}
		</>
	);
};

export default AuditLogTable;
