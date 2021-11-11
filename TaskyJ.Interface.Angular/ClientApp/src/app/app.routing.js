"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var router_1 = require("@angular/router");
var home_component_1 = require("./home/home.component");
var admin_component_1 = require("./admin/admin.component");
var login_component_1 = require("./login/login.component");
var auth_guard_1 = require("./guards/auth.guard");
var roletype_1 = require("./models/roletype");
var table_editor_component_1 = require("./ui/table-editor/table-editor.component");
var table_list_component_1 = require("./ui/table-list/table-list.component");
var GenericChild = {
    path: ':id',
    component: table_editor_component_1.TableEditorComponent,
    canActivate: [auth_guard_1.AuthGuard],
    data: { roles: [roletype_1.RoleType.Admin] },
};
var appRoutes = [
    {
        //not locked
        path: '',
        component: login_component_1.LoginComponent
    },
    {
        //not locked
        path: 'login',
        component: login_component_1.LoginComponent
    },
    {
        path: 'home',
        component: home_component_1.HomeComponent,
        canActivate: [auth_guard_1.AuthGuard],
        data: { roles: [roletype_1.RoleType.AnyUser] }
    },
    {
        path: 'admin',
        component: admin_component_1.AdminComponent,
        canActivate: [auth_guard_1.AuthGuard],
        data: { roles: [roletype_1.RoleType.Admin] },
        children: [
            {
                path: 'user',
                component: table_list_component_1.TableListComponent,
                canActivate: [auth_guard_1.AuthGuard],
                data: { roles: [roletype_1.RoleType.Admin] },
                children: [GenericChild]
            },
            {
                path: 'group',
                component: table_list_component_1.TableListComponent,
                canActivate: [auth_guard_1.AuthGuard],
                data: { roles: [roletype_1.RoleType.Admin] },
                children: [GenericChild]
            },
            {
                path: 'secondarygroup',
                component: table_list_component_1.TableListComponent,
                canActivate: [auth_guard_1.AuthGuard],
                data: { roles: [roletype_1.RoleType.Admin] },
                children: [GenericChild]
            },
            {
                path: 'privilege',
                component: table_list_component_1.TableListComponent,
                canActivate: [auth_guard_1.AuthGuard],
                data: { roles: [roletype_1.RoleType.Admin] },
                children: [GenericChild]
            },
            {
                path: 'userprivilege',
                component: table_list_component_1.TableListComponent,
                canActivate: [auth_guard_1.AuthGuard],
                data: { roles: [roletype_1.RoleType.Admin] },
                children: [GenericChild]
            },
            {
                path: 'groupprivilege',
                component: table_list_component_1.TableListComponent,
                canActivate: [auth_guard_1.AuthGuard],
                data: { roles: [roletype_1.RoleType.Admin] },
                children: [GenericChild]
            },
            {
                path: 'category',
                component: table_list_component_1.TableListComponent,
                canActivate: [auth_guard_1.AuthGuard],
                data: { roles: [roletype_1.RoleType.Admin] },
                children: [GenericChild]
            },
        ]
    },
    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];
exports.routing = router_1.RouterModule.forRoot(appRoutes);
//# sourceMappingURL=app.routing.js.map