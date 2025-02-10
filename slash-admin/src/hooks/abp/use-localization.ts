import { useEffect, useMemo } from "react";
import { mergeRight } from "ramda";
import type { Dictionary } from "#/abp-core/global";
import { format } from "@/utils/string";
import useLocaleStore, { useLocale } from "@/store/localeI18nStore";

// const { L } = useLocalizer(['AbpAuditLogging', 'AbpUi']);

/*
	const { t: $t } = useTranslation();
	const { L } = useLocalizer();
  console.log(L('DisplayName:ClaimValue')) //和getI18nLocales()获取的不同，这个是没有前缀key的
  console.log($t('AbpIdentity.DisplayName:ClaimValue'))
*/
export function useLocalizer(resourceNames?: string | string[], callback?: () => void) {
	const localizations = useLocaleStore.getState().localizations || {};
	const locale = useLocale();

	const mergedResources = useMemo(() => {
		let merged: Dictionary<string, string> = {};
		if (resourceNames) {
			if (Array.isArray(resourceNames)) {
				resourceNames.forEach((name) => {
					merged = mergeRight(merged, localizations[name] || {});
				});
			} else {
				merged = mergeRight(merged, localizations[resourceNames] || {});
			}
		} else {
			// Merge everything if no resource names provided
			Object.keys(localizations).forEach((r) => {
				merged = mergeRight(merged, localizations[r] || {});
			});
		}
		return merged;
	}, [resourceNames, localizations]);

	// biome-ignore lint/correctness/useExhaustiveDependencies:
	useEffect(() => {
		if (callback) {
			callback();
		}
	}, [locale, callback]);

	function L(key: string, args?: any[] | Record<string, string>) {
		if (!key) return "";
		return mergedResources[key] ? format(mergedResources[key], args ?? []) : key;
	}

	function Lr(resource: string, key: string, args?: any[] | Record<string, string>) {
		if (!resource || !key) return "";
		// const { localizations } = useLocaleStore.getState();
		const subResource = localizations[resource] ?? {};
		return subResource[key] ? format(subResource[key], args ?? []) : key;
	}

	return { L, Lr };
}
