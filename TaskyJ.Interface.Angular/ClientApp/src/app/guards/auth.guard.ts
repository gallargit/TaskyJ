import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
    constructor(
        private router: Router,
        private authenticationService: AuthenticationService
    ) { }

    //necessary for tests, authenticator is not injected with useValue
    get getAuthenticator() {
        return this.authenticationService;
    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        if (this.authenticationService === undefined)
            return false;

        const currentUser = this.authenticationService.currentUserValue;

        if (currentUser) {
            // check if route is restricted by role
            if (route.data.roles) {
                    // role not authorised so redirect to home page
                    this.router.navigate(['']);
                    return false;
            }
            // authorised so return true
            return true;
        }

        // not logged in so redirect to login page with the return url
        this.router.navigate(['/login'], { queryParams: { returnUrl: state.url }});
        return false;
    }
}
