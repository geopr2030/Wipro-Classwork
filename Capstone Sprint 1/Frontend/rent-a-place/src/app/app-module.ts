import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { authInterceptor } from './interceptors/auth.interceptor';
import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import { HomeComponent } from './components/home/home';
import { LoginComponent } from './components/login/login';
import { RegisterComponent } from './components/register/register';
import { PropertyListingsComponent } from './components/property-listings/property-listings';
import { PropertyDetailsComponent } from './components/property-details/property-details';
import { CreatePropertyComponent } from './components/create-property/create-property';
import { UserProfileComponent } from './components/user-profile/user-profile';
import { MyReservationsComponent } from './components/my-reservations/my-reservations.component';
import { MyListingsComponent } from './components/my-listings/my-listings.component';
import { AdminDashboardComponent } from './components/admin-dashboard/admin-dashboard.component';

@NgModule({
  declarations: [
    App,
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    PropertyListingsComponent,
    PropertyDetailsComponent,
    CreatePropertyComponent,
    UserProfileComponent,
    MyReservationsComponent,
    MyListingsComponent,
    AdminDashboardComponent,
  ],
  imports: [BrowserModule, AppRoutingModule, FormsModule],
  providers: [provideHttpClient(withInterceptors([authInterceptor]))],
  bootstrap: [App],
})
export class AppModule {}
