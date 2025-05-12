import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Branch } from '../../../models/Branch.Interface';
import { Region } from '../../../models/Region.Interface ';
import { UnitOfWorkServices } from '../../../core/services/unitOfWork.service';
import { ToastrService } from 'ngx-toastr';
import { CommonModule } from '@angular/common';
import { Role } from '../../../models/Role.interface';
import { PageHeaderComponent } from "../page-header/page-header.component";
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { MatTableModule } from '@angular/material/table';




@Component({
  selector: 'app-add-employee',
  imports: [ReactiveFormsModule, CommonModule, PageHeaderComponent,
    MatCardModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatProgressBarModule,
    MatTooltipModule,
    MatTableModule
  ],
  templateUrl: './add-employee.component.html',
  styleUrl: './add-employee.component.css'
})
export class AddEmployeeComponent implements OnInit { 
  employeeForm: FormGroup;
  branches: Branch[] = [];
  regions: Region[] = [];
  // roles: string[] = ['Admin', 'Employee', 'Driver']; // Adjust based on your roles
  roles: Role[] = [];
  isLoadingRoles = false;
  constructor(
    private fb: FormBuilder,
    private _unitOfWork: UnitOfWorkServices,
    private toastr: ToastrService
  ) {
    this.employeeForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      fullName: ['', Validators.required],
      phoneNumber: ['', [Validators.required, Validators.pattern(/^01[0125][0-9]{8}$/)]],
      address: ['', Validators.required],
      branchId: ['', Validators.required],
      regionID: ['', Validators.required],
      roleName: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.loadBranches();
    this.loadRegions();
    this.loadRoles();



  }

  loadBranches(): void {
    this._unitOfWork.Branch.getAll().subscribe({
      next: (branches) => this.branches = branches,
      error: (err) => console.error('Error loading branches:', err)
    });
  }

  loadRegions(): void {
    this._unitOfWork.Region.getAll().subscribe({
      next: (regions) => this.regions = regions,
      error: (err) => console.error('Error loading regions:', err)
    });
  }
  loadRoles(): void {
    this.isLoadingRoles = true;
    this._unitOfWork.Employee.getRoles().subscribe({
      next: (roles) => {
        this.roles = roles;
        this.isLoadingRoles = false;
      },
      error: (err) => {
        console.error('Error loading roles:', err);
        this.isLoadingRoles = false;
      }
    });
  }




  isSubmitting = false;

  onSubmit(): void {
    if (this.employeeForm.valid) {
      this.isSubmitting = true;
      
      const formData = {
        ...this.employeeForm.value,
        branchId: +this.employeeForm.value.branchId,
        regionID: +this.employeeForm.value.regionID
      };

      this._unitOfWork.Employee.createEmployee(formData).subscribe({
        next: () => {
          this.isSubmitting = false;
          this.toastr.success('تمت الإضافة بنجاح', 'نجاح');
          this.employeeForm.reset();
        },
        error: (err) => {
          this.isSubmitting = false;
          this.toastr.error('فشل في الإضافة', 'خطأ');
          console.error('Error:', err);
        }
      });
    }
  }

}
