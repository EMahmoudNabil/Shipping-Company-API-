
import { Component, ElementRef, EventEmitter, HostListener, OnInit, Output, ViewChild } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';

import { CommonModule } from '@angular/common';

import { AuthService } from '../../../core/services/auth.service';
import { RoleService } from '../../../core/services/role.service';

@Component({
  selector: 'app-side-nav',
  standalone: true,
  imports: [RouterLink, RouterLinkActive, CommonModule],
  templateUrl: './side-nav.component.html',
  styleUrl: './side-nav.component.css',
})
export class SideNavComponent {
  @ViewChild('header') header!: ElementRef;
  @ViewChild('headerToggleBtn') headerToggleBtn!: ElementRef;
  isSidebarOpen = false;
  // role = 'Admin'; // يمكنك تغيير هذه القيمة بناءً على صلاحيات المستخدم
  role: string = "";
  arrfromlocal: string[] | null = null; // Type should be string[] | null to reflect the array of strings
  userPermissions: string[] = [];
  
  constructor(
 
    private router: Router,
    private auth: AuthService,
    private roleService: RoleService
  ) {
   
  }

  ngOnInit(): void {
    this.loadPermissions();
  }
  loadPermissions(): void {
    this.userPermissions = this.auth.getPermissions();
    if (this.userPermissions.length === 0) {
      this.roleService.getCurrentUserPermissions().subscribe({
        next: (permissions) => {
          this.userPermissions = permissions;
          this.auth.setPermissions(permissions);
        },
        error: (err) => console.error('Failed to load permissions', err)
      });
    }
  }

    // دالة للتحقق من الصلاحية
    hasPermission(permission: string): boolean {
      return this.userPermissions.includes(permission);
    }
  headerToggle() {
    this.header.nativeElement.classList.toggle('header-show');
    this.headerToggleBtn.nativeElement.classList.toggle('bi-list');
    this.headerToggleBtn.nativeElement.classList.toggle('bi-x');
  }
  toggleDropdown(e: Event, element: HTMLElement) {
    e.preventDefault();
    element.parentElement?.classList.toggle('active');
    element.parentElement?.nextElementSibling?.classList.toggle(
      'dropdown-active'
    );
    e.stopImmediatePropagation();
  }

  logout() {
    this.auth.logout();
  }



}