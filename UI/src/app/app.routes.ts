import { Routes } from '@angular/router';
import { LoginComponent } from './shared/components/login/login.component';
import { AuthGuard } from './core/guards/auth.guard';
import { MainSystemComponent } from './features/main-system/main-system.component';
import { RegionComponent } from './shared/components/region/region.component'; 
import { WeightSettingComponent } from './shared/components/weight-setting/weight-setting.component';
import { CityComponent } from './shared/components/city/city.component';
import { BranchComponent } from './shared/components/branch/branch.component';
import { ShippingTypeComponent } from './shared/components/shipping-type/shipping-type.component';
import { AddEmployeeComponent } from './shared/components/add-employee/add-employee.component';
import { MerchantComponent } from './shared/components/merchant/merchant.component';
import { CourierComponent } from './shared/components/courier/courier.component';
import { OrderReportComponent } from './shared/components/order-report/order-report.component';
import { OrderStatusComponent } from './shared/components/order-status/order-status.component';

import { RoleComponent } from './shared/components/permission/permission.component';
import { DashboardComponent } from './shared/components/dashboard/dashboard.component';
import { UnauthorizedComponent } from './shared/components/unauthorized/unauthorized.component';
import { OrderFromComponent } from './shared/components/order-form/order-form.component';

export const routes: Routes = [
    { path: '', redirectTo: '/login', pathMatch: 'full' }, 
    { path: 'login', component: LoginComponent },
    { path: 'unauthorized', component: UnauthorizedComponent },
    { 
        path: '', 
        component: MainSystemComponent, 
        canActivate: [AuthGuard],
        children: [
            { path: 'dashboard', component: DashboardComponent, data: { permissions: ['Dashboard:View'] } },
            
            // الإدارة
            { path: 'add-employee', component: AddEmployeeComponent, data: { permissions: ['Employees:AddEmployees'] } },
            { path: 'Groups', component: RoleComponent, data: { permissions: ['Permissions:ViewPermissions'] } },
            
            // الطلبات
            { path: 'orders/form', component: OrderFromComponent, data: { permissions: ['Orders:AddOrders'],mode: 'create' } },
            { path: 'orders', component: OrderStatusComponent, data: { permissions: ['Orders:ViewOrders'] } },
            { path: 'order-reports', component: OrderReportComponent, data: { permissions: ['OrderReports:ViewOrderReports'] } },
            { path: 'orders/form/:id', component: OrderFromComponent, data: { permissions: ['Orders:UpdateOrders'] ,mode: 'edit' } },
            
            // إعدادات النظام
            { path: 'regions', component: RegionComponent, data: { permissions: ['Regions:ViewRegions'] } },
            { path: 'branches', component: BranchComponent, data: { permissions: ['Branches:ViewBranches'] } },
            { path: 'Shippingtypes', component: ShippingTypeComponent, data: { permissions: ['ShippingTypes:View'] } },
            { path: 'weightsettings', component: WeightSettingComponent, data: { permissions: ['Settings:ViewSettings'] } },
            { path: 'cities', component: CityComponent, data: { permissions: ['Cities:ViewCities'] } },
            { path: 'addMerchant', component: MerchantComponent, data: { permissions: ['Merchants:AddMerchants'] } },
            { path: 'addCourier', component: CourierComponent, data: { permissions: ['Couriers:AddCouriers'] } }
        ]
    }
];