import { Component, ViewChild, OnDestroy } from '@angular/core';
import { AuthService } from '../../core/services/auth.service';
import { Router, RouterOutlet } from '@angular/router';
import { SideNavComponent } from "../../shared/components/side-nav/side-nav.component";
import { LoadingComponent } from "../../shared/components/loading/loading.component";
import { NavbarComponent } from "../../shared/components/navbar/navbar.component";
import { CommonModule } from '@angular/common';
import { ChatbotComponent } from "../../shared/components/chatbot/chatbot.component";
import { MatIconModule } from '@angular/material/icon';
import { ChatbotService } from '../../core/services/chatbot.service';
import { Subscription } from 'rxjs';
import { DashboardComponent } from "../../shared/components/dashboard/dashboard.component";

@Component({
  selector: 'app-main-system',
  standalone: true,
  imports: [
    RouterOutlet,
    SideNavComponent,
    MatIconModule,
    LoadingComponent,
    CommonModule,
    NavbarComponent,
    ChatbotComponent,
],
  templateUrl: './main-system.component.html',
  styleUrls: ['./main-system.component.css']
})
export class MainSystemComponent implements OnDestroy {
  @ViewChild(SideNavComponent) sideNav?: SideNavComponent;
  private subscriptions = new Subscription();
  
  sidebarActive = false;
  showChat = false;
  unreadMessages = 0;

  constructor(
    private authService: AuthService,
    private router: Router,
    private chatbotService: ChatbotService,
  ) {
    this.loadChatState();
    this.subscribeToNewMessages();
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  private loadChatState(): void {
    const chatState = localStorage.getItem('chatOpen');
    this.showChat = chatState === 'true';
  }

  private subscribeToNewMessages(): void {
    this.subscriptions.add(
      this.chatbotService.newMessageCount$.subscribe(count => {
        this.unreadMessages = count;
      })
    );
  }

  toggleChat(): void {
    this.showChat = !this.showChat;
    localStorage.setItem('chatOpen', this.showChat.toString());
    
    if (this.showChat) {
      this.unreadMessages = 0;
    }
  }

  onChatClosed(): void {
    this.showChat = false;
    localStorage.setItem('chatOpen', 'false');
  }

  toggleSidebar(): void {
    this.sidebarActive = !this.sidebarActive;
    this.sideNav?.headerToggle();
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }

  // هذه الدوال يمكن حذفها إذا لم تكن مستخدمة
  onAddCityClick(): void {
    console.log('Add city clicked');
  }

  reloadCities(): void {
    console.log('reloadCities called!');
  }
}