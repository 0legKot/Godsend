import { Component } from '@angular/core';
import { OnInit } from '@angular/core';
import { IdentityUser } from '../../models/user.model';
import { DataService } from '../../services/data.service';
import { NotificationService } from '../../services/notification.service';

@Component({
    selector: 'godsend-admin',
    templateUrl: './admin.component.html',
    styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {
    userList: IdentityUser[] = [];
    message = '';
    roles = ['Administrator', 'Moderator', 'Supplier'];

    public sendMessage(name: string): void {
        console.log(name);
        this.notificationService.sendMessageTo(name, this.message);
        this.message = '';
    }
    addToRole(username: string, role: string) {
        this.data.sendRequest<any>('post', 'api/account/addtorole', { userName: username, role: role }).subscribe();
    }

    ngOnInit(): void {
        this.data.sendRequest<IdentityUser[]>('get', 'api/account/getuserlist/1/10')
            .subscribe(res => this.userList = res);
    }
    constructor(private notificationService: NotificationService, private data: DataService) { }

}
