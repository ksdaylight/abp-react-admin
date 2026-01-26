import type React from "react";
import { Checkbox, TreeSelect, Spin } from "antd";
import { useTranslation } from "react-i18next";
import { useQuery } from "@tanstack/react-query";
import { getListApi as getFeaturesApi } from "@/api/management/features/feature-definitions";
import { getListApi as getGroupsApi } from "@/api/management/features/feature-group-definitions";
import { listToTree } from "@/utils/tree";
import { localizationSerializer } from "@/utils/abp/localization-serializer";
import { useLocalizer } from "@/hooks/abp/use-localization";
import { valueTypeSerializer } from "@/components/abp/string-value-type";

interface ValueType {
	featureNames: string[];
	requiresAll: boolean;
}

interface FeatureStateCheckProps {
	value?: ValueType;
	onChange?: (value: ValueType) => void;
}

const FeatureStateCheck: React.FC<FeatureStateCheckProps> = ({
	value = { featureNames: [], requiresAll: false },
	onChange,
}) => {
	const { t: $t } = useTranslation();
	const { deserialize } = localizationSerializer();
	const { Lr } = useLocalizer();

	const { data: treeData, isLoading } = useQuery({
		queryKey: ["featureStateCheckData"],
		queryFn: async () => {
			const [groupsRes, featuresRes] = await Promise.all([getGroupsApi(), getFeaturesApi()]);

			// Filter only Boolean features as per logic
			const validFeatures = featuresRes.items.filter((item) => {
				if (item.valueType) {
					try {
						const vt = valueTypeSerializer.deserialize(item.valueType);
						return vt.validator.name === "BOOLEAN";
					} catch {
						return false;
					}
				}
				return true; // Default behavior if no valuetype? Vue code implies filtering strictly.
			});

			const featureNodes = validFeatures.map((f) => {
				const d = deserialize(f.displayName);
				return {
					...f,
					title: Lr(d.resourceName, d.name),
					key: f.name,
					value: f.name,
					isLeaf: true,
				};
			});

			const groupNodes = groupsRes.items.map((g) => {
				const d = deserialize(g.displayName);
				return {
					title: Lr(d.resourceName, d.name),
					key: g.name,
					value: g.name,
					selectable: false, // Groups are just containers
					children: listToTree(
						featureNodes.filter((f) => f.groupName === g.name),
						{ id: "name", pid: "parentName" },
					),
				};
			});

			return groupNodes;
		},
	});

	const triggerChange = (changedValue: Partial<ValueType>) => {
		onChange?.({ ...value, ...changedValue });
	};

	if (isLoading) return <Spin />;

	return (
		<div className="flex flex-col gap-4 w-full">
			<Checkbox checked={value.requiresAll} onChange={(e) => triggerChange({ requiresAll: e.target.checked })}>
				{$t("component.simple_state_checking.requireFeatures.requiresAll")}
			</Checkbox>
			<TreeSelect
				treeData={treeData}
				value={value.featureNames}
				onChange={(val) => triggerChange({ featureNames: val })}
				allowClear
				treeCheckable
				showCheckedStrategy={TreeSelect.SHOW_PARENT}
				placeholder={$t("ui.placeholder.select")}
				style={{ width: "100%" }}
			/>
		</div>
	);
};

export default FeatureStateCheck;
