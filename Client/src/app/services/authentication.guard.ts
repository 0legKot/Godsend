import { Injectable } from '@angular/core';
import { Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthenticationService } from './authentication.service';
import { StorageService } from './storage.service';

@Injectable({
    providedIn: 'root'
})
export class AuthenticationGuard {
    constructor(private router: Router,
        private authService: AuthenticationService,
        private storage: StorageService) { }

    canActivate(route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): boolean {
        if (this.storage.authenticated) {
            return true;
        } else {
            this.authService.callbackUrl = '/' + route.url.toString(); // mb /admin
            this.router.navigateByUrl('/login');
            return false;
        }
    }
}
