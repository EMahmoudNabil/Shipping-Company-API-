export interface Courier {
  id?: string;
  email: string;
  password: string;
  fullName: string;
  phoneNumber: string;
  address: string;
  branchId: number;
  deductionType: number;
  deductionCompanyFromOrder: number;
  specialCourierRegions: {
    regionId: number;
  }[];
}
export interface CourierDTO {
  id: string;
  courierName: string;  // Changed to match backend casing
}