import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../auth/services/auth.service';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-user-sidebar',
  imports: [RouterLink, HttpClientModule],
  providers: [AuthService],
  templateUrl: './user-sidebar.component.html',
  styleUrl: './user-sidebar.component.css'
})
export class UserSidebarComponent {
  memberEmail: string = '';
  
  constructor(private authService: AuthService, private router:Router) { 
    
  }

  ngOnInit() {
    this.memberEmail = localStorage.getItem('user-email') || '';
  }
  logout(): void {
    this.authService.logout();
    this.router.navigate(['/']);
  }

}
