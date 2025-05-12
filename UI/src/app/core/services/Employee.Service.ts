import { Injectable } from "@angular/core";
import { environment } from "../../environment";
import { HttpClient } from "@angular/common/http";
import { Employee } from "../../models/Employee .interface";
import { Observable } from "rxjs";
import { Role } from "../../models/Role.interface";



@Injectable({
    providedIn: 'root'
  })


export class EmployeeService {

    private apiUrl = `${environment.apiUrl}/api`;

    constructor(private http: HttpClient) { }

    createEmployee(employee: Employee): Observable<Employee> {
      return this.http.post<Employee>(`${this.apiUrl}/Auth/addEmployee`, employee);
    }
    getRoles(): Observable<Role[]> {
        return this.http.get<Role[]>(`${this.apiUrl}/Groups`);
      }
}

