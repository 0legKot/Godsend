import { Injectable, Inject } from '@angular/core';
import { HubConnection } from '@aspnet/signalr';
import * as signalR from '@aspnet/signalr';
import * as signalRMsgPack from '@aspnet/signalr-protocol-msgpack';
import { StorageService } from './storage.service';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
    private hubConnection: HubConnection | undefined;
    private messageArray: string[] = [];

    get messages() {
        return this.messageArray;
    }

    constructor(@Inject('BASE_URL') private baseUrl: string, private storage: StorageService) {
        this.reconnect();
    }

    public reconnect(): void {
        this.hubConnection = new signalR.HubConnectionBuilder()
            .withUrl(`${this.baseUrl}chat`, { accessTokenFactory: () => this.storage.JWTToken || '' })
            .withHubProtocol(new signalRMsgPack.MessagePackHubProtocol())
            .configureLogging(signalR.LogLevel.Information)
            .build();

        this.hubConnection.start().catch(err => console.error(err.toString()));

        this.configureConnection();

        this.messageArray = [];
    }

    public sendMessage(message: string): void {
        if (this.hubConnection) {
            this.hubConnection.invoke('Send', message);
        }
    }

    private configureConnection() {
        if (this.hubConnection) {
            this.hubConnection.on('Receive', (data: any) => {
                this.messages.unshift(`Received: ${data}`);
            });

            this.hubConnection.on('Send', (data: any) => {
                this.messages.unshift(`Sent: ${data}`);
            });

            this.hubConnection.on('Success', (data: any) => {
                this.messages.unshift(`Success: ${data}`);
            });

            this.hubConnection.on('Error', (data: any) => {
                this.messages.unshift(`Error: ${data}`);
            });

            this.hubConnection.on('Info', (data: any) => {
                this.messages.unshift(`Info: ${data}`);
            });
        }
    }
}
