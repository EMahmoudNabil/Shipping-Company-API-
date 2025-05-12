import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RoleService } from '../../../core/services/role.service';
import { RoleResponseDTO, CreateRoleRequestDTO, Permission } from '../../../models/roles.interface';
import { PageHeaderComponent } from "../page-header/page-header.component";

@Component({
  selector: 'app-role',
  templateUrl: './permission.component.html',
  styleUrls: ['./permission.component.css'],
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule, PageHeaderComponent]
})
export class RoleComponent implements OnInit {
// Add this method to your RoleComponent class
getPageNumbers(): number[] {
  const totalPages = this.totalPages;
  const currentPage = this.currentPage;
  const pages: number[] = [];
  
  // Always show first page
  pages.push(1);
  
  // Show pages around current page
  const start = Math.max(2, currentPage - 2);
  const end = Math.min(totalPages - 1, currentPage + 2);
  
  if (start > 2) {
    pages.push(-1); // Use -1 to represent ellipsis
  }
  
  for (let i = start; i <= end; i++) {
    pages.push(i);
  }
  
  if (end < totalPages - 1) {
    pages.push(-1); // Use -1 to represent ellipsis
  }
  
  // Always show last page if there's more than one page
  if (totalPages > 1) {
    pages.push(totalPages);
  }
  
  return pages;
}
  roles: RoleResponseDTO[] = [];
  permissionsList: Permission[] = [
    {
      name: 'الحسابات',
      add: 'Accounts:AddAccounts',
      view: 'Accounts:ViewAccounts',
      update: 'Accounts:UpdateAccounts',
      delete: 'Accounts:DeleteAccounts'
    },
    {
      name: 'الاعدادات',
      add: 'Settings:AddSettings',
      view: 'Settings:ViewSettings',
      update: 'Settings:UpdateSettings',
      delete: 'Settings:DeleteSettings'
    },
    {
      name: 'الصلاحيات',
      add: 'Permissions:AddPermissions',
      view: 'Permissions:ViewPermissions',
      update: 'Permissions:UpdatePermissions',
      delete: 'Permissions:DeletePermissions'
    },
    {
      name: 'المدن',
      add: 'Cities:AddCities',
      view: 'Cities:ViewCities',
      update: 'Cities:UpdateCities',
      delete: 'Cities:DeleteCities'
    },
    {
      name: 'الخزن',
      add: 'MoneySafe:AddMoneySafe',
      view: 'MoneySafe:ViewMoneySafe',
      update: 'MoneySafe:UpdateMoneySafe',
      delete: 'MoneySafe:DeleteMoneySafe'
    },
    {
      name: 'الموظفين',
      add: 'Employees:AddEmployees',
      view: 'Employees:ViewEmployees',
      update: 'Employees:UpdateEmployees',
      delete: 'Employees:DeleteEmployees'
    },
    {
      name: 'التجار',
      add: 'Merchants:AddMerchants',
      view: 'Merchants:ViewMerchants',
      update: 'Merchants:UpdateMerchants',
      delete: 'Merchants:DeleteMerchants'
    },
    {
      name: 'الفروع',
      add: 'Branches:AddBranches',
      view: 'Branches:ViewBranches',
      update: 'Branches:UpdateBranches',
      delete: 'Branches:DeleteBranches'
    },
    {
      name: 'المناديب',
      add: 'Couriers:AddCouriers',
      view: 'Couriers:ViewCouriers',
      update: 'Couriers:UpdateCouriers',
      delete: 'Couriers:DeleteCouriers'
    },
    {
      name: 'المناطق',
      add: 'Regions:AddRegions',
      view: 'Regions:ViewRegions',
      update: 'Regions:UpdateRegions',
      delete: 'Regions:DeleteRegions'
    },
    {
      name: 'الطلبات',
      add: 'Orders:AddOrders',
      view: 'Orders:ViewOrders',
      update: 'Orders:UpdateOrders',
      delete: 'Orders:DeleteOrders'
    },
    {
      name: 'المدن',
      add: 'Cities:AddCities',
      view: 'Cities:ViewCities',
      update: 'Cities:UpdateCities',
      delete: 'Cities:DeleteCities'
    },
    {
      name: 'تقارير الشحنات',
      add: 'OrderReports:AddOrderReports',
      view: 'OrderReports:ViewOrderReports',
      update: 'OrderReports:UpdateOrderReports',
      delete: 'OrderReports:DeleteOrderReports'
    }
  ];
  paginatedRoles: RoleResponseDTO[] = [];
  roleForm!: FormGroup;
  loading = false;
  error: string | null = null;

  // Pagination settings
  currentPage = 1;
  totalPages = 1;
  displayCount = 10;

// زياده
  editMode = false;
  currentRoleId: string | null = null;

  constructor(private roleService: RoleService, private fb: FormBuilder) {
    this.initForm();
  }

  private initForm(): void {
    this.roleForm = this.fb.group({
      roleName: ['', Validators.required],
      permissions: [[]]
    });
  }


  resetForm(): void {
    this.editMode = false;
    this.currentRoleId = null;
    this.roleForm.reset({
      roleName: '',
      permissions: []
    });
    this.error = null;
  }

  ngOnInit(): void {
    this.loadRoles();
  }

  loadRoles(): void {
    this.loading = true;
    this.roleService.getAllRoles().subscribe(
      (roles) => {
        this.roles = roles;
        this.paginateRoles();
        this.loading = false;
      },
      (error) => {
        this.error = 'Failed to load roles.';
        this.loading = false;
      }
    );
  }

  onSearch(event: any): void {
    const query = event.target.value.toLowerCase();
    this.roles = this.roles.filter((role) => role.roleName.toLowerCase().includes(query));
  }

// Update the onSubmit method
onSubmit(): void {
  if (this.roleForm.valid) {
    const roleData: CreateRoleRequestDTO = {
      roleName: this.roleForm.value.roleName.trim(),
      permissions: this.roleForm.value.permissions || []
    };

    console.log('Sending role data:', roleData);
    this.loading = true;

    if (this.editMode && this.currentRoleId) {
      // Update existing role
      this.roleService.updateRole(this.currentRoleId, roleData).subscribe({
        next: (response) => {
          console.log('Update success:', response);
          this.loading = false;
          this.resetForm();
          this.loadRoles();
          
          const modalElement = document.getElementById('roleModal');
          const modal = (window as any).bootstrap.Modal.getInstance(modalElement!);
          modal?.hide();
        },
        error: (error) => {
          console.log('Update error:', error.error);
          this.loading = false;
          this.error = 'فشل في تحديث الصلاحية: ' + (error.error?.message || error.message);
        }
      });
    } else {
      // Create new role
      this.roleService.createRole(roleData).subscribe({
        next: (response) => {
          console.log('Create success:', response);
          this.loading = false;
          this.resetForm();
          this.loadRoles();
          
          const modalElement = document.getElementById('roleModal');
          const modal = (window as any).bootstrap.Modal.getInstance(modalElement!);
          modal?.hide();
        },
        error: (error) => {
          console.log('Create error:', error.error);
          this.loading = false;
          this.error = 'فشل في إنشاء الصلاحية: ' + (error.error?.message || error.message);
        }
      });
    }
  } else {
    Object.keys(this.roleForm.controls).forEach(key => {
      const control = this.roleForm.get(key);
      control?.markAsTouched();
    });
    
    this.error = 'الرجاء ملء جميع الحقول المطلوبة';
  }
}

// Update the onPermissionChange method to handle permissions correctly
onPermissionChange(event: Event, permission: string): void {
  const checkbox = event.target as HTMLInputElement;
  let currentPermissions = [...(this.roleForm.get('permissions')?.value || [])];
  
  if (checkbox.checked) {
    if (!currentPermissions.includes(permission)) {
      currentPermissions.push(permission);
    }
  } else {
    currentPermissions = currentPermissions.filter(p => p !== permission);
  }
  
  this.roleForm.patchValue({ permissions: currentPermissions });
  console.log('Current permissions:', currentPermissions); // For debugging
}


  editRole(role: RoleResponseDTO): void {
    this.editMode = true;
    this.currentRoleId = role.roleId;
    
    this.roleService.getRoleById(role.roleId).subscribe({
      next: (roleDetails) => {
        this.roleForm.patchValue({
          roleName: roleDetails.roleName,
          permissions: roleDetails.permissions
        });
        console.log('Loaded permissions:', roleDetails.permissions);
      },
      error: (error) => {
        console.error('Error loading role details:', error);
        this.error = 'Failed to load role details';
      }
    });
  }

  deleteRole(roleId: string): void {
    this.roleService.deleteRole(roleId).subscribe(
      () => {
        this.loadRoles();
      },
      (error) => {
        this.error = 'Failed to delete role.';
      }
    );
  }

  changePage(page: number): void {
    this.currentPage = page;
    this.paginateRoles();
  }

  paginateRoles(): void {
    this.totalPages = Math.ceil(this.roles.length / this.displayCount);
    this.paginatedRoles = this.roles.slice((this.currentPage - 1) * this.displayCount, this.currentPage * this.displayCount);
  }

  setDisplayCount(count: number): void {
    this.displayCount = count;
    this.paginateRoles();
  }
}
