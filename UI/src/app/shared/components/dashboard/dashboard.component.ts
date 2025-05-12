import { Component, OnInit, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { Chart, ChartConfiguration, registerables } from 'chart.js';

import { AuthService } from '../../../core/services/auth.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ActivatedRoute } from '@angular/router';
import { DashboardService } from '../../../core/services/dashboard.service';

Chart.register(...registerables);

interface DashboardStats {
  totalDelivered: number;
  totalPending: number;
  totalCancelled: number;
  totalInProcessing: number;
  totalAwaitingConfirmation: number;
  totalRejected: number;
  totalReceived: number;
  totalShipped: number;
  totalReturned: number;
  totalPayed: number;
  totalUpdated: number;
}

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatDividerModule,
    MatProgressSpinnerModule
  ],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit, AfterViewInit {
  @ViewChild('orderChart') orderChartRef!: ElementRef<HTMLCanvasElement>;
  
  userType: 'employee' | 'merchant' = 'employee';
  isLoading = true;
  lastUpdated = new Date();
  stats: DashboardStats = {
    totalDelivered: 0,
    totalPending: 0,
    totalCancelled: 0,
    totalInProcessing: 0,
    totalAwaitingConfirmation: 0,
    totalRejected: 0,
    totalReceived: 0,
    totalShipped: 0,
    totalReturned: 0,
    totalPayed: 0,
    totalUpdated: 0
  };

  private chart: Chart | null = null;

  constructor(
    private dashboardService: DashboardService,
    private authService: AuthService,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute
  ) {
    this.setArabicLocale();
  }

  private setArabicLocale(): void {
    // You can add Arabic locale configuration here if needed
  }

  ngOnInit(): void {
    this.userType = this.route.snapshot.data['userType'] || this.authService.getUserType() as 'employee' | 'merchant';
    this.loadDashboardData();
  }

  ngAfterViewInit(): void {
    this.initializeChart();
  }

  refreshDashboard(): void {
    this.loadDashboardData();
  }

  private loadDashboardData(): void {
    this.isLoading = true;
    this.dashboardService.getDashboardData().subscribe({
      next: (data) => {
        this.stats = {
          totalDelivered: data.totalDelivered || 0,
          totalPending: data.totalPending || 0,
          totalCancelled: data.totalCancelled || 0,
          totalInProcessing: data.totalInProcessing || 0,
          totalAwaitingConfirmation: data.totalAwaitingConfirmation || 0,
          totalRejected: data.totalRejected || 0,
          totalReceived: data.totalReceived || 0,
          totalShipped: data.totalShipped || 0,
          totalReturned: data.totalReturned || 0,
          totalPayed: data.totalPayed || 0,
          totalUpdated: data.totalUpdated || 0
        };
        this.lastUpdated = new Date();
        this.updateChart(data);
        this.isLoading = false;
        this.showSuccess('تم تحميل البيانات بنجاح');
      },
      error: (error: any) => {
        console.error('Error loading dashboard data:', error);
        this.isLoading = false;
        this.showError('فشل تحميل البيانات');
      }
    });
  }

  private initializeChart(): void {
    const ctx = this.orderChartRef.nativeElement.getContext('2d');
    if (!ctx) return;

    const config: ChartConfiguration = {
      type: 'line',
      data: {
        labels: [],
        datasets: [
          {
            label: 'Orders',
            data: [],
            borderColor: '#3f51b5',
            backgroundColor: 'rgba(63, 81, 181, 0.1)',
            tension: 0.4,
            fill: true
          }
        ]
      },
      options: {
        responsive: true,
        maintainAspectRatio: false,
        plugins: {
          legend: {
            position: 'top',
          },
          title: {
            display: true,
            text: 'Order Trends'
          }
        },
        scales: {
          y: {
            beginAtZero: true,
            ticks: {
              precision: 0
            }
              }
            }
      }
    };

    this.chart = new Chart(ctx, config);
  }

  private updateChart(data: any): void {
    if (!this.chart) return;

    // Update chart data based on the received data
    // This is a placeholder - adjust according to your actual data structure
    const labels = Array.from({ length: 30 }, (_, i) => {
      const date = new Date();
      date.setDate(date.getDate() - (29 - i));
      return date.toLocaleDateString();
    });

    const orderData = Array.from({ length: 30 }, () => Math.floor(Math.random() * 100));

    this.chart.data.labels = labels;
    this.chart.data.datasets[0].data = orderData;
    this.chart.update();
  }

  private showSuccess(message: string): void {
    this.snackBar.open(message, 'إغلاق', {
      duration: 3000,
      horizontalPosition: 'end',
      verticalPosition: 'top',
      panelClass: ['success-snackbar']
    });
  }

  private showError(message: string): void {
    this.snackBar.open(message, 'إغلاق', {
      duration: 5000,
      horizontalPosition: 'end',
      verticalPosition: 'top',
      panelClass: ['error-snackbar']
    });
  }

  ngOnDestroy(): void {
    if (this.chart) {
      this.chart.destroy();
    }
  }
}