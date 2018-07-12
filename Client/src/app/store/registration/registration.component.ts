import { Component } from '@angular/core';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
    selector: 'godsend-registration',
    templateUrl: './registration.component.html',
    styleUrls: ['./registration.component.css']
})
export class RegistrationComponent {
    name = '';
    email = '';
    pass = '';
    constructor(private auth: AuthenticationService) { }
}
