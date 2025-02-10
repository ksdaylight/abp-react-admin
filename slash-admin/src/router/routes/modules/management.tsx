import { Suspense, lazy } from "react";
import { Navigate, Outlet } from "react-router";

import { Iconify, SvgIcon } from "@/components/icon";
import { CircleLoading } from "@/components/loading";

import type { AppRouteObject } from "#/router";

const ProfilePage = lazy(() => import("@/pages/management/user/profile"));
const AccountPage = lazy(() => import("@/pages/management/user/account"));

const OrganizationPage = lazy(() => import("@/pages/management/system/organization"));
const PermissioPage = lazy(() => import("@/pages/management/system/permission"));

const Blog = lazy(() => import("@/pages/management/blog"));
const SecurityLogs = lazy(() => import("@/pages/management/identity/security-logs/security-logs-table"));
const ClaimTypes = lazy(() => import("@/pages/management/identity/claim-types/claim-types-table"));

const AuditingAuditLogs = lazy(() => import("@/pages/management/audit-logs/audit-log-table"));
const PermissionDefinitions = lazy(
	() => import("@/pages/management/permissions/permissions/permission-definition-table"),
);

const PermissionGroupDefinition= lazy(
	() => import("@/pages/management/permissions/definitions/permission-group-definition-table"),
);
const management: AppRouteObject = {
	order: 2,
	path: "management",
	element: (
		<Suspense fallback={<CircleLoading />}>
			<Outlet />
		</Suspense>
	),
	meta: {
		label: "sys.menu.management",
		icon: <SvgIcon icon="ic-management" className="ant-menu-item-icon" size="24" />,
		key: "/management",
	},
	children: [
		{
			index: true,
			element: <Navigate to="user" replace />,
		},
		{
			path: "user",
			meta: { label: "sys.menu.user.index", key: "/management/user" },
			children: [
				{
					index: true,
					element: <Navigate to="profile" replace />,
				},
				{
					path: "profile",
					element: <ProfilePage />,
					meta: {
						label: "sys.menu.user.profile",
						key: "/management/user/profile",
					},
				},
				{
					path: "account",
					element: <AccountPage />,
					meta: {
						label: "sys.menu.user.account",
						key: "/management/user/account",
					},
				},
			],
		},
		{
			path: "system",
			meta: { label: "sys.menu.system.index", key: "/management/system" },
			children: [
				{
					path: "organization",
					element: <OrganizationPage />,
					meta: {
						label: "sys.menu.system.organization",
						key: "/management/system/organization",
					},
				},
				{
					path: "permission",
					element: <PermissioPage />,
					meta: {
						label: "sys.menu.system.permission",
						key: "/management/system/permission",
					},
				},
			],
		},
		{
			path: "blog",
			element: <Blog />,
			meta: { label: "sys.menu.blog", key: "/management/blog" },
		},
		{
			path: "identity",
			meta: {
				label: "abp.manage.identity.title",
				key: "/management/identity",
				icon: <Iconify icon="teenyicons:id-outline" />,
			},
			children: [
				{
					index: true,
					element: <Navigate to="security-logs" replace />, //TODO 修改默认子级
				},
				{
					path: "claim-types",
					element: <ClaimTypes />,
					meta: {
						label: "abp.manage.identity.claimTypes",
						key: "/management/identity/claim-types",
						icon: <Iconify icon="la:id-card-solid" />,
					},
				},
				{
					path: "security-logs",
					element: <SecurityLogs />,
					meta: {
						label: "abp.manage.identity.securityLogs",
						key: "/management/identity/security-logs",
						icon: <Iconify icon="carbon:security" />,
					},
				},
			],
		},
		{
			path: "permissions",
			meta: {
				label: "abp.manage.permissions.title",
				key: "/management/permissions",
				icon: <Iconify icon="arcticons:permissionsmanager" />,
			},
			children: [
				{
					index: true,
					element: <Navigate to="groups" replace />,
				},
				{
					path: "groups",
					element: <PermissionGroupDefinition />,
					meta: {
						label: "abp.manage.permissions.groups",
						key: "/management/permissions/groups",
						icon: <Iconify icon="lucide:group" />,
					},
				},
				{
					path: "definitions",
					element: <PermissionDefinitions />,
					meta: {
						label: "abp.manage.permissions.definitions",
						key: "/management/permissions/definitions",
						icon: <Iconify icon="icon-park-outline:permissions" />,
					},
				},
			],
		},
		{
			path: "audit-logs",
			element: <AuditingAuditLogs />,
			meta: {
				label: "abp.manage.identity.auditLogs",
				key: "/management/audit-logs",
				icon: <Iconify icon="fluent-mdl2:compliance-audit" />,
			},
		},
	],
};

export default management;
