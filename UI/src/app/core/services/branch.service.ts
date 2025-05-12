import { Branch } from './../../models/Branch.Interface';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

import { Observable, tap } from 'rxjs';
import { GenericCURD } from '../../models/Generic.interface';
import { environment } from '../../environment';
@Injectable({
    providedIn: 'root'
  })
  
  export class BranchService  implements GenericCURD<any> {
  
   private apiUrl = `${environment.apiUrl}/api/Branch`;
     
      private hasNextPage: boolean = false;
  
    constructor(private http: HttpClient) {}
  
    getAll(): Observable<Branch[]> {
     
      return this.http.get<Branch[]>(this.apiUrl);
    }
    getAllWithPagination(pageNumber: number, pageSize: number): Observable<Branch[]> {
      const params = new HttpParams()
        .set('PageNumber', pageNumber.toString())
        .set('PageSize', pageSize.toString());
    
      return this.http.get<Branch[]>(this.apiUrl, { params }).pipe(
        tap((branches) => {
          this.hasNextPage = branches.length === pageSize;
        })
      );
    }
    getById(id: number): Observable<Branch> {
      return this.http.get<Branch>(`${this.apiUrl}/${id}`);
    }
  
    create(branch: Branch): Observable<Branch> {
      return this.http.post<Branch>(this.apiUrl, branch);
    }
  
    update(id: number, branch: Branch): Observable<Branch> {
      return this.http.put<Branch>(`${this.apiUrl}/${id}`, branch);
    }
  
    delete(id: number): Observable<any> {
      return this.http.delete(`${this.apiUrl}/${id}`);
    }
  }
  