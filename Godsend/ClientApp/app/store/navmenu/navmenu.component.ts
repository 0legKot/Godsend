import { Component } from '@angular/core';

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent {

    userData: DummyUserData = { name: "Admin", isAdmin: true };
    

    scrollToTop(): void {
        window.scrollTo(0, 0);
    }
    bool = true;
    slideToggle(): void {
        let tmp = document.getElementById('_nav');
        if (tmp) {
            if (this.bool) {
                tmp.style.display = "block";
                this.bool = false;
            }
            else if (this.bool == false){
                tmp.style.display = "none";
                this.bool = true;
            }
            console.log(this.bool);
        }
    }
    logout() {
        this.userData = {};
    }
}

class DummyUserData {   
    name?: string;
    isAdmin?: boolean;
}
