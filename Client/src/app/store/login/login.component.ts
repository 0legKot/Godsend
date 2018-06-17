import { Component } from '@angular/core';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
    selector: 'godsend-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent {
    name = '';
    pass = '';
    constructor(private auth: AuthenticationService) { }
    login() {
        // this.auth.callbackUrl = '/orders';
        this.auth.login(this.name, this.pass);
    }
}
