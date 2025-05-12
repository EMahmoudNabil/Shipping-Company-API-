export interface OrderReport {
  id: number;
  orderStatus: string;
  merchantName: string;
  merchantId: string;
  customerName: string;
  customerPhone1: string;
  regionName: string;
  cityName: string;
  orderCost: number;
  amountReceived: number;
  shippingCost: number;
  shippingCostPaid: number;
  companyValue: number;
  reportDate: string;
}

