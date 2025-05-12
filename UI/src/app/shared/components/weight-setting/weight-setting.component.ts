import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { WeightSetting } from '../../../models/WeightSetting.interface';
import { UnitOfWorkServices } from '../../../core/services/unitOfWork.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-weight-setting',
  templateUrl: './weight-setting.component.html',
  styleUrls: ['./weight-setting.component.css'],
  imports: [FormsModule, CommonModule],
  standalone: true,
})
export class WeightSettingComponent implements OnInit {
  weightSettings: WeightSetting[] = [];
  selectedWeightSetting: Partial<WeightSetting> = this.emptyWeightSetting();
  showModal = false;
  isEditMode = false;
  deletingId: number | null = null;

  // Pagination properties
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
    this.loadWeightSettings();
  }

  // Load all weight settings with pagination
  loadWeightSettings(): void {
    this.unitOfWork.WeightSetting.getAllWithPagination(this.PageNumber, this.pageSize).subscribe({
      next: (response: any) => {
        this.weightSettings = response.data || response;
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

  // Open modal for creating a new weight setting
  openCreateModal(): void {
    this.isEditMode = false;
    this.selectedWeightSetting = this.emptyWeightSetting();
    this.showModal = true;
  }

  // Open modal for editing an existing weight setting
  openEditModal(weightSetting: WeightSetting): void {
    this.isEditMode = true;
    this.selectedWeightSetting = { ...weightSetting };
    this.showModal = true;
  }

  // Close the modal
  closeModal(): void {
    this.showModal = false;
  }

  // Save weight setting (create or update)
  saveWeightSetting(): void {
    if (this.isEditMode) {
      this.updateWeightSetting();
    } else {
      this.createWeightSetting();
    }
  }

  // Create a new weight setting
  createWeightSetting(): void {
    this.unitOfWork.WeightSetting.create({
      ...this.selectedWeightSetting,
      id: this.selectedWeightSetting.id || 0,
    } as WeightSetting).subscribe({
      next: () => {
        this.toastr.success('تم إضافة إعداد الوزن بنجاح');
        this.loadWeightSettings();
        this.closeModal();
      },
      error: (error) => {
        const message = this.getErrorMessage(error);
        this.toastr.error(message);
      },
    });
  }

  // Update an existing weight setting
  updateWeightSetting(): void {
    this.unitOfWork.WeightSetting.update(
      this.selectedWeightSetting.id || 0,
      {
        ...this.selectedWeightSetting,
      } as WeightSetting
    ).subscribe({
      next: () => {
        this.toastr.success('تم تحديث إعداد الوزن بنجاح');
        this.loadWeightSettings();
        this.closeModal();
      },
      error: (error) => {
        const message = this.getErrorMessage(error);
        this.toastr.error(message);
      },
    });
  }

  // Confirm weight setting deletion
  confirmDelete(id: number): void {
    this.deletingId = id;
  }

  // Cancel weight setting deletion
  cancelDelete(): void {
    this.deletingId = null;
  }

  // Delete a weight setting
  deleteWeightSetting(id: number): void {
    this.unitOfWork.WeightSetting.delete(id).subscribe({
      next: () => {
        this.toastr.success('تم حذف إعداد الوزن بنجاح');
        this.loadWeightSettings();
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
      this.loadWeightSettings();
    }
  }

  prevPage(): void {
    if (this.PageNumber > 1) {
      this.PageNumber--;
      this.loadWeightSettings();
    }
  }

  goToPage(page: number): void {
    if (page >= 1 && page <= this.totalPages && page !== this.PageNumber) {
      this.PageNumber = page;
      this.loadWeightSettings();
    }
  }

  onPageSizeChange(newSize: number): void {
    this.pageSize = newSize;
    this.PageNumber = 1;
    this.loadWeightSettings();
  }

  private getErrorMessage(error: any): string {
    return error.message || 'حدث خطأ غير معروف';
  }

  private emptyWeightSetting(): Partial<WeightSetting> {
    return {
      id: 0,
      minWeight: 0,
      maxWeight: 0,
      costPerKg: 0,
      createdAt: new Date().toISOString(),
    };
  }
}