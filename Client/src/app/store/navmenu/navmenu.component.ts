import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { StorageService } from '../../services/storage.service';
import { AuthenticationService } from '../../services/authentication.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
    selector: 'godsend-nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent implements OnInit {
    constructor(private storage: StorageService,
                private auth: AuthenticationService,
                private router: Router,
                private translateService: TranslateService) { }
    get name() { return this.storage.name; }
    get isLogged() { return this.storage.authenticated; }
    get isAdmin() { return Boolean(this.auth.roles.find(x => x == 'Administrator' || x == 'Moderator')); }
    get currentUserId() { return this.storage.id; }
    showMenuMobile = false;

    availableLangs?: string[];
    selectedLang?: string;
    scrollToTop(): void {
        window.scrollTo(0, 0);
    }

    slideToggle(): void {
        this.showMenuMobile = !this.showMenuMobile;
    }

    hideMenu(): void {
        this.showMenuMobile = false;
    }

    ngOnInit() {
        this.selectedLang = this.translateService.currentLang;
        this.availableLangs = this.translateService.getLangs();
    }

    changeLang(newLang: string) {
        console.log('Changing lang to ' + newLang);
        this.translateService.use(newLang);
    }

    logout() {
        this.auth.logout();
    }

    login() {
        this.auth.callbackUrl = this.router.routerState.snapshot.url;
        this.router.navigateByUrl('/login');
    }
}
