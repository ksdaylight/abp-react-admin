import type { IHasSimpleStateCheckers, ISimpleStateChecker } from "#/abp-core";

export interface SimplaCheckStateBase extends IHasSimpleStateCheckers<SimplaCheckStateBase> {
	stateCheckers: ISimpleStateChecker<SimplaCheckStateBase>[];
}

export interface SimpleStateCheckValue {
	name: string; // 'A' | 'F' | 'G' | 'P'
	value?: {
		requiresAll?: boolean;
		featureNames?: string[];
		globalFeatureNames?: string[];
		permissions?: string[];
	};
}
