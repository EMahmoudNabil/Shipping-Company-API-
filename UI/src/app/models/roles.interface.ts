export interface Permission {
  name: string;
  add: string;
  view: string;
  update: string;
  delete: string;
}

export interface RoleResponseDTO {
  roleId: string;
  roleName: string;
  createdAt: string;
}

export interface CreateRoleRequestDTO {
  roleName: string;
  permissions: string[];
}

export interface RoleResponseDTO {
  roleId: string;
  roleName: string;
  createdAt: string;
  permissions: string[]; // Add this line
}