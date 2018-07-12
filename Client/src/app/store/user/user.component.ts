import { Component } from '@angular/core';
import { AuthenticationService } from '../../services/authentication.service';
import { IdentityUser } from '../../models/user.model';

@Component({
    selector: 'godsend-user',
    templateUrl: './user.component.html',
    styleUrls: ['./user.component.css']
})
export class UserComponent {
    name = 'NoName';
    constructor(private auth: AuthenticationService) { }
}
