import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { environment } from '../../environment';
import { LoginDTO, LoginResponseDTO } from '../../models/Login.Interface';
import { Router } from '@angular/router';

export enum UserType {
  EMPLOYEE = 'employee',
  MERCHANT = 'merchant'
}

interface UserData {
  id: string;
  email: string;
  fullName: string;
  merchantName?: string;
  type?: UserType;
}

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private http: HttpClient, private router: Router) {}

  // دالة تسجيل الدخول المعدلة
  login(payload: LoginDTO): Observable<LoginResponseDTO> {
    return this.http.post<LoginResponseDTO>(`${environment.apiUrl}/api/Auth/login`, payload).pipe(
      tap((response: LoginResponseDTO & { merchantName?: string }) => {
        // حفظ التوكن ووقت الانتهاء
        localStorage.setItem('authToken', response.token);
        localStorage.setItem('authTokenExpiry', (Date.now() + response.expiresIn * 1000).toString());
        
        // تحديد نوع المستخدم
        const userType = response.merchantName ? UserType.MERCHANT : UserType.EMPLOYEE;
        
        // حفظ بيانات المستخدم
        const userData: UserData = {
          id: response.id,
          email: response.email,
          fullName: response.fullName,
          merchantName: response.merchantName,
          type: userType
        };
        
        localStorage.setItem('userData', JSON.stringify(userData));
      })
    );
  }

  // دالة تسجيل الخروج
  logout(): void {
    localStorage.removeItem('authToken');
    localStorage.removeItem('authTokenExpiry');
    localStorage.removeItem('userData');
    this.router.navigate(['/login']);
  }

  // دالة الحصول على بيانات المستخدم الحالي
  getCurrentUser(): UserData | null {
    const userData = localStorage.getItem('userData');
    return userData ? JSON.parse(userData) : null;
  }

  // دالة الحصول على التوكن
  getAuthToken(): string | null {
    return localStorage.getItem('authToken');
  }

  // دالة التحقق من حالة المصادقة
  isAuthenticated(): boolean {
    return !!this.getAuthToken() && !this.isTokenExpired();
  }

  // دالة التحقق من انتهاء صلاحية التوكن
  private isTokenExpired(): boolean {
    const expiry = localStorage.getItem('authTokenExpiry');
    if (!expiry) return true;
    return Date.now() > parseInt(expiry, 10);
  }

  // دالة الحصول على ID المستخدم الحالي
  getCurrentUserId(): string {
    const user = this.getCurrentUser();
    return user?.id || "null";
  }

  // دالة تحديد نوع المستخدم
  getUserType(): UserType {
    const userData = this.getCurrentUser();
    
    // إذا كان نوع المستخدم محدداً مسبقاً
    if (userData?.type) {
      return userData.type;
    }
    
    // تحديد النوع بناءً على وجود merchantName
    if (userData?.merchantName) {
      return UserType.MERCHANT;
    }
    
    // افتراضي موظف
    return UserType.EMPLOYEE;
  }

  // دالة التحقق من أن المستخدم موظف
  isEmployee(): boolean {
    return this.getUserType() === UserType.EMPLOYEE;
  }

  // دالة التحقق من أن المستخدم تاجر
  isMerchant(): boolean {
    return this.getUserType() === UserType.MERCHANT;
  }

  // دالة لتحديث بيانات المستخدم
  updateUserData(updates: Partial<UserData>): void {
    const currentData = this.getCurrentUser() || {};
    const updatedData = { ...currentData, ...updates };
    localStorage.setItem('userData', JSON.stringify(updatedData));
  }

  getPermissions(): string[] {
    const permissions = localStorage.getItem('permissions');
    return permissions ? JSON.parse(permissions) : [];
  }

  setPermissions(permissions: string[]): void {
    localStorage.setItem('permissions', JSON.stringify(permissions));
  }
}