import { Injectable, Inject } from '@angular/core';
import { HubConnection } from '@aspnet/signalr';
import * as signalR from '@aspnet/signalr';
import { AuthenticationService } from './authentication.service';
import { StorageService } from './storage.service';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
    private _hubConnection: HubConnection | undefined;
    private _messages: string[] = [];

    get messages() {
        return this._messages;
    }

    constructor(@Inject('BASE_URL') private baseUrl: string, private storage: StorageService) {
        this._hubConnection = new signalR.HubConnectionBuilder()
            .withUrl(`${baseUrl}chat`, { accessTokenFactory: () => this.storage.JWTToken || '' })
            .configureLogging(signalR.LogLevel.Information)
            .build();

        this._hubConnection.start().catch(err => console.error(err.toString()));

        this._hubConnection.on('Receive', (data: any) => {
            this.messages.unshift(`Received: ${data}`);
        });

        this._hubConnection.on('Send', (data: any) => {
            this.messages.unshift(`Sent: ${data}`);
        });

        this._hubConnection.on('Success', (data: any) => {
            this.messages.unshift(`Success: ${data}`);
        })
    }

    public reconnect(): void {
        if (this._hubConnection) {
            this._hubConnection.stop();
            this._hubConnection.start().catch(err => console.error(err.toString()));
        }
    }

    public sendMessage(message: string): void {
        if (this._hubConnection) {
            this._hubConnection.invoke('Send', message);
        }
    }
}
