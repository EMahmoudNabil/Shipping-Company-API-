import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { City } from '../../../models/City.interface';
import { UnitOfWorkServices } from '../../../core/services/unitOfWork.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-city',
  imports: [FormsModule, CommonModule],
  templateUrl: './city.component.html',
  styleUrls: ['./city.component.css'],
  standalone: true,
})
export class CityComponent implements OnInit {
  cities: City[] = [];
  selectedCity: Partial<City> = this.emptyCity();
  regions: { id: number; governorate: string }[] = [];
  showModal = false;
  isEditMode = false;
  deletingId: number | null = null;
  PageNumber: number = 1;
  pageSize: number = 10;
  pages: number[] = [];
  totalItems: number = 0;
  totalPages: number = 1;

  constructor(
    private toastr: ToastrService,
    private unitOfWork: UnitOfWorkServices
  ) {}

  ngOnInit(): void {
    this.loadCities();
    this.loadRegions();
  }

  // Load all cities with pagination
  loadCities(): void {
    this.unitOfWork.City.getAllWithPagination(this.PageNumber, this.pageSize).subscribe({
      next: (response: any) => {
        this.cities = response.data || response;
        this.totalItems = response.totalCount || 0;
        this.calculateTotalPages();
        this.updatePageNumbers();
      },
      error: (error) => {
        const message = this.getErrorMessage(error);
        this.toastr.error(message);
      },
    });
  }

  // Load all regions
  loadRegions(): void {
    this.unitOfWork.Region.getAll().subscribe({
      next: (data) => {
        this.regions = data.map((region) => ({
          id: region.id,
          governorate: region.governorate,
        }));
      },
      error: (error) => {
        const message = this.getErrorMessage(error);
        this.toastr.error(message);
      },
    });
  }

  // Open modal for creating a new city
  openCreateModal(): void {
    this.isEditMode = false;
    this.selectedCity = this.emptyCity();
    this.showModal = true;
  }

  // Open modal for editing an existing city
  openEditModal(city: City): void {
    this.isEditMode = true;
    this.selectedCity = { ...city };
    this.showModal = true;
  }

  // Close the modal
  closeModal(): void {
    this.showModal = false;
  }

  // Save city (create or update)
  saveCity(): void {
    if (this.isEditMode) {
      this.updateCity();
    } else {
      this.createCity();
    }
  }

  // Create a new city
  createCity(): void {
    this.unitOfWork.City.create(this.selectedCity).subscribe({
      next: () => {
        this.toastr.success('تم إضافة المدينة بنجاح');
        this.loadCities();
        this.closeModal();
      },
      error: (error) => {
        const message = this.getErrorMessage(error);
        this.toastr.error(message);
      },
    });
  }

  // Update an existing city
  updateCity(): void {
    this.unitOfWork.City.update(this.selectedCity.id!, this.selectedCity).subscribe({
      next: () => {
        this.toastr.success('تم تحديث المدينة بنجاح');
        this.loadCities();
        this.closeModal();
      },
      error: (error) => {
        const message = this.getErrorMessage(error);
        this.toastr.error(message);
      },
    });
  }

  // Confirm city deletion
  confirmDelete(id: number): void {
    this.deletingId = id;
  }

  // Cancel city deletion
  cancelDelete(): void {
    this.deletingId = null;
  }

  // Delete a city
  deleteCity(id: number): void {
    this.unitOfWork.City.delete(id).subscribe({
      next: () => {
        this.toastr.success('تم حذف المدينة بنجاح');
        this.loadCities();
        this.deletingId = null;
      },
      error: (error) => {
        const message = this.getErrorMessage(error);
        this.toastr.error(message);
        this.deletingId = null;
      },
    });
  }

  // Pagination methods
  private calculateTotalPages(): void {
    this.totalPages = Math.ceil(this.totalItems / this.pageSize);
    if (this.totalPages === 0) this.totalPages = 1;
  }

  private updatePageNumbers(): void {
    this.pages = [];
    const maxVisiblePages = 5;
    let start = Math.max(1, this.PageNumber - Math.floor(maxVisiblePages / 2));
    let end = Math.min(this.totalPages, start + maxVisiblePages - 1);

    if (end === this.totalPages) {
      start = Math.max(1, end - maxVisiblePages + 1);
    }

    for (let i = start; i <= end; i++) {
      this.pages.push(i);
    }
  }

  nextPage(): void {
    if (this.PageNumber < this.totalPages) {
      this.PageNumber++;
      this.loadCities();
    }
  }

  prevPage(): void {
    if (this.PageNumber > 1) {
      this.PageNumber--;
      this.loadCities();
    }
  }

  goToPage(page: number): void {
    if (page >= 1 && page <= this.totalPages && page !== this.PageNumber) {
      this.PageNumber = page;
      this.loadCities();
    }
  }

  onPageSizeChange(newSize: number): void {
    this.pageSize = newSize;
    this.PageNumber = 1;
    this.loadCities();
  }

  private getErrorMessage(error: any): string {
    return error.message || 'حدث خطأ غير معروف';
  }

  private emptyCity(): Partial<City> {
    return {
      id: 0,
      name: '',
      standardShippingCost: 0,
      pickupShippingCost: 0,
      createdAt: new Date().toISOString(),
      regionId: undefined,
      regionName: '',
    };
  }
}