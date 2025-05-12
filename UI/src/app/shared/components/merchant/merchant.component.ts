import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { UnitOfWorkServices } from '../../../core/services/unitOfWork.service';
import { Branch } from '../../../models/Branch.Interface';
import { City } from '../../../models/City.interface'; 
import { Region } from '../../../models/Region.Interface ';
import { Merchant } from '../../../models/Merchant .Interface';


@Component({
  selector: 'app-merchant',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './merchant.component.html',
  styleUrl: './merchant.component.css'
})
export class MerchantComponent implements OnInit {
  @ViewChild('merchantForm') merchantForm!: NgForm;
  
  branches: Branch[] = [];
  regions: Region[] = [];
  cities: City[] = [];
  merchant: Merchant = this.emptyMerchant();
  cityPrices: { [key: number]: number } = {};
  citySelections: { [key: number]: boolean } = {};
  
  isSubmitting = false;

  constructor(
    private toastr: ToastrService,
    private _UnitOfWorkServices: UnitOfWorkServices
  ) {}

  ngOnInit(): void {
    this.loadBranches();
    this.loadRegions();
  }

  private emptyMerchant(): Merchant {
    return {
      email: '',
      password: '',
      fullName: '',
      phoneNumber: '',
      address: '',
      branchId: 0,
      regionId: 0,
      cityId: 0,
      storeName: '',
      specialCityCosts: []
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
      },
      error: (error) => {
        const message = this.getErrorMessage(error);
        this.toastr.error(message);
      }
    });
  }

  loadCities(regionId: number): void {
    if (!regionId) return;
    
    this._UnitOfWorkServices.City.getByRegionId(regionId).subscribe({
      next: (data) => {
        this.cities = data;
        if (this.cities.length > 0) {
          this.merchant.cityId = 0; // Reset city selection
        }
      },
      error: (error) => {
        const message = this.getErrorMessage(error);
        this.toastr.error(message);
      }
    });
  }

  onRegionChange(): void {
    this.loadCities(this.merchant.regionId);
  }

  createMerchant(): void {
    if (this.merchantForm.invalid) {
      this.toastr.error('يرجى تعبئة جميع الحقول المطلوبة بشكل صحيح');
      // Mark all form controls as touched to trigger validation messages
      Object.keys(this.merchantForm.controls).forEach(key => {
        this.merchantForm.controls[key].markAsTouched();
      });
      return;
    }

    // Build specialCityCosts array
    this.merchant.specialCityCosts = [];
    Object.keys(this.citySelections).forEach(key => {
      const cityId = parseInt(key);
      if (this.citySelections[cityId]) {
        this.merchant.specialCityCosts.push({
          citySettingId: cityId,
          price: this.cityPrices[cityId] || 0
        });
      }
    });

    this.isSubmitting = true;

    this._UnitOfWorkServices.Merchant.create(this.merchant).subscribe({
      next: (response) => {
        this.toastr.success('تم تسجيل التاجر بنجاح');
        this.resetForm();
        this.isSubmitting = false;
      },
      error: (error) => {
        const message = this.getErrorMessage(error);
        this.toastr.error(message);
        this.isSubmitting = false;
      }
    });
  }

  resetForm(): void {
    this.merchant = this.emptyMerchant();

    Object.keys(this.citySelections).forEach(key => {
      const cityId = parseInt(key);
      this.citySelections[cityId] = false;
    });
    
    if (this.merchantForm) {
      this.merchantForm.resetForm();
    }
  }

  getErrorMessage(error: any): string {
    return error.message || 'حدث خطأ غير معروف';
  }
}