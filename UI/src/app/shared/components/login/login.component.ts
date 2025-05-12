import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule, FormControl } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../../core/services/auth.service';
import {jwtDecode} from 'jwt-decode';
import { MatFormFieldModule } from '@angular/material/form-field'; // Add this line
import { MatInputModule } from '@angular/material/input'; // Add this line
import { MatButtonModule } from '@angular/material/button'; // Add this line
import { MatIconModule } from '@angular/material/icon'; // Add this line
import { MatCardModule } from '@angular/material/card'; // Add this line
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner'; // Add this line
import { MatSelectModule } from '@angular/material/select'; // Add this line
import { MatCheckboxModule } from '@angular/material/checkbox'; // Add this line
import { MatDividerModule } from '@angular/material/divider'; // Add this line
import { MatTooltipModule } from '@angular/material/tooltip'; // Add this line
import { MatSnackBar } from '@angular/material/snack-bar'; // Add this line
import { MatSidenavModule } from '@angular/material/sidenav'; // Add this line
import { MatToolbarModule } from '@angular/material/toolbar'; // Add this line
import { MatListModule } from '@angular/material/list'; // Add this line
@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatProgressSpinnerModule,
    MatSelectModule,
    MatCheckboxModule,
    MatDividerModule,
    MatTooltipModule,
    MatSidenavModule,
    MatToolbarModule,
    MatListModule
  ],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  loading = false;
  showPassword = false;
  loginForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });

    // Check for existing token
    const token = localStorage.getItem('authToken');
    if (token) {
      try {
        const decoded = jwtDecode(token);
        const decodedToken = decoded as { exp: number; permissions: string[], roles: string[] };
        
        // Check if token is expired
        if (decodedToken.exp * 1000 > Date.now()) {
          // Token is still valid, update localStorage
          localStorage.setItem('permissions', JSON.stringify(decodedToken.permissions));
          localStorage.setItem('roles', JSON.stringify(decodedToken.roles));
          this.router.navigate(['/Dashboard']);
        } else {
          // Token is expired, clear localStorage
          localStorage.clear();
        }
      } catch (error) {
        // Invalid token, clear localStorage
        localStorage.clear();
      }
    }
  }

  togglePasswordVisibility(): void {
    this.showPassword = !this.showPassword;
  }

  onSubmit(): void {
    if (this.loginForm.invalid) {
      return;
    }

    this.loading = true;

    // Clear all localStorage data before login
    localStorage.clear();

    this.authService.login(this.loginForm.value).subscribe({
      next: (res) => {
        if (res && res.token) {
          // Store token and decode it
          localStorage.setItem('authToken', res.token);
          const decoded = jwtDecode(res.token);
          const decodedToken = decoded as { exp: number; permissions: string[], roles: string[] };

          // Store user data
          localStorage.setItem('permissions', JSON.stringify(decodedToken.permissions));
          localStorage.setItem('roles', JSON.stringify(decodedToken.roles));
          localStorage.setItem('fullName', res.fullName);

          this.toastr.success(`مرحبًا ${res.fullName}!`, 'تم تسجيل الدخول بنجاح');
          this.router.navigate(['/dashboard']);
        } else {
          this.toastr.error('لم يتم استلام رمز المصادقة.', 'خطأ');
        }
        this.loading = false;
      },
      error: (err) => {
        this.toastr.error(err.error?.message || 'فشل في تسجيل الدخول', 'خطأ');
        this.loading = false;
      },
    });
  }
}
