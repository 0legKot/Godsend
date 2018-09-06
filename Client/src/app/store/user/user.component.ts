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
    edit: boolean = false;
    backup: UserBackup = {
        name: '',
        email: '',
        birth: ''
    };

    constructor(private storage: StorageService, private data: DataService) { }

    save() {
        if (this.user) {
            this.data.sendRequest<IdentityUser>('post', 'api/account/editProfile/' + this.user.id, this.user).subscribe(u => this.user = u);
            //this.repo.createOrEditEntity('user', User.EnsureType(this.user), 1, 10);
        }

        this.edit = false;
    }

    discard() {
        if (this.user) {
            this.user.name = this.backup.name;
            this.user.email = this.backup.email;
            this.user.birth = this.backup.birth;
        }

        this.edit = false;
    }

    editMode() {
        if (this.user == null) {
            console.log('no data');
        } else {
            this.backup = {
                name: this.user.name,
                email: this.user.email,
                birth: this.user.birth
            };
            this.edit = true;
        }
    }

    ngOnInit(): void {
        this.data.sendRequest<IdentityUser>('get', 'api/account/getprofile/' + this.name).subscribe(
            res => this.user = res
        );
    }
}
interface UserBackup {
    name: string;
    email: string;
    birth: string;
}
