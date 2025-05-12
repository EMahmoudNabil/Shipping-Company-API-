import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { City } from '../../models/City.interface';
import { environment } from '../../environment';

@Injectable({
  providedIn: 'root',
})
export class CityService {
  private apiUrl = `${environment.apiUrl}/api/CitySetting`;

  constructor(private http: HttpClient) {}

  getAll(): Observable<City[]> {
    return this.http.get<City[]>(this.apiUrl);
  }

  getAllWithPagination(pageNumber: number, pageSize: number): Observable<City[]> {
    const params = new HttpParams()
      .set('PageNumber', pageNumber.toString())
      .set('PageSize', pageSize.toString());
    return this.http.get<City[]>(this.apiUrl, { params });
  }
  getByRegionId(regionId: number): Observable<City[]> {
    return this.http.get<City[]>(`${this.apiUrl}/CityByRegion?regionId=${regionId}`);
  }
  getById(id: number): Observable<City> {
    return this.http.get<City>(`${this.apiUrl}/${id}`);
  }

  create(city: Partial<City>): Observable<City> {
    return this.http.post<City>(this.apiUrl, city);
  }

  update(id: number, city: Partial<City>): Observable<City> {
    return this.http.put<City>(`${this.apiUrl}/${id}`, city);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  getCityByGovernorateName(regionId: number): Observable<City[]> {
    const params = new HttpParams().set('RegionId', regionId.toString());
    return this.http.get<City[]>(`${this.apiUrl}/GetCityByGovernorateName`, { params });
  }
}