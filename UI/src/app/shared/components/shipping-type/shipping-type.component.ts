import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ShippingType } from '../../../models/ShippingType.Interface';
import { UnitOfWorkServices } from '../../../core/services/unitOfWork.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-shipping-type',
  imports: [FormsModule, CommonModule],
  templateUrl: './shipping-type.component.html',
  styleUrl: './shipping-type.component.css',
  standalone: true
})
export class ShippingTypeComponent implements OnInit {
  shippingTypes: ShippingType[] = [];
  selectedShippingType: ShippingType = this.emptyShippingType();
  showModal = false;
  isEditMode = false;
  deletingId: number | null = null;
  PageNumber: number = 1;
  pageSize: number = 10;
  pages: number[] = [];
  hasNextPage: boolean = false;
  totalItems: number = 0;
  totalPages: number = 1;

  constructor(
    private toastr: ToastrService,
    private _UnitOfWorkServices: UnitOfWorkServices,
  ) { }

  ngOnInit() {
    this.loadShippingTypes();
  }

  loadShippingTypes(): void {
    this._UnitOfWorkServices.ShippingType.getAllWithPagination(this.PageNumber, this.pageSize).subscribe({
      next: (response: any) => {
        // Assuming the response has pagination metadata
        this.shippingTypes = response.data || response;
        this.totalItems = response.totalCount || 0;
        this.hasNextPage = response.hasNextPage || false;
        
        this.calculateTotalPages();
        this.updatePageNumbers();
      },
      error: (error) => {
        const message = this.getErrorMessage(error);
        this.toastr.error(message);
      }
    });
  }
  
  private calculateTotalPages(): void {
    this.totalPages = Math.ceil(this.totalItems / this.pageSize);
    if (this.totalPages === 0) this.totalPages = 1;
  }
  
  updatePageNumbers(): void {
    this.pages = [];
    
    const maxVisiblePages = 5; 
    let start = Math.max(1, this.PageNumber - Math.floor(maxVisiblePages / 2));
    let end = Math.min(this.totalPages, start + maxVisiblePages - 1);
  
    // Adjust start if end is at maximum
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
      this.loadShippingTypes();
    }
  }
  
  prevPage(): void {
    if (this.PageNumber > 1) {
      this.PageNumber--;
      this.loadShippingTypes();
    }
  }
  
  goToPage(page: number): void {
    if (page >= 1 && page <= this.totalPages && page !== this.PageNumber) {
      this.PageNumber = page;
      this.loadShippingTypes();
    }
  }
  
  onPageSizeChange(newSize: number): void {
    this.pageSize = newSize;
    this.PageNumber = 1; // Reset to first page when changing page size
    this.loadShippingTypes();
  }
  
  getErrorMessage(error: any): string {
    return error.message || 'حدث خطأ غير معروف';
  }
  
  private emptyShippingType(): ShippingType {
    return {
      id: 0,
      name: '',
      baseCost: 0,
      duration: 0,
      createdAt: new Date().toISOString()
    };
  }

  // Open modal for creating a new shipping type
  openCreateModal(): void {
    this.isEditMode = false;
    this.selectedShippingType = this.emptyShippingType();
    this.showModal = true;
  }

  // Open modal for editing an existing shipping type
  openEditModal(shippingType: ShippingType): void {
    this.isEditMode = true;
    this.selectedShippingType = { ...shippingType };
    this.showModal = true;
  }

  // Close the modal
  closeModal(): void {
    this.showModal = false;
  }

  // Save shipping type (create or update)
  saveShippingType(): void {
    if (this.isEditMode) {
      this.updateShippingType();
    } else {
      this.createShippingType();
    }
  }

  // Create a new shipping type
  createShippingType(): void {
    this._UnitOfWorkServices.ShippingType.create(this.selectedShippingType).subscribe({
      next: () => {
        this.toastr.success('تم إضافة نوع الشحن بنجاح');
        this.loadShippingTypes();
        this.closeModal();
      },
      error: (error) => {
        const message = this.getErrorMessage(error);
        this.toastr.error(message);
      }
    });
  }

  // Update an existing shipping type
  updateShippingType(): void {
    this._UnitOfWorkServices.ShippingType.update(this.selectedShippingType.id, this.selectedShippingType).subscribe({
      next: () => {
        this.toastr.success('تم تحديث نوع الشحن بنجاح');
        this.loadShippingTypes();
        this.closeModal();
      },
      error: (error) => {
        const message = this.getErrorMessage(error);
        this.toastr.error(message);
      }
    });
  }

  // Confirm shipping type deletion
  confirmDelete(id: number): void {
    this.deletingId = id;
  }

  // Cancel shipping type deletion
  cancelDelete(): void {
    this.deletingId = null;
  }

  // Delete a shipping type
  deleteShippingType(id: number): void {
    this._UnitOfWorkServices.ShippingType.delete(id).subscribe({
      next: () => {
        this.toastr.success('تم حذف نوع الشحن بنجاح');
        this.loadShippingTypes();
        this.deletingId = null;
      },
      error: (error) => {
        const message = this.getErrorMessage(error);
        this.toastr.error(message);
        this.deletingId = null;
      }
    });
  }
}