import { Component } from '@angular/core';
import { LoginRequest } from '../models/login-request.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { HttpClientModule } from '@angular/common/http';
import { CookieService } from 'ngx-cookie-service';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [CommonModule,FormsModule,HttpClientModule,RouterLink],
  providers: [AuthService],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  model: LoginRequest;
  errorMessage: string = '';

  constructor(private authService: AuthService, 
              private cookieService: CookieService, 
              private router: Router) {
    this.model = {
      email: "",
      password: ""
    }
  }

  onFormSubmit(): void {
    this.errorMessage = '';
    console.log('Login attempt:', this.model);
    
    this.authService.login(this.model)
      .subscribe({
        next: (response) => {
          console.log('Login successful:', response);
          this.cookieService.set('Authorization', `Bearer ${response.token}`,
            undefined, '/', undefined, true, 'Strict');
          

          this.authService.setUser({
            email: response.email,
            roles: response.roles
          });

          if (response.roles.includes('Writer')) {
            this.router.navigateByUrl('/admin');
          } else {
            this.router.navigateByUrl('/user');
          }
        },
        error: (error) => {
          console.error('Login failed:', error);
          if (error.error && error.error.errors) {
            const firstError = Object.values(error.error.errors)[0];
            if (Array.isArray(firstError) && firstError.length > 0) {
              this.errorMessage = firstError[0];
            } else {
              this.errorMessage = 'Login failed';
            }
          } else {
            this.errorMessage = 'Login failed';
          }
        }
      });

    
  }

  
}
