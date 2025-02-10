import { useEffect, useRef, useState } from "react";
import { Button, Modal, Space, Table, Tag, message } from "antd";
import { EditOutlined, DeleteOutlined, PlusOutlined } from "@ant-design/icons";
import { useTranslation } from "react-i18next";

import type { MultiTenancySides, PermissionDefinitionDto } from "#/permissions/definitions";
import { ActionType, ProTable, type ProColumns } from "@ant-design/pro-table";

import { localizationSerializer } from "@/utils/abp/localization-serializer";

import { deleteApi, getListApi as getPermissionsApi } from "@/api/permissions/definitions";
import { getListApi as getGroupsApi } from "@/api/permissions/groups";
import { GroupDefinitionsPermissions } from "@/constants/permissions";
import { hasAccessByCodes } from "@/utils/abp/access-checker";
import PermissionDefinitionModal from "./permission-definition-modal";
import { useLocalizer } from "@/hooks/abp/use-localization";
import { useTypesMap } from "./types";
import { listToTree } from "@/utils/tree";
import { ExtraPropertyDictionary } from "#/abp-core";
import { ColumnsType } from "antd/es/table";

interface PermissionVo {
	children: PermissionVo[];
	displayName: string;
	groupName: string;
	isEnabled: boolean;
	isStatic: boolean;
	multiTenancySide: MultiTenancySides;
	name: string;
	parentName?: string;
	providers: string[];
	stateCheckers: string;
	extraProperties: ExtraPropertyDictionary;
}

interface PermissionGroupVo {
	displayName: string;
	name: string;
	permissions: PermissionVo[];
}

//TODO use query 管理，lint
const PermissionDefinitionTable: React.FC = () => {
	const { t: $t, i18n } = useTranslation();
	const actionRef = useRef<ActionType>();
	const [modalVisible, setModalVisible] = useState(false);
	const [selectedPermission, setSelectedPermission] = useState<PermissionDefinitionDto>();

	const { Lr } = useLocalizer();
	const { deserialize } = localizationSerializer();
	const { multiTenancySidesMap, providersMap } = useTypesMap($t);

	const fetchData = async (params: any) => {
		const { filter } = params;
		const groupRes = await getGroupsApi({ filter });
		const permissionRes = await getPermissionsApi({ filter });

		const groups: PermissionGroupVo[] = groupRes.items.map((group) => {
			const localizableGroup = deserialize(group.displayName);
			const permissions = permissionRes.items
				.filter((permission) => permission.groupName === group.name)
				.map((permission) => {
					const localizablePermission = deserialize(permission.displayName);
					return {
						...permission,
						displayName: Lr(localizablePermission.resourceName, localizablePermission.name),
					};
				});
			return {
				...group,
				displayName: Lr(localizableGroup.resourceName, localizableGroup.name),
				permissions: listToTree<PermissionVo>(permissions, {
					id: "name",
					pid: "parentName",
				}),
			};
		});

		return {
			data: groups,
			success: true,
			total: groups.length,
		};
	};

	const handleDelete = async (permission: PermissionDefinitionDto) => {
		Modal.confirm({
			title: $t("AbpUi.AreYouSure"),
			content: $t("AbpUi.ItemWillBeDeletedMessageWithFormat", { 0: permission.name }),
			onOk: async () => {
				await deleteApi(permission.name);
				message.success($t("AbpUi.SuccessfullyDeleted"));
				actionRef.current?.reload();
			},
		});
	};

	const mainColumns: ProColumns<PermissionGroupVo>[] = [
		{
			title: $t("abp.sequence"),
			dataIndex: "index",
			valueType: "index",
			width: 50,
			render: (_, __, index) => index + 1,
		},
		{
			title: $t("AbpUi.Search"),
			dataIndex: "filter",
			valueType: "text",
			hideInTable: true, // hide in table
		},
		{
			title: $t("AbpPermissionManagement.DisplayName:Name"),
			dataIndex: "name",
			width: 150,
			hideInSearch: true,
		},
		{
			title: $t("AbpPermissionManagement.DisplayName:DisplayName"),
			dataIndex: "displayName",
			width: 150,
			hideInSearch: true,
		},
	];

	const subColumns: ColumnsType<PermissionVo> = [
		{
			title: $t("AbpPermissionManagement.DisplayName:Name"),
			dataIndex: "name",
			width: 200,
			ellipsis: true,
		},
		{
			title: $t("AbpPermissionManagement.DisplayName:DisplayName"),
			dataIndex: "displayName",
			width: 200,
		},
		{
			align: "center",
			minWidth: 100,
			// slots: { default: 'tenant' },
			title: $t("AbpPermissionManagement.DisplayName:MultiTenancySide"),
			render: (_, record) => (
				<Space>
					<Tag color="blue">{multiTenancySidesMap[record.multiTenancySide]}</Tag>
				</Space>
			),
		},
		{
			title: $t("AbpPermissionManagement.DisplayName:Providers"),
			width: 200,
			render: (_, record) => (
				<Space>
					{record.providers.map((provider) => (
						<Tag key={provider} color="blue">
							{providersMap[provider]}
						</Tag>
					))}
				</Space>
			),
		},
		{
			title: $t("AbpUi.Actions"),
			width: 180,
			fixed: "right",
			render: (_, record) => (
				<Space>
					{hasAccessByCodes([GroupDefinitionsPermissions.Update]) && (
						<Button
							type="link"
							icon={<EditOutlined />}
							onClick={() => {
								setSelectedPermission(record);
								setModalVisible(true);
							}}
						>
							{$t("AbpUi.Edit")}
						</Button>
					)}
					{!record.isStatic && hasAccessByCodes([GroupDefinitionsPermissions.Delete]) && (
						<Button type="link" danger icon={<DeleteOutlined />} onClick={() => handleDelete(record)}>
							{$t("AbpUi.Delete")}
						</Button>
					)}
				</Space>
			),
		},
	];

	const expandedRowRender = (group: PermissionGroupVo) => {
		return (
			<Table<PermissionVo>
				columns={subColumns}
				dataSource={group.permissions}
				pagination={false}
				rowKey={(record) => record.name}
				indentSize={30}
				expandable={{
					defaultExpandAllRows: false,
					childrenColumnName: "children", // 指定子项字段
				}}
			/>
		);
	};

	// 工具栏
	const toolBarRender = () => [
		hasAccessByCodes([GroupDefinitionsPermissions.Create]) && (
			<Button
				type="primary"
				icon={<PlusOutlined />}
				onClick={() => {
					setSelectedPermission(undefined);
					setModalVisible(true);
				}}
			>
				{$t("AbpPermissionManagement.PermissionDefinitions:AddNew")}
			</Button>
		),
	];
	useEffect(() => {
		actionRef.current?.reload();
		setModalVisible(false);
	}, [i18n.language]); //因为使用了从L Lr直接获取值，然后复制给displayName的方式，所以需要监听语言变化
	return (
		<>
			<ProTable<PermissionGroupVo>
				headerTitle={$t("AbpPermissionManagement.PermissionDefinitions")}
				actionRef={actionRef}
				columns={mainColumns}
				request={fetchData}
				rowKey={(record) => record.name}
				expandable={{ expandedRowRender }}
				toolBarRender={toolBarRender}
				pagination={{
					showSizeChanger: true,
				}}
				search={{
					labelWidth: "auto",
					span: 12, //search part width
					defaultCollapsed: true,
				}}
			/>
			<PermissionDefinitionModal
				visible={modalVisible}
				permission={selectedPermission}
				onClose={() => {
					setModalVisible(false);
					setSelectedPermission(undefined);
				}}
				onChange={() => {
					setModalVisible(false);
					actionRef.current?.reload();
				}}
			/>
		</>
	);
};

export default PermissionDefinitionTable;
