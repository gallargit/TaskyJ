import { async, TestBed } from '@angular/core/testing';
//import { MockAuthenticationService } from '../services/mock/mock.authentication.service';
import { AuthGuard } from './auth.guard';
import { FormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { CommonModule } from '@angular/common';
import { Router, RouterStateSnapshot, ActivatedRouteSnapshot, UrlSegment } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';
import { RouterTestingModule } from '@angular/router/testing';
//import { RoleType } from '../models/roletype';

describe('AuthGuard', () => {
    let authGuard: AuthGuard;
    //let mockAuthenticationService: MockAuthenticationService;
    
    //let storageService: StorageService;
   let router = {
        navigate: jasmine.createSpy('navigate')
    };

    // async beforeEach
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [ RouterTestingModule.withRoutes([
                { path: 'admin',
//                    component: AdminComponent,
                    canActivate: [AuthGuard],
                    //data: { roles: [RoleType.Admin] }
                }
              ]), HttpClientTestingModule, FormsModule, CommonModule ],
            providers: [AuthGuard, 
               // { provide: AuthenticationService, useClass : MockAuthenticationService },
                { provide: Router, useValue: router }
            ]
        })
        .compileComponents();
        //mockAuthenticationService = TestBed.get(MockAuthenticationService);
        authGuard = TestBed.get(AuthGuard);
    }));

    it('should not be able to enter admin route no user is logged in', () => {
        authGuard.getAuthenticator.login("undefined", "undefined").subscribe(
            user => {
                let mockSnapshot: any = jasmine.createSpyObj<RouterStateSnapshot>("RouterStateSnapshot", ['toString']);
                var route = new ActivatedRouteSnapshot();
                route.url = [ new UrlSegment('admin', {}) ];
                route.data = { };
                //route.data.roles = [ RoleType.Admin ] ;

                expect(authGuard.canActivate(route, mockSnapshot)).toBe(false);
            }
        );
    });

    it('should be able to enter admin route when admin user is logged in', () => {
        authGuard.getAuthenticator.login("Administrador", "Administrador").subscribe(
            user => {
                let mockSnapshot: any = jasmine.createSpyObj<RouterStateSnapshot>("RouterStateSnapshot", ['toString']);
                var route = new ActivatedRouteSnapshot();
                route.url = [ new UrlSegment('admin', {}) ];
                route.data = { };
                //route.data.roles = [ RoleType.Admin ] ;

                expect(authGuard.canActivate(route, mockSnapshot)).toBe(true);
            }
        );
    });

    it('should be able to enter home route when a user is logged in', () => {
        authGuard.getAuthenticator.login("user", "user").subscribe(
            user => {
                let mockSnapshot: any = jasmine.createSpyObj<RouterStateSnapshot>("RouterStateSnapshot", ['toString']);
                var route = new ActivatedRouteSnapshot();
                route.url = [ new UrlSegment('home', {}) ];
                route.data = { };
                //route.data.roles = [ RoleType.AnyUser ] ;

                expect(authGuard.canActivate(route, mockSnapshot)).toBe(true);
            }
        );
    });

    it('should not be able to enter admin route when non-admin user is logged in', () => {
        authGuard.getAuthenticator.login("user", "user").subscribe(
            user => {
                let mockSnapshot: any = jasmine.createSpyObj<RouterStateSnapshot>("RouterStateSnapshot", ['toString']);
                var route = new ActivatedRouteSnapshot();
                route.url = [ new UrlSegment('admin', {}) ];
                route.data = { };
                //route.data.roles = [ RoleType.Admin ] ;

                expect(authGuard.canActivate(route, mockSnapshot)).toBe(false);
            }
        );
    });

});


/*
describe('AuthGuard', () => {
  let component: AuthGuard;
  let fixture: ComponentFixture<AuthGuard>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdminComponent ],
      imports: [ RouterTestingModule ],
      providers: [ { provide: MockAuthenticationService, useClass: MockAuthenticationService } ]
    })
    .compileComponents().then(() => {
      //localStorage.removeItem('currentUser');
    });
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AuthGuard);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

});*/

  /*it('should display welcome message', () => {
    const fixture = TestBed.createComponent(AdminComponent);
    fixture.detectChanges();
    const compiled = fixture.debugElement.nativeElement;
    expect(compiled.querySelector('h3').textContent).toContain('This page can only be accessed by administrators');
  });*/

  /*it('should display menu', () => {
    const fixture = TestBed.createComponent(AdminComponent);
    fixture.detectChanges();
    let links = fixture.nativeElement.querySelectorAll('a');
    expect(links.length).toBeGreaterThan(5);
    expect(links[0].text).toBe("Usuarios");
    expect(links[1].text).toBe("Grupos");
    expect(links[2].text).toBe("GruposSecundarios");
    expect(links[3].text).toBe("Privilegios");
    expect(links[4].text).toBe("PrivilegiosUsuarios");
    expect(links[5].text).toBe("PrivilegiosGrupos");
    expect(links[6].text).toBe("Categorías");
  });*/

/*
para probar las route guards
https://stackoverflow.com/questions/41218323/how-to-unit-test-canactivate-guard-method-of-angular2-using-jasmine
probar la navegacion
https://codecraft.tv/courses/angular/unit-testing/routing/
  it('should allow administrators users to enter', () => {
    const fixture = TestBed.createComponent(AdminComponent);
    fixture.detectChanges();
    const compiled = fixture.debugElement.nativeElement;
    var authServ = new MockAuthenticationService();
    var user = authServ.login("user", "user");
    console.log('holax');
    console.log(JSON.stringify(user));
    expect(compiled.querySelector('h3').textContent).toContain('This page can only be accessed by administrators');
  });

  it('should not allow regular users to enter', () => {
    const fixture = TestBed.createComponent(AdminComponent);
    fixture.detectChanges();
    const compiled = fixture.debugElement.nativeElement;
    var authServ = new MockAuthenticationService();
    var user = authServ.login("user", "user");
    expect(compiled.querySelector('h3').textContent).toBeNull();
  });
*/



/*import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
    constructor(
        private router: Router,
        private authenticationService: AuthenticationService
    ) {}

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        const currentUser = this.authenticationService.currentUserValue;
        if (currentUser) {
            // check if route is restricted by role
            if (route.data.roles && route.data.roles.indexOf(currentUser.GrupoPrincipal) === -1) {
                // role not authorised so redirect to home page
                this.router.navigate(['/']);
                return false;
            }
            // authorised so return true
            return true;
        }

        // not logged in so redirect to login page with the return url
        this.router.navigate(['/login'], { queryParams: { returnUrl: state.url }});
        return false;
}*/
