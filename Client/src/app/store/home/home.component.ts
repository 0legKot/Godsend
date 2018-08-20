import { Component, OnInit } from '@angular/core';
import { HubConnection } from '@aspnet/signalr';
import * as signalR from '@aspnet/signalr';

@Component({
    selector: 'godsend-home',
    templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit{
    private _hubConnection: HubConnection | undefined;
    public async: any;
    message = '';
    messages: string[] = [];

    constructor() {
    }

    public sendMessage(): void {
        const data = `Sent: ${this.message}`;

        if (this._hubConnection) {
            this._hubConnection.invoke('Send', this.message);
        }
        this.messages.push(data);
    }

    ngOnInit() {
        this._hubConnection = new signalR.HubConnectionBuilder()
            .withUrl('http://localhost:56440/chat')
            .configureLogging(signalR.LogLevel.Information)
            .build();

        this._hubConnection.start().catch(err => console.error(err.toString()));

        this._hubConnection.on('Send', (data: any) => {
            const received = `Received: ${data}`;
            this.messages.push(received);
        });
    }
}
