import { Component } from '@angular/core';
import { IdentityUser } from '../../models/user.model';
import { DataService } from '../../services/data.service';
import { OnInit } from '@angular/core/src/metadata/lifecycle_hooks';
import { StorageService } from '../../services/storage.service';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'godsend-user',
    templateUrl: './user.component.html',
    styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
    user: IdentityUser = new IdentityUser();
    curId: string = this.route.snapshot.params.id;
    name = this.storage.name;
    edit = false;
    backup: UserBackup = {
        name: '',
        email: '',
        birth: ''
    };

    get isCurrent() {
        return this.storage.id === this.curId;
    }

    constructor(private storage: StorageService, private data: DataService, private route: ActivatedRoute) { }

    save() {
        if (this.user) {
            this.user.token = this.storage.JWTToken ? this.storage.JWTToken : '';
            this.data.sendRequest<IdentityUser>('post',
                'api/account/editProfile/' , this.user).subscribe(u => this.user = u);
            // this.repo.createOrEditEntity('user', User.EnsureType(this.user), 1, 10);
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
        this.data.sendRequest<IdentityUser>('get', 'api/account/getprofile/' + this.curId).subscribe(
            res => {
                this.user = res;
                // if (this.user.birth == null) this.user.birth = "private info";
            }
        );
    }
}
interface UserBackup {
    name: string;
    email: string;
    birth: string;
}
