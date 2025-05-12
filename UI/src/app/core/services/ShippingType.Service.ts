import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { GenericCURD } from '../../models/Generic.interface';
import { ShippingType } from '../../models/ShippingType.Interface';
import { environment } from '../../environment';



@Injectable({
  providedIn: 'root'
})
export class ShippingTypeService implements GenericCURD<ShippingType> {
  private apiUrl = `${environment.apiUrl}/api/ShippingType`;
  private hasNextPage: boolean = false;

  constructor(private http: HttpClient) {}

  getAll(): Observable<ShippingType[]> {
    return this.http.get<ShippingType[]>(this.apiUrl);
  }

  getAllWithPagination(pageNumber: number, pageSize: number): Observable<ShippingType[]> {
    const params = new HttpParams()
      .set('PageNumber', pageNumber.toString())
      .set('PageSize', pageSize.toString());
  
    return this.http.get<ShippingType[]>(this.apiUrl, { params }).pipe(
      tap((shippingTypes) => {
        this.hasNextPage = shippingTypes.length === pageSize;
      })
    );
  }

  getById(id: number): Observable<ShippingType> {
    return this.http.get<ShippingType>(`${this.apiUrl}/${id}`);
  }

  create(shippingType: ShippingType): Observable<ShippingType> {
    return this.http.post<ShippingType>(this.apiUrl, shippingType);
  }

  update(id: number, shippingType: ShippingType): Observable<ShippingType> {
    return this.http.put<ShippingType>(`${this.apiUrl}/${id}`, shippingType);
  }

  delete(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}