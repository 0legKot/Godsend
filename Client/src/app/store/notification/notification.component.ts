import { Component, OnInit } from '@angular/core';
import { NotificationService } from '../../services/notification.service';

@Component({
  selector: 'godsend-notification',
  templateUrl: './notification.component.html',
})
export class NotificationComponent implements OnInit {
    message = '';
    showMenuMobile = false;
    get messages() {
        return this.notificationService.messages;
    }
    slideToggle(): void {
        this.showMenuMobile = !this.showMenuMobile;
    }

    hideMenu(): void {
        this.showMenuMobile = false;
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
