import { Component, EventEmitter, Output } from "@angular/core";
import { RouterLink } from "@angular/router";

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'], // Fixed typo here
})
export class NavbarComponent {
  @Output() sidebarToggle = new EventEmitter();
  toggleSidebar(icon: HTMLElement) {
    this.sidebarToggle.emit();
  }
}