import { Component, OnInit } from '@angular/core';
import { OrderReportService } from '../../../core/services/order-report.service';
import { ToastrService } from 'ngx-toastr';
import { OrderReport } from '../../../models/OrderReport.Interface';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ChangeDetectorRef } from '@angular/core';
import { OrderStatus } from '../../../models/OrderStatus.Interface';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import { MatTableDataSource } from '@angular/material/table';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { PageHeaderComponent } from "../page-header/page-header.component";
import { RouterModule } from '@angular/router';
@Component({
  selector: 'app-order-report',
  templateUrl: './order-report.component.html',
  styleUrls: ['./order-report.component.css'],
  imports: [FormsModule, CommonModule,RouterModule, MatButtonModule,
    MatCardModule, MatCheckboxModule, MatFormFieldModule,
    MatInputModule, MatPaginatorModule, MatProgressSpinnerModule,
    MatSelectModule, MatTableModule, MatTooltipModule, MatChipsModule,
    MatDatepickerModule, MatNativeDateModule, MatButtonModule,
    MatIconModule, PageHeaderComponent],
  standalone: true,
})
export class OrderReportComponent implements OnInit {
  orderReports: OrderReport[] = []; // Initialize as an empty array
  OrderStatus = OrderStatus;
  orderStatuses = [
    { value: 'Pending', label: 'قيد الانتظار' },
    { value: 'WaitingForConfirmation', label: 'في انتظار التأكيد' },
    { value: 'InProgress', label: 'قيد التنفيذ' },
    { value: 'Delivered', label: 'تم التسليم' },
    { value: 'DeliveredToCourier', label: 'تم التسليم للمندوب' },
    { value: 'Declined', label: 'مرفوض' },
    { value: 'UnreachableCustomer', label: 'عميل غير قابل للوصول' },
    { value: 'PartialDelivery', label: 'تسليم جزئي' },
    { value: 'CanceledByRecipient', label: 'تم الإلغاء من المستلم' },
    { value: 'DeclinedWithPartialPayment', label: 'مرفوض مع دفع جزئي' },
    { value: 'DeclinedWithFullPayment', label: 'مرفوض مع دفع كامل' }
  ];
  filters = {
    orderStatus: '',
    startDate: '',
    endDate: '',
  };
  pageNumber = 1;
  pageSize = 10;
  totalPages = 1;
  loading: boolean = false;
  error: boolean = false;
  totalItems: number = 0;
  displayedColumns: string[] = [
    'id',
    'status',
    'merchant',
    'customer',
    'phone',
    'region',
    'city',
    'orderCost',
    'amountReceived',
    'shippingCost',
    'shippingCostPaid',
    'companyValue',
    'date'
  ];

  constructor(
    private orderReportService: OrderReportService,
    private toastr: ToastrService,
  ) {}
  getStatusLabel(status: number): string {
    const statusKey = OrderStatus[status] as keyof typeof OrderStatus;
    const statusObj = this.orderStatuses.find(s => s.value === statusKey);
    return statusObj ? statusObj.label : 'حالة غير معروفة';
  }

    // دالة مساعدة للحصول على كلاس الحالة
    private statusColorMap = new Map<string, string>([
      ['WaitingForConfirmation', 'bg-warning text-dark'],
      ['Pending', 'bg-info text-dark'],
      ['InProgress', 'bg-primary text-white'],
      ['Delivered', 'bg-success text-white'],
      ['DeliveredToCourier', 'bg-primary text-white'],
      ['Declined', 'bg-secondary text-white'],
      ['UnreachableCustomer', 'bg-dark text-white'],
      ['PartialDelivery', 'bg-warning text-dark'],
      ['CanceledByRecipient', 'bg-danger text-white'],
      ['DeclinedWithPartialPayment', 'bg-danger text-white'],
      ['DeclinedWithFullPayment', 'bg-danger text-white']
    ]);
    
    getStatusClass(status: number): any {
      const statusKey = OrderStatus[status] as keyof typeof OrderStatus;
      const classes = this.statusColorMap.get(statusKey) || 'bg-light text-dark';
      return {[classes]: true};
    }
  sortReportsByDate(order: 'asc' | 'desc'): void {
    this.orderReports.sort((a, b) => {
      const dateA = new Date(a.reportDate).getTime();
      const dateB = new Date(b.reportDate).getTime();
  
      if (order === 'asc') {
        return dateA - dateB; // Ascending order
      } else {
        return dateB - dateA; // Descending order
      }
    });
  }

  ngOnInit(): void {
    this.loadReports();
  }
// Add this method to resolve the error
getStatusColor(orderStatus: string): string {
  switch (orderStatus) {
    case 'pending':
      return 'warn';
    case 'completed':
      return 'primary';
    case 'cancelled':
      return 'accent';
    default:
      return '';
  }
}
// Add the missing method in the component class
onPageChange(pageIndex: number): void {
  this.pageNumber = pageIndex;
  this.loadReports(); // Ensure this method exists to fetch data for the new page
}

  loadReports(): void {
    this.loading = true;
    this.error = false;
    
    this.orderReportService
      .getOrderReports(this.pageNumber, this.pageSize, this.filters)
      .subscribe({
        next: (response) => {
          this.orderReports = Array.isArray(response) ? response : [];
          this.totalPages = this.calculateTotalPages(this.orderReports.length);
          this.loading = false;
          this.filterAndSortReports();
          console.log(this.orderReports);
        },
        error: (error) => {
          console.error('API Error:', error);
          this.error = true;
          this.loading = false;
          this.toastr.error('فشل تحميل التقارير');
        },
      });
  }

  // Helper method to calculate total pages if not provided by API
  calculateTotalPages(totalItems: number): number {
    return Math.ceil(totalItems / this.pageSize) || 1;
  }

  searchReports(): void {
    this.pageNumber = 1;
    this.loadReports();
  }

  prevPage(): void {
    if (this.pageNumber > 1) {
      this.pageNumber--;
      this.loadReports();
    }
  }

  nextPage(): void {
    if (this.pageNumber < this.totalPages) {
      this.pageNumber++;
      this.loadReports();
    }
  }

  filterAndSortReports(): void {
    const startDate = this.filters.startDate ? new Date(this.filters.startDate).setHours(0, 0, 0, 0) : null;
    const endDate = this.filters.endDate ? new Date(this.filters.endDate).setHours(23, 59, 59, 999) : null;
    
    this.orderReports = this.orderReports
      .filter((report) => {
        const reportDate = new Date(report.reportDate).getTime();
        if (startDate && reportDate < startDate) {
          return false; 
        }
        if (endDate && reportDate > endDate) {
          return false; 
        }
        return true; 
      })
      .sort((a, b) => {
        const dateA = new Date(a.reportDate).getTime();
        const dateB = new Date(b.reportDate).getTime();
        return dateA - dateB; 
      });
  } 
}