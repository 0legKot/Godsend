import { Component } from '@angular/core';

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent {
    userData: DummyUserData = { name: 'Admin', isAdmin: true };
    showMenuMobile = false;

    scrollToTop(): void {
        window.scrollTo(0, 0);
    }

    slideToggle(): void {
        this.showMenuMobile = !this.showMenuMobile;
    }

    hideMenu(): void {
        this.showMenuMobile = false;
    }

    logout() {
        this.userData = {};
    }
}

class DummyUserData {
    name?: string;
    isAdmin?: boolean;
}
