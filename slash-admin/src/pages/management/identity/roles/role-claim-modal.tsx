import type React from "react";
import { Modal } from "antd";
import { useTranslation } from "react-i18next";
import type { IdentityRoleDto } from "#/identity";
import type { IdentityClaimCreateDto, IdentityClaimDeleteDto, IdentityClaimUpdateDto } from "#/identity/claims";
import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import { createClaimApi, deleteClaimApi, getClaimsApi, updateClaimApi } from "@/api/identity/role";
import { IdentityRolePermissions } from "@/constants/identity/permissions";
import { toast } from "sonner";
import ClaimTable from "@/components/abp/claims/claim-table";

interface Props {
	visible: boolean;
	onClose: () => void;
	role: IdentityRoleDto;
}

const RoleClaimModal: React.FC<Props> = ({ visible, onClose, role }) => {
	const { t: $t } = useTranslation();
	const queryClient = useQueryClient();
	const queryKey = ["roleClaims", role.id];

	// Query for getting claims
	const { data: claims } = useQuery({
		queryKey,
		queryFn: () => getClaimsApi(role.id),
		enabled: visible,
	});

	// Mutations for CRUD operations
	const { mutateAsync: createClaim } = useMutation({
		mutationFn: (input: IdentityClaimCreateDto) => createClaimApi(role.id, input),
		onSuccess: () => {
			toast.success($t("AbpUi.SuccessfullyCreated"));
			queryClient.invalidateQueries({ queryKey });
		},
	});

	const { mutateAsync: updateClaim } = useMutation({
		mutationFn: (input: IdentityClaimUpdateDto) => updateClaimApi(role.id, input),
		onSuccess: () => {
			toast.success($t("AbpUi.SuccessfullyUpdated"));
			queryClient.invalidateQueries({ queryKey });
		},
	});

	const { mutateAsync: deleteClaim } = useMutation({
		mutationFn: (input: IdentityClaimDeleteDto) => deleteClaimApi(role.id, input),
		onSuccess: () => {
			toast.success($t("AbpUi.DeletedSuccessfully"));
			queryClient.invalidateQueries({ queryKey });
		},
	});

	return (
		<Modal
			title={$t("AbpIdentity.ManageClaim")}
			open={visible}
			onCancel={onClose}
			footer={null}
			width={800}
			destroyOnClose
		>
			<ClaimTable
				createApi={createClaim}
				createPolicy={IdentityRolePermissions.ManageClaims}
				deleteApi={deleteClaim}
				deletePolicy={IdentityRolePermissions.ManageClaims}
				updateApi={updateClaim}
				updatePolicy={IdentityRolePermissions.ManageClaims}
				getApi={() => Promise.resolve(claims || { items: [] })} //简单伪装
			/>
		</Modal>
	);
};

export default RoleClaimModal;
