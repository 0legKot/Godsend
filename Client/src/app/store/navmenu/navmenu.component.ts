import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { StorageService } from '../../services/storage.service';
import { AuthenticationService } from '../../services/authentication.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
    selector: 'godsend-nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent {
    showMenuMobile = false;
    constructor(private storage: StorageService, private auth: AuthenticationService, private router: Router, private translate: TranslateService) {
        translate.setDefaultLang('ru');
    }
    scrollToTop(): void {
        window.scrollTo(0, 0);
    }

    slideToggle(): void {
        this.showMenuMobile = !this.showMenuMobile;
    }

    hideMenu(): void {
        this.showMenuMobile = false;
    }
    get name() { return this.storage.name; }
    get isLogged() { return this.storage.authenticated; }
    get isAdmin() { return this.storage.authenticated; }
    logout() {
        this.auth.logout();
    }

    login() {
        this.auth.callbackUrl = this.router.routerState.snapshot.url;
        this.router.navigateByUrl('/login');
    }
}
