// login.interface.ts
export interface LoginDTO {
  email: string;
  password: string;
}

export interface LoginResponseDTO {
  id: string;
  email: string;
  fullName: string;
  token: string;
  expiresIn: number;
  userType: 'employee' | 'merchant'; // إضافة حقل جديد لتحديد نوع المستخدم
  merchantId?: string; // فقط للتجار
}

export interface UserData {
  id: string;
  email: string;
  fullName: string;
  userType: 'employee' | 'merchant'; // إضافة نوع المستخدم
  merchantId?: string; // اختياري للتجار
}