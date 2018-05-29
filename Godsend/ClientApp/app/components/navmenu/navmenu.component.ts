import { Component } from '@angular/core';

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent {

    userData: DummyUserData = { name: "Admin", isAdmin: true};

    scrollToTop(): void {
        window.scrollTo(0, 0);
    }

    logout() {
        this.userData = {};
    }
}

class DummyUserData {   
    name?: string;
    isAdmin?: boolean;
}
