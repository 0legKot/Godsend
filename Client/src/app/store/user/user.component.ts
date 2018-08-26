import { Component } from '@angular/core';
import { IdentityUser } from '../../models/user.model';
import { DataService } from '../../services/data.service';
import { OnInit } from '@angular/core/src/metadata/lifecycle_hooks';
import { StorageService } from '../../services/storage.service';

@Component({
    selector: 'godsend-user',
    templateUrl: './user.component.html',
    styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
    user: IdentityUser = new IdentityUser();
    name = this.storage.name;

    constructor(private storage: StorageService, private data: DataService) { }

    ngOnInit(): void {
        this.data.sendRequest<IdentityUser>('get', 'api/account/getprofile/' + this.name).subscribe(
            res => this.user = res
        );
    }
}
