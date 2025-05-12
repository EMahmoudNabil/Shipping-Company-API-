import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Branch } from '../../../models/Branch.Interface';
import { UnitOfWorkServices } from '../../../core/services/unitOfWork.service';
import { ToastrService } from 'ngx-toastr';
import { Region } from '../../../models/Region.Interface ';

@Component({
  selector: 'app-branch',
  imports: [FormsModule, CommonModule],
  templateUrl: './branch.component.html',
  styleUrl: './branch.component.css',
  standalone: true
})
export class BranchComponent implements OnInit {
  branches: Branch[] = [];
  regions: Region[] = [];
  selectedBranch: Branch = this.emptyBranch();
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
    this.loadBranches();
    this.loadRegions();
  }

  loadBranches(): void {
    this._UnitOfWorkServices.Branch.getAllWithPagination(this.PageNumber, this.pageSize).subscribe({
      next: (response: any) => {
        // Assuming the response has pagination metadata
        this.branches = response.data || response;
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
      this.loadBranches();
    }
  }
  
  prevPage(): void {
    if (this.PageNumber > 1) {
      this.PageNumber--;
      this.loadBranches();
    }
  }
  
  goToPage(page: number): void {
    if (page >= 1 && page <= this.totalPages && page !== this.PageNumber) {
      this.PageNumber = page;
      this.loadBranches();
    }
  }
  
  onPageSizeChange(newSize: number): void {
    this.pageSize = newSize;
    this.PageNumber = 1; // Reset to first page when changing page size
    this.loadBranches();
  }
  
  getErrorMessage(error: any): string {
    return error.message || 'حدث خطأ غير معروف';
  }
  
  private emptyBranch(): Branch {
    return {
      id: 0,
      name: '',
      location: '',
      isDeleted: false,
      branchDate: new Date().toISOString(),
      regionName: '',
      regionId: 0,
    };
  }

  // Open modal for creating a new branch
  openCreateModal(): void {
    this.isEditMode = false;
    this.selectedBranch = this.emptyBranch();
    this.showModal = true;
  }

  // Open modal for editing an existing branch
  openEditModal(branch: Branch): void {
    this.isEditMode = true;
    this.selectedBranch = { ...branch };
    
    // If regionId is not set, try to find the corresponding region
    if (!this.selectedBranch.regionId && this.selectedBranch.regionName && this.regions.length > 0) {
      const matchingRegion = this.regions.find(r => r.governorate === this.selectedBranch.regionName);
      if (matchingRegion) {
        this.selectedBranch.regionId = matchingRegion.id;
      }
    }
    
    this.showModal = true;
  }

  // Close the modal
  closeModal(): void {
    this.showModal = false;
  }

  // Save branch (create or update)
  saveBranch(): void {
    if (this.isEditMode) {
      this.updateBranch();
    } else {
      this.createBranch();
    }
  }

  // Create a new branch
  createBranch(): void {
    // Set the regionName from the selected regionId
    if (this.selectedBranch.regionId) {
      const selectedRegion = this.regions.find(r => r.id === this.selectedBranch.regionId);
      if (selectedRegion) {
        this.selectedBranch.regionName = selectedRegion.governorate;
      }
    }
    
    this._UnitOfWorkServices.Branch.create(this.selectedBranch).subscribe({
      next: () => {
        this.toastr.success('تم إضافة الفرع بنجاح');
        this.loadBranches();
        this.closeModal();
      },
      error: (error) => {
        const message = this.getErrorMessage(error);
        this.toastr.error(message);
      }
    });
  }

  // Update an existing branch
  updateBranch(): void {
    // Set the regionName from the selected regionId
    if (this.selectedBranch.regionId) {
      const selectedRegion = this.regions.find(r => r.id === this.selectedBranch.regionId);
      if (selectedRegion) {
        this.selectedBranch.regionName = selectedRegion.governorate;
      }
    }
    
    this._UnitOfWorkServices.Branch.update(this.selectedBranch.id, this.selectedBranch).subscribe({
      next: () => {
        this.toastr.success('تم تحديث الفرع بنجاح');
        this.loadBranches();
        this.closeModal();
      },
      error: (error) => {
        const message = this.getErrorMessage(error);
        this.toastr.error(message);
      }
    });
  }

  // Confirm branch deletion
  confirmDelete(id: number): void {
    this.deletingId = id;
  }

  // Cancel branch deletion
  cancelDelete(): void {
    this.deletingId = null;
  }

  // Delete a branch
  deleteBranch(id: number): void {
    this._UnitOfWorkServices.Branch.delete(id).subscribe({
      next: () => {
        this.toastr.success('تم حذف الفرع بنجاح');
        this.loadBranches();
        this.deletingId = null;
      },
      error: (error) => {
        const message = this.getErrorMessage(error);
        this.toastr.error(message);
        this.deletingId = null;
      }
    });
  }

  // Toggle branch active status
  toggleBranchStatus(branch: Branch): void {
    const updatedBranch = { ...branch, isDeleted: !branch.isDeleted };
    
    this._UnitOfWorkServices.Branch.update(branch.id, updatedBranch).subscribe({
      next: () => {
        const statusMessage = updatedBranch.isDeleted ? 'تم تعطيل الفرع بنجاح' : 'تم تفعيل الفرع بنجاح';
        this.toastr.success(statusMessage);
        this.loadBranches();
      },
      error: (error) => {
        const message = this.getErrorMessage(error);
        this.toastr.error(message);
      }
    });
  }
}