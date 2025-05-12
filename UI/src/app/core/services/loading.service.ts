import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environment';
import { EmpDashboardDTO, MerchantDashboardDTO } from '../../models/dashboard.interface';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  private readonly apiUrl = `${environment.apiUrl}/api/Dashboard`;

  constructor(private http: HttpClient) { }

  /**
   * Gets dashboard data based on user role
   * @returns Observable of EmpDashboardDTO or MerchantDashboardDTO
   */
  getDashboardData(): Observable<EmpDashboardDTO | MerchantDashboardDTO> {
    const roles = JSON.parse(localStorage.getItem('roles') || '[]');
    const isMerchant = roles[0] === 'Merchant';
    
    if (isMerchant) {
      return this.http.get<MerchantDashboardDTO>(`${this.apiUrl}/MerchantDashboard`);
    } else {
      return this.http.get<EmpDashboardDTO>(`${this.apiUrl}/EmpDashboard`);
    }
  }

  /**
   * Gets user role from localStorage
   * @returns string
   */
  private getUserRole(): string {
    const user = JSON.parse(localStorage.getItem('roles') || '{}');
    return user.role || '';
  }

  // Keep these methods for specific usage if needed
  getEmployeeDashboard(): Observable<EmpDashboardDTO> {
    return this.http.get<EmpDashboardDTO>(`${this.apiUrl}/EmpDashboard`);
  }

  getMerchantDashboard(): Observable<MerchantDashboardDTO> {
    return this.http.get<MerchantDashboardDTO>(`${this.apiUrl}/MerchantDashboard`);
  }
}