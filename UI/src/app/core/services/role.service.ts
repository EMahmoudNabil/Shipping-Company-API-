import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../app/environment';
import { RoleResponseDTO, CreateRoleRequestDTO } from '../../models/roles.interface';

@Injectable({
  providedIn: 'root'
})
export class RoleService {
  private apiUrl = `${environment.apiUrl}/api/Groups`;

  constructor(private http: HttpClient) {}

  // Get all roles
  getAllRoles(): Observable<RoleResponseDTO[]> {
    return this.http.get<RoleResponseDTO[]>(this.apiUrl);
  }

  // Get a single role by ID
  getRoleById(id: string): Observable<RoleResponseDTO> {
    return this.http.get<RoleResponseDTO>(`${this.apiUrl}/${id}`);
  }

  // // Create a new role
  // createRole(createRoleRequestDTO: CreateRoleRequestDTO): Observable<any> {
  //   return this.http.post(this.apiUrl, createRoleRequestDTO);
  // }

  createRole(createRoleRequestDTO: CreateRoleRequestDTO): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });

    return this.http.post(this.apiUrl, createRoleRequestDTO, { headers });
  }

  // Update a role
  updateRole(id: string, updateRoleRequestDTO: CreateRoleRequestDTO): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, updateRoleRequestDTO);
  }

  // Delete a role
  deleteRole(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

  getCurrentUserPermissions(): Observable<string[]> {
    return this.http.get<string[]>(`${this.apiUrl}/GetCurrentUserPermissions`);
  }
}
