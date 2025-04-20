import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AdminSidebarComponent } from '../admin-sidebar/admin-sidebar.component';
import { AuthService } from '../../auth/services/auth.service';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-admin-layout',
  standalone: true,
  providers: [AuthService],
  imports: [RouterOutlet, AdminSidebarComponent, HttpClientModule],
  templateUrl: './admin-layout.component.html',
  styleUrl: './admin-layout.component.css'
})
export class AdminLayoutComponent implements OnInit{
  
  constructor(private authService: AuthService) { }
   ngOnInit(): void {
       this.authService.user().subscribe({
        next: (response) => {
          console.log(response);
        }
       })
   }
    
  
}