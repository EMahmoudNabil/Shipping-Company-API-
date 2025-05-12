import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { UnitOfWorkServices } from '../../../core/services/unitOfWork.service';
import { Branch } from '../../../models/Branch.Interface';
import { HttpErrorResponse } from '@angular/common/http';
import { Courier } from '../../../models/Courier.interface';
import { Region } from '../../../models/Region.Interface ';

@Component({
  selector: 'app-courier',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './courier.component.html',
  styleUrl: './courier.component.css'
})
export class CourierComponent implements OnInit {
  @ViewChild('courierForm') courierForm!: NgForm;
  
  branches: Branch[] = [];
  regions: Region[] = [];
  courier: Courier = this.emptyCourier();
  regionSelections: { [key: number]: boolean } = {};
  
  isSubmitting = false;

  constructor(
    private toastr: ToastrService,
    private _UnitOfWorkServices: UnitOfWorkServices
  ) {}

  ngOnInit(): void {
    this.loadBranches();
    this.loadRegions();
  }

  private emptyCourier(): Courier {
    return {
      email: '',
      password: '',
      fullName: '',
      phoneNumber: '',
      address: '',
      branchId: 0,
      deductionType: 0,
      deductionCompanyFromOrder: 0,
      specialCourierRegions: []
    };
  }

  loadBranches(): void {
    this._UnitOfWorkServices.Branch.getAll().subscribe({
      next: (data) => {
        this.branches = data.filter(branch => !branch.isDeleted);
      },
      error: (error) => {
        const message = this.getErrorMessage(error);
        this.toastr.error(message);
      }
    });
  }

  loadRegions(): void {
    this._UnitOfWorkServices.Region.getAll().subscribe({
      next: (data) => {
        this.regions = data;
        this.initializeRegionSelections();
      },
      error: (error) => {
        const message = this.getErrorMessage(error);
        this.toastr.error(message);
      }
    });
  }

  initializeRegionSelections(): void {
    // Initialize selections for all regions to false
    this.regions.forEach(region => {
      this.regionSelections[region.id] = false;
    });
  }

  createCourier(): void {
    if (this.courierForm.invalid) {
      this.toastr.error('يرجى تعبئة جميع الحقول المطلوبة بشكل صحيح');
      // Mark all form controls as touched to trigger validation messages
      Object.keys(this.courierForm.controls).forEach(key => {
        this.courierForm.controls[key].markAsTouched();
      });
      return;
    }

    // Check if at least one region is selected
    const hasSelectedRegions = Object.values(this.regionSelections).some(value => value);
    if (!hasSelectedRegions) {
      this.toastr.error('يرجى تحديد منطقة واحدة على الأقل');
      return;
    }

    // Build specialCourierRegions array
    this.courier.specialCourierRegions = [];
    Object.keys(this.regionSelections).forEach(key => {
      const regionId = parseInt(key);
      if (this.regionSelections[regionId]) {
        this.courier.specialCourierRegions.push({
          regionId: regionId
        });
      }
    });

    console.log('Prepared courier data:', JSON.stringify(this.courier));
    this.isSubmitting = true;

    this._UnitOfWorkServices.Courier.create(this.courier).subscribe({
      next: (response) => {
        this.toastr.success('تم تسجيل المندوب بنجاح');
        this.resetForm();
        this.isSubmitting = false;
      },
      error: (error: HttpErrorResponse) => {
        this.isSubmitting = false;
        
        // Enhanced error handling
        console.error('API Error:', error);
        
        let errorMessage = 'حدث خطأ أثناء تسجيل المندوب';
        
        if (error.status === 400) {
          if (error.error && typeof error.error === 'string' && error.error.length > 0) {
            errorMessage = error.error;
          } else if (error.error && error.error.errors) {
            const validationErrors = error.error.errors;
            const firstError = Object.values(validationErrors)[0];
            if (Array.isArray(firstError) && firstError.length > 0) {
              errorMessage = firstError[0];
            }
          }
        }
        
        this.toastr.error(errorMessage);
      }
    });
  }

  resetForm(): void {
    this.courier = this.emptyCourier();
    this.initializeRegionSelections();
    
    if (this.courierForm) {
      this.courierForm.resetForm();
    }
  }

  getErrorMessage(error: any): string {
    if (error.error && typeof error.error === 'string' && error.error.length > 0) {
      return error.error;
    }
    if (error.message) {
      return error.message;
    }
    return 'حدث خطأ غير معروف';
  }
}