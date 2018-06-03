import { Component } from '@angular/core';
import { AuthenticationService } from '../../authentication/authentication.service';

@Component({
    selector: 'godsend-nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent {
    userData: DummyUserData = { name: 'Admin', isAdmin: true };
  showMenuMobile = false;
  constructor(private auth: AuthenticationService) { }
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
  logout() {
    this.auth.logout();
    }
}

class DummyUserData {
    name?: string;
    isAdmin?: boolean;
}
