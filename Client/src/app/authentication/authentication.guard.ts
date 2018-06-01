import { Injectable } from '@angular/core';
import { Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthenticationService } from './authentication.service';

@Injectable()
export class AuthenticationGuard {

    constructor(private router: Router,
        private authService: AuthenticationService) { }

    canActivate(route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): boolean {
        if (this.authService.authenticated) {
            return true;
        } else {
            this.authService.callbackUrl = '/' + route.url.toString(); // mb /admin
            this.router.navigateByUrl('/login');
            return false;
        }
    }
}
