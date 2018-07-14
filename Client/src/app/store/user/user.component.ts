import { Component } from '@angular/core';
import { AuthenticationService } from '../../services/authentication.service';
import { IdentityUser } from '../../models/user.model';
import { DataService } from '../../services/data.service';
import { OnInit } from '@angular/core/src/metadata/lifecycle_hooks';

@Component({
    selector: 'godsend-user',
    templateUrl: './user.component.html',
    styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit{
    user: IdentityUser = new IdentityUser();
    ngOnInit(): void {
        this.data.sendRequest<IdentityUser>("get", 'api/account/getprofile/' + this.name).subscribe(
            res => this.user = res
        );
    }
    name = this.auth.name;
    constructor(private auth: AuthenticationService, private data: DataService) { }

}
