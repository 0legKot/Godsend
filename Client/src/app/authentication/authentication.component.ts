import { Component } from '@angular/core';
import { AuthenticationService } from './authentication.service';

@Component({
    templateUrl: 'authentication.component.html',
    selector: 'godsend-auth'
})
export class AuthenticationComponent {
    name = '';
    password = '';

    constructor(public authService: AuthenticationService) { }

    showError = false;

    login() {
        this.showError = false;
        this.authService.login(this.name, this.password);
        // .subscribe(result => {
        //     this.showError = !result;
        // });
    }
}
