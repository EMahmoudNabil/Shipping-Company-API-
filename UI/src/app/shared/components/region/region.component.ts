import { Component, OnInit } from '@angular/core';
import { RegionService } from '../../../core/services/region.service';
import { Region } from '../../../models/Region.Interface ';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { NavbarComponent } from "../navbar/navbar.component";
import { SideNavComponent } from "../side-nav/side-nav.component";
import { UnitOfWorkServices } from '../../../core/services/unitOfWork.service';



@Component({
  selector: 'app-region',
  templateUrl: './region.component.html',
  styleUrls: ['./region.component.css'],
  imports: [FormsModule, CommonModule],
})
export class RegionComponent implements OnInit {
  regions: Region[] = [];
  selectedRegion: Region = this.emptyRegion();
  showModal = false;
  isEditMode = false;
  deletingId: number | null = null;

  // Pagination properties
   PageNumber: number = 1;
   pageSize: number = 10;
   pages: number[] = [];
   hasNextPage: boolean = false;
  totalItems: number = 0;
  totalPages: number = 1;

  constructor(
    private toastr: ToastrService,
    private _UnitOfWorkServices: UnitOfWorkServices,
    
  ) {}

  ngOnInit(): void {
    this.loadRegions();
  }




  loadRegions(): void {
    this._UnitOfWorkServices.Region.getAllWithPagination(this.PageNumber, this.pageSize)
      .subscribe({
        next: (regions) => {
          this.regions = regions;
          this.calculateTotalPages();
          this.updatePageNumbers();
        }
      });
  }


  // Pagination methods
  private calculateTotalPages(): void {
  
    this.totalPages = Math.ceil(this.totalItems / this.pageSize);
  }

  updatePageNumbers(): void {
    this.pages = [];
    
    const maxVisiblePages = 5; 
    let start = Math.max(1, this.PageNumber - Math.floor(maxVisiblePages / 2));
    let end = start + maxVisiblePages - 1;
  
    if (this.hasNextPage && end < this.PageNumber + 2) {
      end = this.PageNumber + 2;
    }
  
    start = Math.max(1, start);
    
    for (let i = start; i <= end; i++) {
      this.pages.push(i);
    }
  }
  nextPage(): void {
    this.PageNumber++;
    this.loadRegions();
  }
  
  prevPage(): void {
    this.PageNumber = Math.max(1, this.PageNumber - 1);
    this.loadRegions();
  }
  
  onPageSizeChange(newSize: number): void {
    this.pageSize = newSize;
    this.PageNumber = 1;
    this.loadRegions();
  }



  //modal
  openCreateModal(): void {
    this.isEditMode = false;
    this.selectedRegion = this.emptyRegion();
    this.showModal = true;
  }

  openEditModal(region: Region): void {
    this.isEditMode = true;
    this.selectedRegion = { ...region };
    this.showModal = true;
  }

  closeModal(): void {
    this.showModal = false;
  }

  handleSubmit(): void {
    if (this.isEditMode) {
      this._UnitOfWorkServices.Region.update(this.selectedRegion.id, this.selectedRegion)
        .subscribe({
          next: () => {
            this.toastr.success('تم تحديث المنطقة بنجاح', 'نجاح');
            this.loadRegions();
            this.closeModal();
          },
          error: (err) => {
            this.toastr.error('فشل في تحديث المنطقة', 'خطأ');
            console.error('Update error:', err);
          }
        });
    } else {
      this._UnitOfWorkServices.Region.create(this.selectedRegion)
        .subscribe({
          next: () => {
            this.toastr.success('تم إضافة المنطقة بنجاح', 'نجاح');
            this.loadRegions();
            this.closeModal();
          },
          error: (err) => {
            this.toastr.error('فشل في إضافة المنطقة', 'خطأ');
            console.error('Create error:', err);
          }
        });
    }}



  private emptyRegion(): Region {
    return {
      id: 0,
      governorate: '',
      isDeleted: false,
      createdAt: new Date().toISOString()
    };
  }





  updateRegionStatus(region: Region) {
    const originalStatus = region.isDeleted;
    
    // Toggle the status immediately for better UX
    region.isDeleted = !region.isDeleted;
  
    this._UnitOfWorkServices.Region.update(region.id, region).subscribe({
      error: (err:any) => {
        // Revert on error
        region.isDeleted = originalStatus;
        console.error('Failed to update status:', err);
        this.toastr.error('فشل في تحديث الحالة', 'خطأ');
      }
    });
  }

}