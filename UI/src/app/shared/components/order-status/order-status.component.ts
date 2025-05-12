import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UnitOfWorkServices } from '../../../core/services/unitOfWork.service';
import { PageHeaderComponent } from "../page-header/page-header.component";
import { RouterModule } from '@angular/router';
import { OrderStatus } from '../../../models/OrderStatus.Interface';
import { OrderWithProductsDto } from '../../../models/order.interface';
import { catchError, finalize, tap, switchMap } from 'rxjs/operators';
import { of } from 'rxjs';
import { CourierService } from '../../../core/services/Courier.Service';
import { CourierDTO } from '../../../models/Courier.interface';
// Material Imports
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatChipsModule } from '@angular/material/chips';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatDividerModule } from '@angular/material/divider';
import { DeleteConfirmDialogComponent } from '../delete-confirm-modal.component/delete-confirm-modal.component';
import { MatDialog } from '@angular/material/dialog';


@Component({
  selector: 'app-order-status',
  templateUrl: './order-status.component.html',
  styleUrls: ['./order-status.component.css'],
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule,MatIconModule,    // Material Modules
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatInputModule,
    MatFormFieldModule,
    MatSelectModule,
    MatTableModule,
    MatCheckboxModule,
    MatChipsModule,
    MatPaginatorModule,
    MatProgressSpinnerModule,
    MatTooltipModule,
    MatDividerModule,
    MatFormFieldModule ,MatProgressSpinnerModule, FormsModule, RouterModule, PageHeaderComponent]
})
export class OrderStatusComponent implements OnInit {
  orders: OrderWithProductsDto[] = [];
  loading = false;
  currentPage = 1;
  pageSize = 10;
  totalItems = 0;
  totalPages = 0;
  selectedOrder: OrderWithProductsDto | null = null;
  newStatus: OrderStatus | null = null;
  currentStatus: OrderStatus | null = null;
  searchTerm = '';
  availableCouriers: CourierDTO[] = [];
  selectedCourierId: string | null = null;
  
  // Make OrderStatus enum available to the template
  OrderStatus = OrderStatus;
  
  statusOptions = [
    { value: OrderStatus.Pending, label: 'قيد الانتظار' },
    { value: OrderStatus.WaitingForConfirmation, label: 'في انتظار التأكيد' },
    { value: OrderStatus.InProgress, label: 'قيد التنفيذ' },
    { value: OrderStatus.Delivered, label: 'تم التسليم' },
    { value: OrderStatus.DeliveredToCourier, label: 'تم التسليم للمندوب' },
    { value: OrderStatus.Declined, label: 'مرفوض' },
    { value: OrderStatus.UnreachableCustomer, label: 'لا يمكن الوصول' },
    { value: OrderStatus.PartialDelivery, label: 'تم التسليم جزئياً' },
    { value: OrderStatus.CanceledByRecipient, label: 'تم الإلغاء من المستلم' },
    { value: OrderStatus.DeclinedWithPartialPayment, label: 'مرفوض مع دفع جزئي' },
    { value: OrderStatus.DeclinedWithFullPayment, label: 'مرفوض مع دفع كامل' }
  ];
  
  showCourierModal = false;
  isAssigning = false;

  constructor(
    private fb: FormBuilder,
    private _unitOfWork: UnitOfWorkServices,
    private toastr: ToastrService,
    private courierService: CourierService,
    private dialog: MatDialog  ) {}

  ngOnInit(): void {
    this.loadOrders();
  }

  loadOrders(): void {
    if (this.loading) return;
    
    this.loading = true;
    
    const params = {
      PageNumber: this.currentPage.toString(),
      PageSize: this.pageSize.toString(),
      SearchTerm: this.searchTerm?.trim() || ''
    };
  
    this._unitOfWork.Order.getOrdersByStatus(this.currentStatus, params)
      .pipe(
        tap(response => {
          if (response) {
            this.orders = response.items || [];
            this.totalItems = response.totalCount || 0;
            this.totalPages = response.totalPages || Math.ceil(this.totalItems / this.pageSize);
            
            console.log('Pagination data:', {
              currentPage: this.currentPage,
              pageSize: this.pageSize,
              totalItems: this.totalItems,
              totalPages: this.totalPages
            });
          }
        }),
        catchError(error => {
          console.error('Error loading orders:', error);
          this.toastr.error(this.getErrorMessage(error));
          return of(null);
        }),
        finalize(() => this.loading = false)
      )
      .subscribe();
  }

  private getErrorMessage(error: any): string {
    if (error.error?.errors) {
      return Object.values(error.error.errors).join(', ');
    }
    return error.message || 'فشل في تحميل الطلبات';
  }

  filterByStatus(status: OrderStatus | null): void {
    this.currentStatus = status;
    this.currentPage = 1; // Reset to first page
    this.loadOrders();
  }

  onSearch(): void {
    // Reset page when searching
    this.currentPage = 1;
    // Trim search term
    this.searchTerm = this.searchTerm?.trim();
    this.loadOrders();
  }

  onPageChange(page: number): void {
    // Validate the page number
    if (page >= 1 && page <= this.totalPages && page !== this.currentPage) {
      this.currentPage = page;
      this.loadOrders();
    }
  }

  nextPage(): void {
    // Fix for next page navigation
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
      this.loadOrders();
    }
  }

  prevPage(): void {
    // Fix for previous page navigation
    if (this.currentPage > 1) {
      this.currentPage--;
      this.loadOrders();
    }
  }

  onPageSizeChange(event: any): void {
    const newSize = parseInt(event.target.value, 10);
    if (newSize !== this.pageSize) {
      this.pageSize = newSize;
      this.currentPage = 1; // Reset to first page when changing page size
      
      // Add delay to ensure DOM updates before API call
      setTimeout(() => {
        this.loadOrders();
      }, 0);
    }
  }

  getStatusLabel(status: string | OrderStatus): string {
    // Handle both string and enum types
    let statusValue: OrderStatus;
    
    if (typeof status === 'string') {
      // Convert string status to enum value
      statusValue = this.convertStatusToEnum(status);
    } else {
      statusValue = status;
    }
    
    const option = this.statusOptions.find(opt => opt.value === statusValue);
    return option ? option.label : 'غير معروف';
  }
  
  getPages(): number[] {
    const pages: number[] = [];
    const totalPages = this.totalPages;
    const currentPage = this.currentPage;
    const maxVisiblePages = 5; // عدد الصفحات المرئية حول الصفحة الحالية
  
    if (totalPages <= maxVisiblePages) {
      // إذا كان العدد الإجمالي للصفحات أقل من الحد الأقصى
      for (let i = 1; i <= totalPages; i++) {
        pages.push(i);
      }
    } else {
      // إضافة الصفحة الأولى
      pages.push(1);
  
      // حساب بداية ونهاية الصفحات المرئية
      let start = Math.max(2, currentPage - Math.floor(maxVisiblePages / 2));
      let end = start + maxVisiblePages - 3;
  
      if (end >= totalPages - 1) {
        end = totalPages - 1;
        start = Math.max(2, end - maxVisiblePages + 3);
      }
  
      // إضافة النقاط (...) إذا لزم الأمر
      if (start > 2) {
        pages.push(-1); // استخدم -1 لتمثيل النقاط (...)
      }
  
      // إضافة الصفحات المرئية
      for (let i = start; i <= end; i++) {
        pages.push(i);
      }
  
      // إضافة النقاط (...) إذا لزم الأمر
      if (end < totalPages - 1) {
        pages.push(-1);
      }
  
      // إضافة الصفحة الأخيرة
      pages.push(totalPages);
    }
  
    return pages;
  }
  extractCustomerInfo(order: any, type: 'name' | 'phone'): string {
    if (!order.customerInfo) return 'غير محدد';
    
    const parts = order.customerInfo.split(' ');
    if (parts.length < 2) return 'غير محدد';

    return type === 'name' ? parts[0] : parts[1];
  }

  getDisplayValue(value: any, defaultValue: string = 'غير محدد'): string {
    return value || defaultValue;
  }

  // Convert string status to enum
  private convertStatusToEnum(status: string): OrderStatus {
    switch(status) {
      case 'Pending': return OrderStatus.Pending;
      case 'WaitingForConfirmation': return OrderStatus.WaitingForConfirmation;
      case 'InProgress': return OrderStatus.InProgress;
      case 'Delivered': return OrderStatus.Delivered;
      case 'Declined': return OrderStatus.Declined;
      case 'DeliveredToCourier': return OrderStatus.DeliveredToCourier;
      case 'UnreachableCustomer': return OrderStatus.UnreachableCustomer;
      case 'PartialDelivery': return OrderStatus.PartialDelivery;
      case 'CanceledByRecipient': return OrderStatus.CanceledByRecipient;
      case 'DeclinedWithPartialPayment': return OrderStatus.DeclinedWithPartialPayment;
      case 'DeclinedWithFullPayment': return OrderStatus.DeclinedWithFullPayment;
      default: return OrderStatus.Pending;
    }
  }

  getStatusClass(status: OrderStatus): string {
    switch(status) {
      case OrderStatus.Pending: return 'badge btn-pending';
      case OrderStatus.WaitingForConfirmation: return 'badge btn-waiting';
      case OrderStatus.InProgress: return 'badge btn-in-progress';
      case OrderStatus.Delivered: return 'badge btn-delivered';
      case OrderStatus.Declined: return 'badge btn-declined';
      case OrderStatus.DeliveredToCourier: return 'badge btn-delivered-courier';
      case OrderStatus.UnreachableCustomer: return 'badge btn-unreachable';
      case OrderStatus.PartialDelivery: return 'badge btn-partial';
      case OrderStatus.CanceledByRecipient: return 'badge btn-canceled';
      case OrderStatus.DeclinedWithPartialPayment: return 'badge btn-declined-partial';
      case OrderStatus.DeclinedWithFullPayment: return 'badge btn-declined-full';
      default: return 'badge btn-secondary';
    }
  }

  getStatusButtonClass(status: OrderStatus): string {
    if (this.selectedOrder?.status === status) {
      return 'btn-secondary disabled';
    }
    if (this.newStatus === status) {
      return this.getStatusClass(status);
    }
    return 'btn-outline-secondary';
  }

  selectStatus(status: OrderStatus): void {
    // Update selected status
    if (status !== this.selectedOrder?.status) {
      this.newStatus = status;
    }
  }

  openStatusModal(order: OrderWithProductsDto): void {
    this.selectedOrder = {...order}; // Create a copy to avoid direct mutation
    this.newStatus = order.status; // Set initial status to current status
  }

  closeStatusModal(): void {
    this.selectedOrder = null;
    this.newStatus = null;
  }

  updateOrderStatus(): void {
    if (!this.selectedOrder || this.newStatus === null) {
      this.toastr.error('الرجاء اختيار حالة جديدة');
      return;
    }
  
    if (this.selectedOrder.status === this.newStatus) {
      this.toastr.info('لم يتم تغيير الحالة');
      this.closeStatusModal();
      return;
    }
  
    this.loading = true;
  
    // Update the payload to match what the API expects
    const orderId = this.selectedOrder.id;
    const newStatus = this.newStatus;
  
    this._unitOfWork.Order.updateOrderStatus(orderId, newStatus)
      .pipe(
        tap(() => {
          this.toastr.success('تم تحديث حالة الطلب بنجاح');
          
          // Update the local array with the new status
          const index = this.orders.findIndex(o => o.id === orderId);
          if (index !== -1) {
            this.orders[index] = {
              ...this.orders[index],
              status: newStatus
            };
          }
          
          this.closeStatusModal();
          
          // Optional: If you want to ensure complete synchronization with the server
          // Uncomment the next line:
          this.loadOrders(); 
        }),
        catchError(error => {
          console.error('Update status error:', error);
          this.toastr.error(this.getErrorMessage(error) || 'فشل في تحديث حالة الطلب');
          return of(null);
        }),
        finalize(() => {
          this.loading = false;
        })
      )
      .subscribe();
  }

  selectAllOrders(event: any): void {
    const checked = event.target.checked;
    this.orders.forEach(order => order.isDeleted = checked);
  }

  exportToPDF(): void {
    // Implement PDF export functionality
    this.toastr.info('جاري تصدير PDF...');
  }

  exportToExcel(): void {
    // Implement Excel export functionality
    this.toastr.info('جاري تصدير Excel...');
  }

  // deleteOrder(id: number): void {
  //   if (confirm('هل أنت متأكد من رغبتك في حذف هذا الطلب؟')) {
  //     this.loading = true;
      
  //     this._unitOfWork.Order.deleteOrder(id)
  //       .pipe(
  //         tap(() => {
  //           this.toastr.success('تم حذف الطلب بنجاح');
  //           this.loadOrders(); // Reload orders after deletion
  //         }),
  //         catchError(error => {
  //           console.error('Failed to delete order:', error);
  //           this.toastr.error(this.getErrorMessage(error) || 'فشل في حذف الطلب');
  //           return of(null); // Return an observable with null to continue the stream
  //         }),
  //         finalize(() => this.loading = false)
  //       )
  //       .subscribe();
  //   }
  // }
  deleteOrder(id: number): void {
    const dialogRef = this.dialog.open(DeleteConfirmDialogComponent, {
      width: '450px',
      direction: 'rtl',
      disableClose: true,
      data: {
        title: 'حذف الطلب',
        message: 'هل أنت متأكد من رغبتك في حذف هذا الطلب؟',
        itemId: id
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.executeDelete(id);
      }
    });
  }

  private executeDelete(id: number): void {
    this.loading = true;
    this._unitOfWork.Order.deleteOrder(id)
      .pipe(
        tap(() => {
          this.toastr.success(`تم حذف الطلب #${id} بنجاح`);
          this.handlePostDeletion();
        }),
        catchError(error => {
          this.toastr.error(`فشل في حذف الطلب #${id}`, error.message || 'خطأ');
          return of(null);
        }),
        finalize(() => this.loading = false)
      )
      .subscribe();
  }

  private handlePostDeletion(): void {
    // إذا كانت الصفحة الأخيرة وتم حذف العنصر الوحيد
    if (this.orders.length === 1 && this.currentPage > 1) {
      this.currentPage--;
    }
    
    // إعادة تحميل البيانات
    this.loadOrders();
    
    // بديل: حذف العنصر من المصفوفة مباشرة بدون إعادة تحميل
    // this.orders = this.orders.filter(order => order.id !== id);
    // this.totalItems--;
  }

 
  printOrder(id: number): void {
    // Implement print functionality
    this.toastr.info('جاري تحضير الطباعة...');
    // You would typically open a new window or generate a print view here
  }

  canAssignCourier(order: OrderWithProductsDto): boolean {
    return Boolean(order) && 
           order.status === OrderStatus.Pending && 
           !order.courierId;
  }

  // Add a helper method to get courier status text
  getCourierStatusText(order: OrderWithProductsDto): string {
    if (order.courierId) {
      return 'تم التعيين';
    }
    if (order.status !== OrderStatus.Pending) {
      return 'غير متاح';
    }
    return 'تعيين مندوب';
  }

  assignCourier(order: OrderWithProductsDto): void {
    if (!this.selectedCourierId || !order) {
      this.toastr.error('الرجاء اختيار مندوب');
      return;
    }
  
    this.isAssigning = true;
    const courierId = this.selectedCourierId.toString();
    
    // Find the courier name before making the API call
    const selectedCourier = this.availableCouriers.find(c => c.id === courierId);
    
    this._unitOfWork.Order.assignOrderToCourier(order.id, courierId)
      .pipe(
        switchMap(() => {
          return this._unitOfWork.Order.updateOrderStatus(order.id, OrderStatus.DeliveredToCourier);
        }),
        tap(() => {
          this.toastr.success('تم تعيين المندوب وتحديث حالة الطلب بنجاح');
          const index = this.orders.findIndex(o => o.id === order.id);
          if (index !== -1) {
            this.orders[index] = {
              ...this.orders[index],
              courierId: courierId,
              CourierName: selectedCourier?.courierName || null,
              status: OrderStatus.DeliveredToCourier
            };
          }
          this.closeCourierModal();
          this.loadOrders();
        }),
        // ...existing error handling...
      )
      .subscribe();
  }

  openCourierModal(order: OrderWithProductsDto): void {
    this.selectedOrder = order;
    this.showCourierModal = true;
    this.selectedCourierId = null;
    this.loadAvailableCouriers(order.id);
  }

  closeCourierModal(): void {
    this.showCourierModal = false;
    this.selectedOrder = null;
    this.selectedCourierId = null;
    this.availableCouriers = [];
  }

  private loadAvailableCouriers(orderId: number): void {
    this.isAssigning = true;
    this.courierService.getCouriersByBranch(orderId)
      .pipe(
        tap(couriers => {
          this.availableCouriers = couriers;
          if (couriers.length === 0) {
            this.toastr.warning('لا يوجد مندوبين متاحين لهذا الفرع');
          }
        }),
        catchError(error => {
          console.error('Failed to load couriers:', error);
          this.toastr.error('فشل في تحميل قائمة المندوبين');
          return of([]);
        }),
        finalize(() => {
          this.isAssigning = false;
        })
      )
      .subscribe();
  }
}