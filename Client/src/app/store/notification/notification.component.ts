import { Component, OnInit } from '@angular/core';
import { NotificationService } from '../../services/notification.service';

@Component({
  selector: 'godsend-notification',
  templateUrl: './notification.component.html',
})
export class NotificationComponent implements OnInit {
    message = '';
    showMenuMobilee = false;
    get messages() {
        return this.notificationService.messages;
    }
    slideTogglee(): void {
        this.showMenuMobilee = !this.showMenuMobilee;
    }

    hideMenuu(): void {
        this.showMenuMobilee = false;
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
