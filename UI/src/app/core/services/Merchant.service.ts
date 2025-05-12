
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GenericCURD } from '../../models/Generic.interface';
import { environment } from '../../environment';
import { Merchant, MerchantResponse } from '../../models/Merchant .Interface';


@Injectable({
  providedIn: 'root'
})
export class MerchantService implements GenericCURD<any> {
  private apiUrl = `${environment.apiUrl}/api/Auth/addMerchant`;
  private specialCityCostUrl = `${environment.apiUrl}/api/SpecialCityCost`;
  private GetMerchant = `${environment.apiUrl}/api/Merchant/GetMerchant`;
  constructor(private http: HttpClient) {}
  
  getAll(): Observable<MerchantResponse[]> {
      return this.http.get<MerchantResponse[]>(`${this.GetMerchant}`); //error
  }
  
  getById(id: number | string): Observable<Merchant> {
    throw new Error('Method not implemented.');
  }
  
  update(id: number | string, item: Merchant): Observable<Merchant> {
    throw new Error('Method not implemented.');
  }
  
  delete(id: number | string): Observable<void> {
    throw new Error('Method not implemented.');
  }

  create(merchant: Merchant): Observable<Merchant> {
    // Format merchant data to match the expected API format
    const merchantData = {
      email: merchant.email,
      password: merchant.password,
      fullName: merchant.fullName,
      phoneNumber: merchant.phoneNumber,
      address: merchant.address,
      branchId: merchant.branchId,
      regionId: merchant.regionId,
      cityId: merchant.cityId,
      storeName: merchant.storeName,
      specialCityCosts: merchant.specialCityCosts.map(item => ({
        price: item.price,
        citySettingId: item.citySettingId
      }))
    };
    
    // Send the formatted data to the API
    return this.http.post<Merchant>(this.apiUrl, merchantData);
  }



}