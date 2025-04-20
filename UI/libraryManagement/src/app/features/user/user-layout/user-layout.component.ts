import { Component } from '@angular/core';
import { UserSidebarComponent } from '../user-sidebar/user-sidebar.component';
import { RouterOutlet } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-user-layout',
  imports: [UserSidebarComponent,RouterOutlet,HttpClientModule],
  providers:[],
  templateUrl: './user-layout.component.html',
  styleUrl: './user-layout.component.css'
})
export class UserLayoutComponent {

}
