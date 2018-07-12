import { Component } from '@angular/core';
import { AuthenticationService } from '../../services/authentication.service';
import { Router } from '@angular/router';

@Component({
    selector: 'godsend-nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent {
    showMenuMobile = false;
    constructor(private auth: AuthenticationService, private router: Router) { }
    scrollToTop(): void {
        window.scrollTo(0, 0);
    }

    slideToggle(): void {
        this.showMenuMobile = !this.showMenuMobile;
    }

    hideMenu(): void {
        this.showMenuMobile = false;
    }
    get name() { return this.auth.name; }
    get isLogged() { return this.auth.authenticated; }
    get isAdmin() { return this.auth.authenticated; }
    logout() {
        this.auth.logout();
    }

    login() {
        this.auth.callbackUrl = this.router.routerState.snapshot.url;
        this.router.navigateByUrl('/login');
    }
}
