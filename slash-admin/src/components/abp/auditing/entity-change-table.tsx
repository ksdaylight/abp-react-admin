import { formatToDateTime } from "@/utils/abp";
import { useTranslation } from "react-i18next";
import type { EntityChangeDto, PropertyChange, ChangeType } from "#/auditing/entity-changes";
import { Tag, Table } from "antd";
import type { ColumnsType } from "antd/es/table";
import { useState, useMemo } from "react";
import { useAuditLogs } from "@/hooks/abp/auditing/use-audit-logs";

interface EntityChangeTableProps {
	data: EntityChangeDto[];
	showUserName?: boolean;
}

export const EntityChangeTable: React.FC<EntityChangeTableProps> = ({ data, showUserName = false }) => {
	const [pageSize, setPageSize] = useState(10);
	const [current, setCurrent] = useState(1);
	const { t } = useTranslation();
	const { getChangeTypeColor, getChangeTypeValue } = useAuditLogs();

	const columns: ColumnsType<EntityChangeDto> = [
		showUserName && {
			title: t("AbpAuditLogging.UserName"),
			dataIndex: "userName",
			width: 100,
		},
		{
			title: t("AbpAuditLogging.ChangeType"),
			dataIndex: "changeType",
			width: 100,
			align: "center",
			sorter: true,
			render: (type: ChangeType) => <Tag color={getChangeTypeColor(type)}>{getChangeTypeValue(type)}</Tag>,
		},
		{
			title: t("AbpAuditLogging.StartTime"),
			dataIndex: "changeTime",
			width: 200,
			sorter: true,
			render: (value: Date) => value && formatToDateTime(value),
		},
		{
			title: t("AbpAuditLogging.EntityTypeFullName"),
			dataIndex: "entityTypeFullName",
			sorter: true,
		},
		{
			title: t("AbpAuditLogging.EntityId"),
			dataIndex: "entityId",
			width: 280,
			sorter: true,
		},
		{
			title: t("AbpAuditLogging.TenantId"),
			dataIndex: "entityTenantId",
			width: 280,
			sorter: true,
		},
	].filter(Boolean) as ColumnsType<EntityChangeDto>;

	const propertyColumns: ColumnsType<PropertyChange> = [
		{
			title: t("AbpAuditLogging.PropertyName"),
			dataIndex: "propertyName",
			width: 120,
			sorter: true,
		},
		{
			title: t("AbpAuditLogging.NewValue"),
			dataIndex: "newValue",
			width: 200,
			sorter: true,
			className: "font-medium text-green-600",
		},
		{
			title: t("AbpAuditLogging.OriginalValue"),
			dataIndex: "originalValue",
			width: 200,
			sorter: true,
			className: "font-medium text-red-600",
		},
		{
			title: t("AbpAuditLogging.PropertyTypeFullName"),
			dataIndex: "propertyTypeFullName",
			width: 220,
			sorter: true,
		},
	];

	const paginatedData = useMemo(() => {
		const startIndex = (current - 1) * pageSize;
		return data.slice(startIndex, startIndex + pageSize);
	}, [data, current, pageSize]);

	return (
		<Table
			columns={columns}
			dataSource={paginatedData}
			rowKey="id"
			pagination={{
				current,
				pageSize,
				total: data.length,
				onChange: (page, size) => {
					setCurrent(page);
					setPageSize(size);
				},
				showSizeChanger: true,
				pageSizeOptions: ["10", "25", "50", "100"],
			}}
			expandable={{
				expandedRowRender: (record) => (
					<Table
						columns={propertyColumns}
						dataSource={record.propertyChanges}
						pagination={false}
						rowKey="propertyName"
						bordered
					/>
				),
			}}
		/>
	);
};
