import { Component, OnInit } from '@angular/core';
import { HubConnection } from '@aspnet/signalr';
import * as signalR from '@aspnet/signalr';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
  selector: 'godsend-notification',
  templateUrl: './notification.component.html',
})
export class NotificationComponent implements OnInit {

    private _hubConnection: HubConnection | undefined;
    public async: any;
    message = '';
    messages: string[] = [];

    constructor(private authService: AuthenticationService) {
    }

    public sendMessage(): void {
        const data = `Sent: ${this.message}`;

        if (this._hubConnection) {
            this._hubConnection.invoke('Send', this.message);
        }
        this.messages.unshift(data);
    }

    ngOnInit() {
        this._hubConnection = new signalR.HubConnectionBuilder()
            .withUrl('http://localhost:56440/chat', { accessTokenFactory: () => this.authService.getJWTToken() })
            .configureLogging(signalR.LogLevel.Information)
            .build();

        this._hubConnection.start().catch(err => console.error(err.toString()));

        this._hubConnection.on('Send', (data: any) => {
            this.messages.unshift(`Received: ${data}`);
        });

        this._hubConnection.on('Success', (data: any) => {
            this.messages.unshift(`Success: ${data}`);
        })
    }

}
