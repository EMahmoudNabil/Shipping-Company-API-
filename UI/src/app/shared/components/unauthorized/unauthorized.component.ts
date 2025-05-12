// unauthorized.component.ts
import { Component } from '@angular/core';

@Component({
  selector: 'app-unauthorized',
  template: `
    <div class="container text-center mt-5">
      <h1 class="text-danger"><i class="fas fa-ban"></i> 403 - غير مصرح به</h1>
      <p class="lead">ليس لديك الصلاحيات الكافية للوصول إلى هذه الصفحة</p>
      <a routerLink="/dashboard" class="btn btn-primary">العودة للصفحة الرئيسية</a>
    </div>
  `,
  styles: []
})
export class UnauthorizedComponent {}