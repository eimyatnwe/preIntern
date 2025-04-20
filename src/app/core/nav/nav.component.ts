import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { AuthService } from '../../features/auth/services/auth.service';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-nav',
  imports: [RouterLink,HttpClientModule],
  providers: [AuthService],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent implements OnInit{
  constructor(private authService: AuthService){}

  ngOnInit(): void {
      this.authService.user()
      .subscribe({
        next: (response) => {
          console.log(response);
        }
      })
  }
}
