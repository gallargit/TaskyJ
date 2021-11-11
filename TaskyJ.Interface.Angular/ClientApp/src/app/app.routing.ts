import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { LogoffComponent } from './logoff/logoff.component';
import { UsersComponent } from './users/users.component';
import { TasklistComponent } from './tasklist/tasklist.component';
import { TaskComponent } from './task/task.component';
import { AuthGuard } from './guards/auth.guard';

const GenericChild = {
    path: ':id', 
    //component: TableEditorComponent,
    canActivate: [AuthGuard], 
    //data: { roles: [RoleType.Admin] },
}

const appRoutes: Routes = [
  {
    //not locked
    path: '',
    canActivate: [AuthGuard],
    component: TasklistComponent
  },
  {
      //not locked
      path: 'login', 
      component: LoginComponent         
  },
  {
      //not locked
      path: 'logoff', 
      component: LogoffComponent         
  },
    {
        path: 'home',
        component: HomeComponent,
        canActivate: [AuthGuard],
    },
    {
        path: 'tasklist',
        component: TasklistComponent,
        canActivate: [AuthGuard],
    },
    {
        path: 'task/:id',
        component: TaskComponent,
        canActivate: [AuthGuard],
    },
    { 
        path: 'admin', 
        component: UsersComponent, 
        canActivate: [AuthGuard], 
        //data: { roles: [RoleType.Admin] },
/*        children: [
            {
                path: 'user',
                //component: TableListComponent,
                canActivate: [AuthGuard], 
                //data: { roles: [RoleType.Admin] },
                children: [GenericChild]
            },
            {
                path: 'group',
                //component: TableListComponent,
                canActivate: [AuthGuard], 
                //data: { roles: [RoleType.Admin] },
                children: [GenericChild]
            },
            {
                path: 'secondarygroup',
                //component: TableListComponent,
                canActivate: [AuthGuard], 
                //data: { roles: [RoleType.Admin] },
                children: [GenericChild]
            },
            {
                path: 'privilege',
                //component: TableListComponent,
                canActivate: [AuthGuard], 
                //data: { roles: [RoleType.Admin] },
                children: [GenericChild]
            },
            {
                path: 'userprivilege',
                //component: TableListComponent,
                canActivate: [AuthGuard], 
                //data: { roles: [RoleType.Admin] },
                children: [GenericChild]
            },
            {
                path: 'groupprivilege',
                //component: TableListComponent,
                canActivate: [AuthGuard], 
                //data: { roles: [RoleType.Admin] },
                children: [GenericChild]
            },
            {
                path: 'category',
                //component: TableListComponent,
                canActivate: [AuthGuard], 
                //data: { roles: [RoleType.Admin] },
                children: [GenericChild]
            },
        ]*/
    },
    // otherwise redirect to home
    { path: '**', redirectTo: '' }
]

export const routing = RouterModule.forRoot(appRoutes)
