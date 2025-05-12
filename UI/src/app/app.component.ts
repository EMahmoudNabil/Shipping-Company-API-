import { Component } from '@angular/core';
// import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterOutlet } from '@angular/router';
import { LoadingComponent } from './shared/components/loading/loading.component';
import { NavbarComponent } from "./shared/components/navbar/navbar.component";
import { LoginComponent } from "./shared/components/login/login.component";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
// import { AddCityComponent } from "./shared/components/add-city/add-city.component";


@Component({
  selector: 'app-root',
  imports: [RouterOutlet, LoadingComponent] ,
  standalone: true,
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',

})
export class AppComponent {
  title = 'shipping-app';
}
