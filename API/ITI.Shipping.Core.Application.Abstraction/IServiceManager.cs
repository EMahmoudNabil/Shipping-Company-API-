using ITI.Shipping.Core.Application.Abstraction.Branch;
using ITI.Shipping.Core.Application.Abstraction.CitySetting;
using ITI.Shipping.Core.Application.Abstraction.Courier;
using ITI.Shipping.Core.Application.Abstraction.CourierReport;
using ITI.Shipping.Core.Application.Abstraction.Dashboard;
using ITI.Shipping.Core.Application.Abstraction.Employee;
using ITI.Shipping.Core.Application.Abstraction.Merchant;
using ITI.Shipping.Core.Application.Abstraction.Order;
using ITI.Shipping.Core.Application.Abstraction.OrderReport;
using ITI.Shipping.Core.Application.Abstraction.Product;
using ITI.Shipping.Core.Application.Abstraction.Region;
using ITI.Shipping.Core.Application.Abstraction.ShippingType;
using ITI.Shipping.Core.Application.Abstraction.SpecialCityCost;
using ITI.Shipping.Core.Application.Abstraction.SpecialCourierRegion;
using ITI.Shipping.Core.Application.Abstraction.WeightSetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Application.Abstraction
{
    public interface IServiceManager
    {
        // / Define all the services that the service manager will provide
        public IBranchService BranchService { get; }
        public ICitySettingService CitySettingService { get; }
        public ICourierReportService CourierReportService { get; }
        public IRegionService RegionService { get; }
        public ISpecialCourierRegionService SpecialCourierRegionService { get; }
        public ISpecialCityCostService specialCityCostService { get; }
        public IShippingTypeService shippingTypeService { get; }
        public IWeightSettingService weightSettingService { get; }
        public IOrderService orderService { get; }
        public IProductService productService { get; }
        public IOrderReportService orderReportService { get; }
        public ICourierService courierService { get; }
        public IEmployeeService employeeService { get; }
        public IDashboardService dashboardService { get; }
        public IMerchantService merchantService { get; }
    }
}
