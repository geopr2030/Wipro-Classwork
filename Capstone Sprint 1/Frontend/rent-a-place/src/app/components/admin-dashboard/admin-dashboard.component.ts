import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../services/admin.service';

@Component({
  selector: 'app-admin-dashboard',
  standalone: false,
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {
  users: any[] = [];
  properties: any[] = [];
  isLoadingUsers = true;
  isLoadingProperties = true;
  activeTab = 'users'; // 'users' or 'properties'

  constructor(private adminService: AdminService) {}

  ngOnInit(): void {
    this.loadUsers();
    this.loadProperties();
  }

  setTab(tab: string) {
    this.activeTab = tab;
  }

  loadUsers() {
    this.isLoadingUsers = true;
    this.adminService.getAllUsers().subscribe({
      next: (data) => {
        this.users = data;
        this.isLoadingUsers = false;
      },
      error: (err) => {
        console.error('Failed to load users:', err);
        this.isLoadingUsers = false;
      }
    });
  }

  loadProperties() {
    this.isLoadingProperties = true;
    this.adminService.getAllProperties().subscribe({
      next: (data) => {
        this.properties = data;
        this.isLoadingProperties = false;
      },
      error: (err) => {
        console.error('Failed to load properties:', err);
        this.isLoadingProperties = false;
      }
    });
  }

  deleteUser(userId: string) {
    if (confirm('Are you sure you want to delete this user? All their properties and reservations will also be deleted.')) {
      this.adminService.deleteUser(userId).subscribe({
        next: () => this.loadUsers(),
        error: (err) => alert('Failed to delete user.')
      });
    }
  }

  deleteProperty(propertyId: number) {
    if (confirm('Are you sure you want to delete this property?')) {
      this.adminService.deleteProperty(propertyId).subscribe({
        next: () => this.loadProperties(),
        error: (err) => alert('Failed to delete property.')
      });
    }
  }
}
