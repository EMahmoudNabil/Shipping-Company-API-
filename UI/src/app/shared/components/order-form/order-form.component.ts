import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';


import { UnitOfWorkServices } from '../../../core/services/unitOfWork.service';
import { CreateOrderDto, Order, OrderProduct, OrderWithProductsDto, ProductDTO, UpdateOrderDto } from '../../../models/order.interface';
import { AuthService } from '../../../core/services/auth.service';
import { Role } from '../../../models/Role.interface';
import { ToastrService } from 'ngx-toastr';
import { City } from '../../../models/City.interface';

import { Branch } from '../../../models/Branch.Interface';
import { PageHeaderComponent } from "../page-header/page-header.component";
import { PaymentType } from '../../enum/payment-type.enum';
import { ShippingType } from '../../../models/ShippingType.Interface';

import { OrderType } from '../../enum/order-types.enum';
import { Merchant, MerchantResponse } from '../../../models/Merchant .Interface';
import { Region } from '../../../models/Region.Interface ';
import { forkJoin } from 'rxjs';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import { MatTableDataSource } from '@angular/material/table';
import { OrderStatus } from '../../../models/OrderStatus.Interface';
@Component({
  selector: 'app-order-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    PageHeaderComponent,
     // Angular Material Modules
    
     MatButtonModule,
     MatCardModule,
     MatCheckboxModule,
     MatFormFieldModule,
     MatInputModule,
     MatPaginatorModule,
     MatProgressSpinnerModule,
     MatSelectModule,
     MatTableModule,
     MatTooltipModule,
     MatChipsModule,
     MatIconModule
  
  ],
  templateUrl: './order-form.component.html',
  styleUrl: './order-form.component.css'
})
export class OrderFromComponent implements OnInit {
  // Form Configuration
  
  @Input() mode: 'create' | 'edit' = 'create';
  @Input() orderId?: number;

  orderForm!: FormGroup;
  merchants: MerchantResponse[] = [];
  branches: Branch[] = [];
  regions: Region[] = [];
  cities: City[] = [];
  shippingTypes: ShippingType[] = [];
  isEmployee = false;
  isLoadingCities = false;
  isLoading = false;
  displayedColumns: string[] = ['name', 'quantity', 'weight', 'actions'];
  productsDataSource = new MatTableDataSource<AbstractControl>([]);

  
  
  // UI Configuration
  paymentTypes = [
    { value: PaymentType.Collectible, label: 'تحصيل' },
    { value: PaymentType.Prepaid, label: 'مسبق الدفع' },
    { value: PaymentType.Change, label: 'شحن مقابل' }
  ];

  orderTypes = [
    { value: OrderType.Pickup, label: 'استلام من الفرع' },
    { value: OrderType.Delivery, label: 'توصيل إلى العنوان' }
  ];



  
 
  constructor(
    private fb: FormBuilder,
    private _unitOfWork: UnitOfWorkServices,
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute,
    private toastr: ToastrService,
    private cdr: ChangeDetectorRef
    
  ) {
   
  }
  ngOnInit(): void {
    this.initializeForm();
    this.productsDataSource = new MatTableDataSource(this.products.controls);
    // تحميل البيانات الأساسية
    this.loadBranches();
    this.loadRegions();
    // this.loadCities(); // دالة جديدة لتحميل جميع المدن
    this.loadShippingTypes();
    this.loadMerchants();; // تهيئة النموذج أولاً
    
    this.route.queryParams.subscribe(params => {
      this.mode = params['mode'] || 'create';
    });
    
    this.route.params.subscribe(params => {
      if (params['id']) {
        this.orderId = +params['id'];
        this.loadOrderData(this.orderId);
      }
    });
  
    this.checkUserRole();
    this.loadInitialData();
  }

  
  private loadInitialData(): void {
    this.loadBranches();
    this.loadRegions();
    this.loadShippingTypes();
    this.loadMerchants();
  }
  ngAfterViewInit() {
    this.updateProductsDataSource();
  }
  

  
  addProduct(): void {
    const productGroup = this.createProductFormGroup();
    this.products.push(productGroup);
    this.updateProductsDataSource();
    this.calculateTotals();
  }
  
  removeProduct(index: number): void {
    this.products.removeAt(index);
    this.calculateTotals();
    this.updateProductsDataSource(); // <-- أضف هذا السطر
  }

  onProductChange(): void {
    this.calculateTotals();
    this.updateProductsDataSource();
  }

  getProductError(control: AbstractControl | null, fieldName: string): string {
    if (!control || !control.errors) return '';
    
    if (control.hasError('required')) {
      return `${fieldName} مطلوب`;
    }
    if (control.hasError('min')) {
      return `${fieldName} يجب أن يكون ${control.getError('min').min} على الأقل`;
    }
    return 'قيمة غير صالحة';
  }
  // Form Initialization
  private initializeForm(): void {
    this.orderForm = this.fb.group({
      orderTypes: [OrderType.Pickup, Validators.required],
      isOutOfCityShipping: [false],
      ShippingId: [null, Validators.required],
      paymentType: [PaymentType.Collectible, Validators.required],
      branch: [null, Validators.required],
      region: [null, Validators.required],
      city: [null, Validators.required],
      totalWeight: [0, [Validators.required, Validators.min(0.1)]],
      merchantName: [null, Validators.required],
      merchantId: [null] , // يمكن أن يكون اختياريًا الآن  
      orderCost: [0, [Validators.required, Validators.min(0.01)]],
      customerName: ['', [Validators.required, Validators.minLength(3)]],
      customerPhone1: ['', [Validators.required, Validators.pattern(/^01[0125][0-9]{8}$/)]],
      customerPhone2: ['', [Validators.pattern(/^01[0125][0-9]{8}$/)]],
      customerAddress: ['', Validators.required],
      customerEmail: ['', [Validators.email]],
      products: this.fb.array([this.createProductFormGroup()])
    });
  
    this.orderForm.get('region')?.valueChanges.subscribe(regionId => {
      this.onRegionChange(regionId);
    });
  }
 
  private loadOrderData(orderId: number): void {
    this.isLoading = true;
    
    forkJoin({
      merchants: this._unitOfWork.Merchant.getAll(),
      order: this._unitOfWork.Order.getOrderForUpdate(orderId)
    }).subscribe({
      next: ({merchants, order}) => {
        this.merchants = merchants;
        this.populateForm(order);
        this.isLoading = false;
      },
      error: (err) => this.handleLoadError(err)
    });
  }
  trackByFn(index: number, item: AbstractControl): number {
    return index;
  }
  private handleLoadError(err: any): void {
    console.error('Error loading order data:', err);
    this.toastr.error('فشل تحميل بيانات الطلب', 'خطأ');
    this.isLoading = false;
    this.router.navigate(['/orders']);
  }

  private updateProductsDataSource(): void {
    this.productsDataSource.data = this.products.controls;
    this.cdr.detectChanges(); // تأكد من اكتشاف التغييرات
  }
  private populateForm(order: UpdateOrderDto): void {
   // تحقق مما إذا كان التاجر غير معروف
   const cleanMerchantId = order.merchantId?.trim();
   const cleanMerchantName = order.merchantName?.trim();
 
   // البحث عن التاجر مع مراعاة المسافات
   const merchant = this.merchants.find(m => 
     m.name.trim() === cleanMerchantId || 
     m.name.trim() === cleanMerchantName
   );
 
   console.log('Cleaned Merchant Data:', {
     fromServer: {
       merchantId: cleanMerchantId,
       merchantName: cleanMerchantName
     },
     foundMerchant: merchant?.name
   });
    // تعبئة البيانات الأساسية
    this.orderForm.patchValue({
      orderTypes: order.orderTypes,
      isOutOfCityShipping: order.isOutOfCityShipping,
      ShippingId: order.shippingId,
      paymentType: order.paymentType,
      branch: order.branch,
      region: order.region,
      city: order.city,
      totalWeight: order.totalWeight,
      merchantName: merchant?.name || this.merchants[0]?.name,
      merchantId: merchant?.id || null,
      orderCost: order.orderCost,
      customerName: order.customerName,
      customerPhone1: order.customerPhone1,
      customerPhone2: order.customerPhone2 || '',
      customerAddress: order.customerAddress,
      customerEmail: order.customerEmail || ''
    });
  
    // تحميل المدن بناءً على المنطقة المحددة
    if (order.region) {
      this.loadCities(Number(order.region));
    }
  
    // تعبئة بيانات المنتجات
    this.products.clear();
    order.products.forEach(product => {
      const productGroup = this.fb.group({
        name: [product.name, Validators.required],
        weight: [product.weight, [Validators.required, Validators.min(0.1)]],
        quantity: [product.quantity, [Validators.required, Validators.min(1)]]
      });
      this.products.push(productGroup);
    });
    
    this.updateProductsDataSource();
  }
  compareMerchants(a: any, b: any): boolean {
    // مقارنة الأسماء مباشرةً
    return a?.toString()?.toLowerCase() === b?.toString()?.toLowerCase();
  }
  compareMerchantsByName(a: string, b: string): boolean {
    if (!a || !b) return false;
    return a.trim().toLowerCase() === b.trim().toLowerCase();
  }
  isMerchantExists(): boolean {
    const currentMerchantName = this.orderForm.get('merchantName')?.value?.trim();
    return !this.merchants.some(m => m.name.trim() === currentMerchantName);
  }
  private addProductWithData(product: OrderProduct): void {
    const productGroup = this.fb.group({
      name: [product.name, Validators.required],
      weight: [product.weight, [Validators.required, Validators.min(0.1)]],
      quantity: [product.quantity, [Validators.required, Validators.min(1)]]
    });
    
    this.products.push(productGroup);
    this.updateProductsDataSource();
  }


  // Form Array Handling
  get products(): FormArray {
    return this.orderForm.get('products') as FormArray;
  }
  

  logFormErrors() {
    Object.keys(this.orderForm.controls).forEach(key => {
      const controlErrors = this.orderForm.get(key)?.errors;
      if (controlErrors) {
        console.error('Control:', key, 'Errors:', controlErrors);
      }
    });
  }


  private createProductFormGroup(): FormGroup {
    return this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(100)]],
      weight: [0.1, [Validators.required, Validators.min(0.1), Validators.max(100)]],
      quantity: [1, [Validators.required, Validators.min(1), Validators.max(1000)]]
    });
  }
  translateTypeName(englishName: string): string {
    const translations: Record<string, string> = {
      'Standard': 'طلب عادي',
      'Express': 'طلب سريع',
      'Overnight': 'طلب مخصص'
    };
    return translations[englishName] || englishName;
  }
  // Product Management
  // addProduct(): void {
  //   this.products.push(this.createProductFormGroup());
  //   this.calculateTotals();
  // }


  // Calculations
  public calculateTotals(): void {
    const totalWeight = this.products.controls.reduce((acc, product) => {
      return acc + (+product.get('weight')?.value * +product.get('quantity')?.value || 0);
    }, 0);

    const orderCost = this.products.controls.reduce((acc, product) => {
      return acc + (+product.get('weight')?.value * +product.get('quantity')?.value * 10) || 0;
    }, 0);

    this.orderForm.patchValue({
      totalWeight: totalWeight.toFixed(2),
      orderCost: orderCost.toFixed(2)
    });
  }
 
  // Form Submission
  onSubmit(): void {
    const formValue = this.orderForm.value;
    const merchantData = formValue.merchantName === 'Unknown Merchant' ? {
      merchantId: 'Unknown Merchant',
      merchantName: 'Unknown Merchant'
    } : {
      merchantId: this.merchants.find(m => m.name === formValue.merchantName)?.id || formValue.merchantName,
      merchantName: formValue.merchantName
    };
  
    if (!this.orderForm.value.merchantName && this.orderForm.value.merchantId) {
      const selectedMerchant = this.merchants.find(m => m.id === this.orderForm.value.merchantId);

      if (selectedMerchant) {
        this.orderForm.patchValue({ merchantName: selectedMerchant.name });
      }
    }
  
    if (this.orderForm.invalid) {
      this.markFormGroupTouched(this.orderForm);
      this.logFormErrors();
      this.toastr.error('يرجى ملء جميع الحقول المطلوبة بشكل صحيح', 'خطأ');
      return;
    }
  
    this.isLoading = true;

    // تحضير بيانات المنتجات
    const products: ProductDTO[] = formValue.products.map((product: any) => ({
      name: product.name,
      weight: +product.weight,
      quantity: +product.quantity
    }));
    // تحضير بيانات الطلب
    const orderData = {
      ...(this.mode === 'edit' && { id: this.orderId }),
      ...merchantData,
      orderTypes: +formValue.orderTypes,
      isOutOfCityShipping: formValue.isOutOfCityShipping,
      shippingId: +formValue.ShippingId,
      paymentType: +formValue.paymentType,
      branch: +formValue.branch,
      region: +formValue.region,
      city: +formValue.city,
      totalWeight: +formValue.totalWeight,
      merchantId:  formValue.merchantName, // نرسل الاسم كـ merchantId
      merchantName: formValue.merchantName,
      orderCost: +formValue.orderCost,
      customerName: formValue.customerName,
      customerPhone1: formValue.customerPhone1,
      customerPhone2: formValue.customerPhone2 || '',
      customerAddress: formValue.customerAddress,
      customerEmail: formValue.customerEmail || '',
      products: products
    };
  
    if (this.mode === 'create') {
      this.createOrder(orderData as CreateOrderDto);
    } else {
      this.updateOrder(orderData as UpdateOrderDto);
    }
  }
  private createOrder(orderData: CreateOrderDto): void {
    this._unitOfWork.Order.createOrder(orderData).subscribe({
      next: (createdOrder) => {
        this.toastr.success('تم إضافة الطلب بنجاح', 'نجاح');
        this.router.navigate(['/orders', createdOrder.id]);
      },
      error: (err) => {
        this.isLoading = false;
        this.handleOrderError(err, 'إضافة');
      }
    });
  }

  private updateOrder(orderData: UpdateOrderDto): void {
    this.isLoading = true;
    
    this._unitOfWork.Order.updateOrder(orderData).subscribe({
      next: (updatedOrder) => {
        this.toastr.success('تم تحديث الطلب بنجاح', 'نجاح');
        this.router.navigate(['/orders', updatedOrder.id]);
      },
      error: (err) => {
        this.isLoading = false;
        this.handleOrderError(err, 'تحديث');
      }
    });
  }
  
private handleOrderError(error: any, operation: string): void {
  console.error(`${operation} error:`, error);
  
  // معالجة أخطاء التحقق من الصحة من الخادم
  if (error.status === 400 && error.error?.errors) {
    const validationErrors = error.error.errors;
    
    // معالجة أخطاء المنتجات
    if (validationErrors.Products) {
      validationErrors.Products.forEach((productError: any, index: number) => {
        if (this.products.length > index) {
          Object.keys(productError).forEach(key => {
            this.products.at(index).get(key)?.setErrors({
              serverError: productError[key]
            });
          });
        }
      });
    }
    
    // معالجة الأخطاء العامة
    Object.keys(validationErrors).forEach(key => {
      if (key !== 'Products' && this.orderForm.get(key)) {
        this.orderForm.get(key)?.setErrors({
          serverError: validationErrors[key][0]
        });
      }
    });
    
    this.toastr.error('يوجد أخطاء في البيانات المرسلة', 'خطأ في التحقق');
  } else {
    this.toastr.error(`فشل ${operation} الطلب`, 'خطأ');
  }
}

  private loadBranches(): void {
    this._unitOfWork.Branch.getAll().subscribe({
      next: (branches) => {
        console.log("breansh hereeee+"+branches);
        this.branches = branches;
       
      },
      error: (err) => this.showError('فشل تحميل الفروع', err)
    });
  }
  private loadRegions(): void {
    this._unitOfWork.Region.getAll().subscribe({
      next: (regions) => this.regions = regions,
      error: (err) => this.showError('فشل تحميل المناطق', err)
    });
  }

  private loadCities(regionId: number): void {
    this.isLoadingCities = true;
    this._unitOfWork.City.getByRegionId(regionId).subscribe({
      next: (data) => {
        this.cities = data;
        setTimeout(() => {
          this.isLoadingCities = false; // Defer the change to avoid ExpressionChangedAfterItHasBeenCheckedError
          this.orderForm.get('city')?.enable();
        });
      },
      error: (err) => {
        this.toastr.error('Failed to load cities');
        setTimeout(() => {
          this.isLoadingCities = false; // Defer the change
        });
      }
    });
  }

  private loadShippingTypes(): void {
      this._unitOfWork.ShippingType.getAll().subscribe({
      next: (shippingTypes: ShippingType[]) => this.shippingTypes = shippingTypes,
      error: (err: any) => this.showError('فشل تحميل أنواع الشحن', err)
    });

  }
  private loadMerchants(): void {
  
      this._unitOfWork.Merchant.getAll().subscribe({
        next: (merchants) => {console.log(merchants),
          this.merchants = merchants},
        error: (err) => this.showError('فشل تحميل التجار', err)
      });
    }

    private onRegionChange(regionId: number): void {
      this.orderForm.patchValue({ city: '' });
      
      if (regionId) {
        this.orderForm.get('city')?.disable();
        this._unitOfWork.City.getByRegionId(regionId).subscribe({
          next: (cities) => {
            this.cities = cities;
            this.orderForm.get('city')?.enable();
          },
          error: (err) => {
            this.toastr.error('فشل تحميل المدن', 'خطأ');
            this.orderForm.get('city')?.enable();
          }
        });
      } else {
        this.cities = [];
        this.orderForm.get('city')?.enable();
      }
    }
    
  // User Role Handling
  private checkUserRole(): void {
    this._unitOfWork.Employee.getRoles().subscribe((roles: Role[]) => {
      this.isEmployee = roles.some(role => role.roleName === 'Employee');
      if (!this.isEmployee) {
        this.loadCurrentMerchant();
      }
    });
  }

  private loadCurrentMerchant(): void {
    this._unitOfWork.Merchant.getAll().subscribe({
      next: (merchants) => this.merchants = merchants,
        error: (err) => this.showError('فشل تحميل التجار', err)
      });
 
  }

  // Helper Methods
  getPaymentTypeLabel(type: number): string {
    return this.paymentTypes.find(t => t.value === type)?.label || 'غير معروف';
  }

  private markFormGroupTouched(formGroup: FormGroup | FormArray) {
    Object.values(formGroup.controls).forEach(control => {
      control.markAsTouched();
      if (control instanceof FormGroup || control instanceof FormArray) {
        this.markFormGroupTouched(control);
      }
    });
  }
  validateProducts(): boolean {
    if (this.products.length === 0) {
      this.toastr.error('يجب إضافة منتج واحد على الأقل', 'خطأ');
      return false;
    }
  
    let isValid = true;
    this.products.controls.forEach((product, index) => {
      if (product.invalid) {
        isValid = false;
        this.markFormGroupTouched(product as FormGroup);
        this.toastr.error(`يوجد خطأ في المنتج رقم ${index + 1}`, 'خطأ');
      }
    });
    return isValid;
  }
  private showError(message: string, error: any): void {
    console.error(error);
    this.toastr.error(message, 'خطأ');
  }

  onMerchantSelect(event: any): void {
    const selectedMerchant = this.merchants.find(m => m.id === event.target.value);
    if (selectedMerchant) {
      this.orderForm.patchValue({
        merchantId: selectedMerchant.id,
        merchantName: selectedMerchant.name
      });
      this.orderForm.get('merchantName')?.markAsTouched(); // إضافة هذه السطر
    }
  }
}