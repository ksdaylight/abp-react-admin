import userStore from "@/store/userStore";
import React from "react";

export const withAccessChecker = (element: React.ReactElement, requiredCodes: string[]) => {
	const accessCodes = userStore.getState().accessCodes;

	const canAccess = requiredCodes.every((code) => accessCodes.includes(code));

	return canAccess ? element : null;
};
