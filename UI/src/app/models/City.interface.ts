export interface City {
    id: number;
    name: string;
    standardShippingCost: number;
    pickupShippingCost: number;
    createdAt: string;
    regionId?: number;
    regionName?: string;
    usersName: string[];
    ordersCost: number[];
    usersThatHasSpecialCityCost: string[];
  }