export interface WeightSetting {
    id: number;
    minWeight: number;
    maxWeight: number;
    costPerKg: number;
    createdAt: string; // Use string to handle date formatting
  }