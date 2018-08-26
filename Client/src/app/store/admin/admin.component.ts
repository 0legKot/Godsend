import { Component } from '@angular/core';
import { AuthenticationService } from '../../services/authentication.service';
import { OnInit } from '@angular/core';
import { IdentityUser } from '../../models/user.model';
import { DataService } from '../../services/data.service';

@Component({
    selector: 'godsend-admin',
    templateUrl: './admin.component.html',
    styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {
    userList: IdentityUser[] = [];
    ngOnInit(): void {
        this.data.sendRequest<IdentityUser[]>('get', 'api/account/getuserlist/1/10')
            .subscribe(res => this.userList = res);
    }
    constructor(private auth: AuthenticationService, private data: DataService) { }

}
