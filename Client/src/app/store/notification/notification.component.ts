import { Component, OnInit } from '@angular/core';
import { NotificationService } from '../../services/notification.service';

@Component({
  selector: 'godsend-notification',
  templateUrl: './notification.component.html',
})
export class NotificationComponent implements OnInit {
    message = '';
    get messages() {
        return this.notificationService.messages;
    }

    constructor(private notificationService: NotificationService) {
    }

    public sendMessage(): void {
        this.notificationService.sendMessage(this.message);
        this.message = '';
    }

    ngOnInit() {
       
    }

}
