import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home';
import { LoginComponent } from './components/login/login';
import { RegisterComponent } from './components/register/register';
import { PropertyListingsComponent } from './components/property-listings/property-listings';
import { PropertyDetailsComponent } from './components/property-details/property-details';
import { CreatePropertyComponent } from './components/create-property/create-property';
import { UserProfileComponent } from './components/user-profile/user-profile';
import { AdminDashboardComponent } from './components/admin-dashboard/admin-dashboard.component';
import { MyReservationsComponent } from './components/my-reservations/my-reservations.component';
import { MyListingsComponent } from './components/my-listings/my-listings.component';
import { authGuard } from './guards/auth.guard';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'properties', component: PropertyListingsComponent },
  { path: 'properties/:id', component: PropertyDetailsComponent },
  { 
    path: 'create-property', 
    component: CreatePropertyComponent, 
    canActivate: [authGuard], 
    data: { roles: ['Owner', 'Admin'] } 
  },
  { 
    path: 'profile', 
    component: UserProfileComponent, 
    canActivate: [authGuard] 
  },
  { 
    path: 'admin', 
    component: AdminDashboardComponent, 
    canActivate: [authGuard], 
    data: { roles: ['Admin'] } 
  },
  { 
    path: 'my-reservations', 
    component: MyReservationsComponent, 
    canActivate: [authGuard], 
    data: { roles: ['Renter'] } 
  },
  { 
    path: 'my-listings', 
    component: MyListingsComponent, 
    canActivate: [authGuard], 
    data: { roles: ['Owner'] } 
  },
  { path: '**', redirectTo: '', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
