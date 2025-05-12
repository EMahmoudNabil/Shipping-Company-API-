using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Domin.Entities_Helper;
public static class DefaultRole
{
    public static string Admin = nameof(Admin);
    public static string AdminRoleId = "01961d25-b4da-7184-a2a8-765486bd4857";
    public static string AdminRoleConcurrencyStamp = "EAE00686-2608-4516-AD1B-F96CD87C475E";
    public static string Courier = nameof(Courier);
    public static string CourierRoleId = "01961d25-b4da-75a5-a1f4-a7aa10e421ed";
    public static string CourierRoleConcurrencyStamp = "386C6E14-D0FD-40FF-80D0-74B419360EF0";
    public static string Merchant = nameof(Merchant);
    public static string MerchantrRoleId = "01961d25-b4da-71e9-a488-1b8db232e984";
    public static string MerchantRoleConcurrencyStamp = "1420D50C-F54D-4503-88E8-A2EFA3BD7137";
}
