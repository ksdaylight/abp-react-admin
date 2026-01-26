import type React from "react";
import { useMemo } from "react";
import { Checkbox, TreeSelect, Spin } from "antd";
import { useTranslation } from "react-i18next";
import { useQuery } from "@tanstack/react-query";
import { getListApi as getPermissionsApi } from "@/api/management/permissions/definitions";
import { getListApi as getGroupsApi } from "@/api/management/permissions/groups";
import { listToTree } from "@/utils/tree";
import { localizationSerializer } from "@/utils/abp/localization-serializer";
import { useLocalizer } from "@/hooks/abp/use-localization";

interface ValueType {
	permissions: string[];
	requiresAll: boolean;
}

interface PermissionStateCheckProps {
	value?: ValueType;
	onChange?: (value: ValueType) => void;
}

const PermissionStateCheck: React.FC<PermissionStateCheckProps> = ({
	value = { permissions: [], requiresAll: false },
	onChange,
}) => {
	const { t: $t } = useTranslation();
	const { deserialize } = localizationSerializer();
	const { Lr } = useLocalizer();

	const { data: treeData, isLoading } = useQuery({
		queryKey: ["permissionStateCheckData"],
		queryFn: async () => {
			const [groupsRes, permissionsRes] = await Promise.all([getGroupsApi(), getPermissionsApi()]);

			const permissionNodes = permissionsRes.items.map((p) => {
				const d = deserialize(p.displayName);
				return {
					title: Lr(d.resourceName, d.name),
					key: p.name,
					value: p.name,
					groupName: p.groupName,
					parentName: p.parentName,
					isLeaf: true,
				};
			});

			const groupNodes = groupsRes.items.map((g) => {
				const d = deserialize(g.displayName);
				return {
					title: Lr(d.resourceName, d.name),
					key: g.name,
					value: g.name,
					selectable: false,
					children: listToTree(
						permissionNodes.filter((p) => p.groupName === g.name),
						{ id: "key", pid: "parentName" },
					),
				};
			});

			return groupNodes;
		},
	});

	const triggerChange = (changedValue: Partial<ValueType>) => {
		onChange?.({ ...value, ...changedValue });
	};

	/**
	 * Handle TreeSelect Change.
	 * When treeCheckStrictly is true, val is { label: string, value: string }[]
	 * We must map it back to string[] for the data model.
	 */
	const handleTreeChange = (val: any) => {
		// Check if val is array (it should be)
		if (Array.isArray(val)) {
			const permissionNames = val.map((item) => item.value);
			triggerChange({ permissions: permissionNames });
		} else {
			triggerChange({ permissions: [] });
		}
	};

	/**
	 * Prepare Value for TreeSelect.
	 * When treeCheckStrictly is true, it expects objects.
	 * We map our string[] model to { value, label }[] objects so AntD displays them correctly.
	 */
	const treeSelectValue = useMemo(() => {
		return (value.permissions || []).map((p) => ({
			value: p,
			label: p, // AntD will try to match this with treeData to show the real label
		}));
	}, [value.permissions]);

	if (isLoading) return <Spin />;

	return (
		<div className="flex flex-col gap-4 w-full">
			<Checkbox checked={value.requiresAll} onChange={(e) => triggerChange({ requiresAll: e.target.checked })}>
				{$t("component.simple_state_checking.requirePermissions.requiresAll")}
			</Checkbox>
			<TreeSelect
				treeData={treeData}
				value={treeSelectValue}
				onChange={handleTreeChange}
				allowClear
				treeCheckable
				treeCheckStrictly
				showCheckedStrategy={TreeSelect.SHOW_PARENT}
				style={{ width: "100%" }}
				placeholder={$t("ui.placeholder.select")}
			/>
		</div>
	);
};

export default PermissionStateCheck;
