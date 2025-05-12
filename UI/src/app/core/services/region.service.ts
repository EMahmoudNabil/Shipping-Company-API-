import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

import { Observable, tap } from 'rxjs';
import { Region } from '../../models/Region.Interface ';
import { GenericCURD } from '../../models/Generic.interface';
import { environment } from '../../environment';

@Injectable({
  providedIn: 'root'
})
export class RegionService  implements GenericCURD<any> {

 private apiUrl = `${environment.apiUrl}/api/Region`;
   
    private PageNumber: number = 1;
    private pageSize: number = 10;
    private pages: number[] = [];
    private hasNextPage: boolean = false;

  constructor(private http: HttpClient) {}

  getAll(): Observable<Region[]> {
   
    return this.http.get<Region[]>(`${this.apiUrl}/?PageSize=${this.pageSize}&PageNumber=${this.PageNumber}`).pipe(
      tap((regions) => {
        // تحديد إذا كانت هناك صفحة تالية
        this.hasNextPage = regions.length === this.pageSize;
      }));
  }
  getAllWithPagination(pageNumber: number, pageSize: number): Observable<Region[]> {
    const params = new HttpParams()
      .set('PageNumber', pageNumber.toString())
      .set('PageSize', pageSize.toString());
  
    return this.http.get<Region[]>(this.apiUrl, { params }).pipe(
      tap((regions) => {
        this.hasNextPage = regions.length === pageSize;
      })
    );
  }
  getById(id: number): Observable<Region> {
    return this.http.get<Region>(`${this.apiUrl}/${id}`);
  }

  create(region: Region): Observable<Region> {
    return this.http.post<Region>(this.apiUrl, region);
  }

  update(id: number, region: Region): Observable<Region> {
    return this.http.put<Region>(`${this.apiUrl}/${id}`, region);
  }

  delete(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
