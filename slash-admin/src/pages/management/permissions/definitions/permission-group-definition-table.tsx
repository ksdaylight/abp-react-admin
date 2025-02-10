import { useCallback, useRef, useState } from "react";
import { Button, Dropdown, Menu, Modal } from "antd";
import { EditOutlined, DeleteOutlined, PlusOutlined, EllipsisOutlined } from "@ant-design/icons";
import { useTranslation } from "react-i18next";
import type { PermissionGroupDefinitionDto } from "#/permissions/groups";
import { type ActionType, ProTable, type ProColumns } from "@ant-design/pro-table";
import { hasAccessByCodes } from "@/utils/abp/access-checker";
import { localizationSerializer } from "@/utils/abp/localization-serializer";
import { deleteApi, getListApi } from "@/api/permissions/groups";
import { GroupDefinitionsPermissions, PermissionDefinitionsPermissions } from "@/constants/permissions";
import PermissionGroupDefinitionModal from "./permission-group-definition-modal";
import PermissionDefinitionModal from "../permissions/permission-definition-modal";
import { useLocalizer } from "@/hooks/abp/use-localization";
import { toast } from "sonner";
import { Iconify } from "@/components/icon";

const PermissionGroupDefinitionTable: React.FC = () => {
	const { t: $t } = useTranslation();
	const actionRef = useRef<ActionType>();

	const { deserialize } = localizationSerializer();

	// Modal states
	const [groupModalVisible, setGroupModalVisible] = useState(false);
	const [permissionModalVisible, setPermissionModalVisible] = useState(false);
	const [selectedGroup, setSelectedGroup] = useState<PermissionGroupDefinitionDto>();
	const [selectedGroupForPermission, setSelectedGroupForPermission] = useState<string>();
	const { Lr } = useLocalizer(
		undefined,
		useCallback(() => {
			actionRef.current?.reload();
			setPermissionModalVisible(false);
			setGroupModalVisible(false);
		}, []),
	);

	//TODO react query, lint
	const columns: ProColumns<PermissionGroupDefinitionDto>[] = [
		{
			title: $t("AbpUi.Search"),
			dataIndex: "filter",
			valueType: "text",
			hideInTable: true, // hide in table
		},
		{
			title: $t("AbpPermissionManagement.DisplayName:Name"),
			dataIndex: "name",
			width: "auto",
			hideInSearch: true,
		},
		{
			title: $t("AbpPermissionManagement.DisplayName:DisplayName"),
			dataIndex: "displayName",
			width: "auto",
			hideInSearch: true,
		},
		{
			title: $t("AbpUi.Actions"),
			width: 220,
			fixed: "right",
			hideInSearch: true,
			render: (_, record) => (
				<div className="flex flex-row">
					<div className={`${record.isStatic ? "w-full" : "basis-1/3"}`}>
						{hasAccessByCodes([GroupDefinitionsPermissions.Update]) && (
							<Button type="link" icon={<EditOutlined />} block onClick={() => handleUpdate(record)}>
								{$t("AbpUi.Edit")}
							</Button>
						)}
					</div>
					{!record.isStatic && (
						<>
							<div className="basis-1/3">
								{hasAccessByCodes([GroupDefinitionsPermissions.Delete]) && (
									<Button type="link" danger icon={<DeleteOutlined />} block onClick={() => handleDelete(record)}>
										{$t("AbpUi.Delete")}
									</Button>
								)}
							</div>
							<div className="basis-1/3">
								<Dropdown
									menu={{
										items: [
											hasAccessByCodes([PermissionDefinitionsPermissions.Create])
												? {
														key: "permissions",
														icon: <Iconify icon="icon-park-outline:permissions" />,
														label: $t("AbpPermissionManagement.PermissionDefinitions:AddNew"),
													}
												: null,
										].filter((item) => item !== null), // 过滤
										onClick: ({ key }) => handleMenuClick(key as string, record),
									}}
								>
									<Button type="link" icon={<EllipsisOutlined />} />
								</Dropdown>
							</div>
						</>
					)}
				</div>
			),
		},
	];

	const fetchData = async (params: any) => {
		const { filter } = params;
		const { items } = await getListApi({ filter });

		const groups = items.map((item) => {
			const localizableString = deserialize(item.displayName);
			return {
				...item,
				displayName: Lr(localizableString.resourceName, localizableString.name),
			};
		});

		return {
			data: groups,
			success: true,
			total: groups.length,
		};
	};

	const handleCreate = () => {
		setSelectedGroup(undefined);
		setGroupModalVisible(true);
	};

	const handleUpdate = (group: PermissionGroupDefinitionDto) => {
		setSelectedGroup(group);
		setGroupModalVisible(true);
	};

	const handleDelete = (group: PermissionGroupDefinitionDto) => {
		Modal.confirm({
			title: $t("AbpUi.AreYouSure"),
			content: $t("AbpUi.ItemWillBeDeletedMessageWithFormat", { 0: group.name }),
			onOk: async () => {
				await deleteApi(group.name);
				toast.success($t("AbpUi.DeletedSuccessfully"));
				actionRef.current?.reload();
			},
		});
	};

	const handleMenuClick = (key: string, group: PermissionGroupDefinitionDto) => {
		if (key === "permissions") {
			setSelectedGroupForPermission(group.name);
			setPermissionModalVisible(true);
		}
	};
	return (
		<>
			<ProTable<PermissionGroupDefinitionDto>
				headerTitle={$t("AbpPermissionManagement.GroupDefinitions")}
				actionRef={actionRef}
				columns={columns}
				request={fetchData}
				rowKey="name"
				pagination={{
					showSizeChanger: true,
				}}
				search={{
					labelWidth: "auto",
					span: 12,
					defaultCollapsed: true,
				}}
				toolBarRender={() => [
					hasAccessByCodes([GroupDefinitionsPermissions.Create]) && (
						<Button type="primary" icon={<PlusOutlined />} onClick={handleCreate}>
							{$t("AbpPermissionManagement.GroupDefinitions:AddNew")}
						</Button>
					),
				]}
			/>

			<PermissionGroupDefinitionModal
				visible={groupModalVisible}
				groupName={selectedGroup?.name}
				onClose={() => setGroupModalVisible(false)}
				onChange={() => {
					setGroupModalVisible(false);
					actionRef.current?.reload();
				}}
			/>

			<PermissionDefinitionModal
				visible={permissionModalVisible}
				onClose={() => {
					console.log("close");
					setPermissionModalVisible(false);
				}}
				onChange={() => {
					setPermissionModalVisible(false);
					actionRef.current?.reload();
				}}
				groupName={selectedGroupForPermission}
			/>
		</>
	);
};

export default PermissionGroupDefinitionTable;
