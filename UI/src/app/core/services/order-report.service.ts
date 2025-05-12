import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environment';
import { OrderReport } from '../../models/OrderReport.Interface';

@Injectable({
  providedIn: 'root',
})
export class OrderReportService {
  private apiUrl = `${environment.apiUrl}/api/OrderReport`;

  constructor(private http: HttpClient) {}

  getOrderReports(
    pageNumber: number,
    pageSize: number,
    filters: { orderStatus?: string; startDate?: string; endDate?: string }
  ): Observable<{ data: OrderReport[]; totalPages: number }> {
    let params = new HttpParams()
      .set('PageNumber', pageNumber.toString())
      .set('PageSize', pageSize.toString());

    if (filters.orderStatus) {
      params = params.set('OrderStatus', filters.orderStatus);
    }
    if (filters.startDate) {
      params = params.set('StartDate', filters.startDate);
    }
    if (filters.endDate) {
      params = params.set('EndDate', filters.endDate);
    }

    return this.http.get<{ data: OrderReport[]; totalPages: number }>(
      `${this.apiUrl}/GetAllByPramter`,
      { params }
    );
  }
}