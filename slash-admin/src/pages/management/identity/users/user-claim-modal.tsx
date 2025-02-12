import type React from "react";
import { Modal } from "antd";
import { useTranslation } from "react-i18next";
import type { IdentityUserDto } from "#/identity";
import type { IdentityClaimCreateDto, IdentityClaimDeleteDto, IdentityClaimUpdateDto } from "#/identity/claims";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { createClaimApi, deleteClaimApi, getClaimsApi, updateClaimApi } from "@/api/identity/users";
import { IdentityRolePermissions } from "@/constants/identity/permissions";
import ClaimTable from "@/components/abp/claims/claim-table";

interface Props {
	visible: boolean;
	onClose: () => void;
	user: IdentityUserDto;
}

const RoleClaimModal: React.FC<Props> = ({ visible, onClose, user }) => {
	const { t: $t } = useTranslation();
	const queryClient = useQueryClient();
	const queryKey = ["userClaims", user.id];

	// Mutations for CRUD operations
	const { mutateAsync: createClaim } = useMutation({
		mutationFn: (input: IdentityClaimCreateDto) => createClaimApi(user.id, input),
		onSuccess: () => {
			queryClient.invalidateQueries({ queryKey });
		},
	});

	const { mutateAsync: updateClaim } = useMutation({
		mutationFn: (input: IdentityClaimUpdateDto) => updateClaimApi(user.id, input),
		onSuccess: () => {
			queryClient.invalidateQueries({ queryKey });
		},
	});

	const { mutateAsync: deleteClaim } = useMutation({
		mutationFn: (input: IdentityClaimDeleteDto) => deleteClaimApi(user.id, input),
		onSuccess: () => {
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
				getApi={async () => {
					return await getClaimsApi(user.id); //其它的可以保持原样
				}}
			/>
		</Modal>
	);
};

export default RoleClaimModal;
