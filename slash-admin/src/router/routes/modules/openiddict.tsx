import { Suspense, lazy } from "react";

import { Iconify } from "@/components/icon";
import { CircleLoading } from "@/components/loading";

import type { AppRouteObject } from "#/router";
import { Outlet } from "react-router";

const OpenIddictApplications = lazy(() => import("@/pages/openiddict/applications/application-table"));

const OpenIddictAuthorizations = lazy(() => import("@/pages/openiddict/authorizations/authorization-table"));

const openiddict: AppRouteObject[] = [
	{
		order: 2.2, //暂时挨着management，后续再调整
		path: "openiddict",
		element: (
			<Suspense fallback={<CircleLoading />}>
				<Outlet />
			</Suspense>
		),
		meta: {
			label: "abp.openiddict.title",
			icon: <Iconify icon="mdi:openid" size={24} />,
			key: "/openiddict",
		},
		children: [
			{
				path: "applications",
				element: <OpenIddictApplications />,
				meta: {
					icon: <Iconify icon="carbon:application" />,
					label: "abp.openiddict.applications",
					key: "/openiddict/applications",
				},
			},
			{
				path: "authorizations",
				element: <OpenIddictAuthorizations />,
				meta: {
					icon: <Iconify icon="arcticons:ente-authenticator" />,
					label: "abp.openiddict.authorizations",
					key: "/openiddict/authorizations",
				},
			},
		],
	},
];

export default openiddict;
