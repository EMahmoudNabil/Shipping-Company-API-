import { Injectable } from '@angular/core';
import { UserData } from '../../models/Login.Interface';


@Injectable({
  providedIn: 'root',
})
export class UserDataService {
  private userData: UserData | null = null;

  setUserData(data: UserData): void {
    this.userData = data;
    localStorage.setItem('userData', JSON.stringify(data));
  }

  getUserData(): UserData | null {
    if (!this.userData) {
      const stored = localStorage.getItem('userData');
      if (stored) {
        this.userData = JSON.parse(stored);
      }
    }
    return this.userData;
  }

  clearUserData(): void {
    this.userData = null;
    localStorage.removeItem('userData');
    localStorage.removeItem('authToken');
    localStorage.removeItem('authTokenExpiry');
  }
}
