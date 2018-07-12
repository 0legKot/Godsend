import { Component } from '@angular/core';
import { AuthenticationService } from '../../services/authentication.service';
import { IdentityUser } from '../../models/user.model';

@Component({
    selector: 'godsend-registration',
    templateUrl: './registration.component.html',
    styleUrls: ['./registration.component.css']
})
export class RegistrationComponent {
    name = '';
    email = '';
    pass = '';
    birth = '';
    constructor(private auth: AuthenticationService) { }
    register() {
        this.auth.register(new IdentityUser(this.email, this.name, this.birth), this.pass);
    }
}
