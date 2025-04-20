import { Routes } from '@angular/router';
import { NavComponent } from './core/nav/nav.component';
import { AdminLayoutComponent } from './features/admin/admin-layout/admin-layout.component';
import { BookListComponent } from './features/book/book-list/book-list.component';
import { AddBookComponent } from './features/book/add-book/add-book.component';
import { MemberListComponent } from './features/member/member-list/member-list.component';
import { UserLayoutComponent } from './features/user/user-layout/user-layout.component';
import { MemberBookListComponent } from './features/member/book-list/book-list.component';
import { LoginComponent } from './features/auth/login/login.component';
import { RegisterComponent } from './features/auth/register/register.component';
import { BorrowRecordComponent } from './features/borrowRecord/borrow-record/borrow-record.component';
import { UpdateBookComponent } from './features/book/update-book/update-book.component';

export const routes: Routes = [
    {
        path: '',
        component: NavComponent
    },
    {
        path: 'admin',
        component: AdminLayoutComponent,
        children: [
            {
                path: 'book',
                children: [
                    { path: '', component: BookListComponent },
                    { path: 'add', component: AddBookComponent },
                    {
                        path:"edit/:id",
                        component: UpdateBookComponent
                    },
                ]
            },
            
            {
                path: 'member',
                component: MemberListComponent
            },
            {
                path: '',
                redirectTo: 'book',
                pathMatch: 'full'
            }
        ]
    },

    {
        path: 'user',
        component: UserLayoutComponent,
        children: [
            {
                path: 'book',
                component: MemberBookListComponent
            },
            {
                path: 'record',
                component: BorrowRecordComponent
            },
            {
                path: '',
                redirectTo: 'book',
                pathMatch: 'full'
            }
        ]
    },
    {
        path:'login',
        component:LoginComponent
    },{
        path:'register',
        component: RegisterComponent
    }
];
