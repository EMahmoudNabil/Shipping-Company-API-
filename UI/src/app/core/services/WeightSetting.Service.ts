import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { WeightSetting } from '../../models/WeightSetting.interface';
import { GenericCURD } from '../../models/Generic.interface';
import { environment } from '../../environment';

@Injectable({
  providedIn: 'root',
})
export class WeightSettingService implements GenericCURD<WeightSetting> {
  private apiUrl = `${environment.apiUrl}/api/WeightSetting`;

  constructor(private http: HttpClient) {}

  getAll(): Observable<WeightSetting[]> {
    return this.http.get<WeightSetting[]>(this.apiUrl);
  }

  getAllWithPagination(pageNumber: number, pageSize: number): Observable<WeightSetting[]> {
    const params = new HttpParams()
      .set('PageNumber', pageNumber.toString())
      .set('PageSize', pageSize.toString());
    return this.http.get<WeightSetting[]>(this.apiUrl, { params });
  }

  getById(id: number): Observable<WeightSetting> {
    return this.http.get<WeightSetting>(`${this.apiUrl}/${id}`);
  }

  create(weightSetting: WeightSetting): Observable<WeightSetting> {
    return this.http.post<WeightSetting>(this.apiUrl, weightSetting);
  }

  update(id: number, weightSetting: WeightSetting): Observable<WeightSetting> {
    return this.http.put<WeightSetting>(`${this.apiUrl}/${id}`, weightSetting);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}