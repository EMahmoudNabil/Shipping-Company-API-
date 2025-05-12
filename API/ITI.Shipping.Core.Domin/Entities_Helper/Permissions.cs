using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Domin.Entities_Helper
{
    public static class Permissions
    {
        public static string Type { get; } = "permissions";
        // ----------- Permissions ---------------------------------
        public const string ViewPermissions = "Permissions:ViewPermissions";
        public const string AddPermissions = "Permissions:AddPermissions";
        public const string UpdatePermissions = "Permissions:UpdatePermissions";
        public const string DeletePermissions = "Permissions:DeletePermissions";

        // ----------- Settings ---------------------------------
        public const string ViewSettings = "Settings:ViewSettings";
        public const string AddSettings = "Settings:AddSettings";
        public const string UpdateSettings = "Settings:UpdateSettings";
        public const string DeleteSettings = "Settings:DeleteSettings";

        // ----------- Bank ---------------------------------
        public const string ViewBank = "Bank:ViewBank";
        public const string AddBank = "Bank:AddBank";
        public const string UpdateBank = "Bank:UpdateBank";
        public const string DeleteBank = "Bank:DeleteBank";

        // ----------- MoneySafe ---------------------------------
        public const string ViewMoneySafe = "MoneySafe:ViewMoneySafe";
        public const string AddMoneySafe = "MoneySafe:AddMoneySafe";
        public const string UpdateMoneySafe = "MoneySafe:UpdateMoneySafe";
        public const string DeleteMoneySafe = "MoneySafe:DeleteMoneySafe";

        // ----------- Branches ---------------------------------
        public const string ViewBranches = "Branches:ViewBranches";
        public const string AddBranches = "Branches:AddBranches";
        public const string UpdateBranches = "Branches:UpdateBranches";
        public const string DeleteBranches = "Branches:DeleteBranches";

        // ----------- Employees ---------------------------------
        public const string ViewEmployees = "Employees:ViewEmployees";
        public const string AddEmployees = "Employees:AddEmployees";
        public const string UpdateEmployees = "Employees:UpdateEmployees";
        public const string DeleteEmployees = "Employees:DeleteEmployees";

        // ----------- Merchants ---------------------------------
        public const string ViewMerchants = "Merchants:ViewMerchants";
        public const string AddMerchants = "Merchants:AddMerchants";
        public const string UpdateMerchants = "Merchants:UpdateMerchants";
        public const string DeleteMerchants = "Merchants:DeleteMerchants";

        // ----------- Couriers ---------------------------------
        public const string ViewCouriers = "Couriers:ViewCouriers";
        public const string AddCouriers = "Couriers:AddCouriers";
        public const string UpdateCouriers = "Couriers:UpdateCouriers";
        public const string DeleteCouriers = "Couriers:DeleteCouriers";

        // ----------- Regions ---------------------------------
        public const string ViewRegions = "Regions:ViewRegions";
        public const string AddRegions = "Regions:AddRegions";
        public const string UpdateRegions = "Regions:UpdateRegions";
        public const string DeleteRegions = "Regions:DeleteRegions";

        // ----------- Cities ---------------------------------
        public const string ViewCities = "Cities:ViewCities";
        public const string AddCities = "Cities:AddCities";
        public const string UpdateCities = "Cities:UpdateCities";
        public const string DeleteCities = "Cities:DeleteCities";

        // ----------- Orders ---------------------------------
        public const string ViewOrders = "Orders:ViewOrders";
        public const string AddOrders = "Orders:AddOrders";
        public const string UpdateOrders = "Orders:UpdateOrders";
        public const string DeleteOrders = "Orders:DeleteOrders";

        // ----------- OrderReports ---------------------------------
        public const string ViewOrderReports = "OrderReports:ViewOrderReports";
        public const string AddOrderReports = "OrderReports:AddOrderReports";
        public const string UpdateOrderReports = "OrderReports:UpdateOrderReports";
        public const string DeleteOrderReports = "OrderReports:DeleteOrderReports";

        // ----------- Accounts ---------------------------------
        public const string ViewAccounts = "Accounts:ViewAccounts";
        public const string AddAccounts = "Accounts:AddAccounts";
        public const string UpdateAccounts = "Accounts:UpdateAccounts";
        public const string DeleteAccounts = "Accounts:DeleteAccounts";

        public static IList<string?> GetAllPermissions() =>
        typeof(Permissions).GetFields().Select(f => f.GetValue(f) as string).ToList();
    }
}