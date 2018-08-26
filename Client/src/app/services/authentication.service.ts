import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { DataService } from '../services/data.service';
import { IdentityUser } from '../models/user.model';
import { StorageService } from './storage.service';
import { NotificationService } from './notification.service';

@Injectable({
    providedIn: 'root'
})
export class AuthenticationService {
    constructor(
        private router: Router,
        private data: DataService,
        private storage: StorageService,
        private notificationService: NotificationService
    ) { }

    callbackUrl = '';

    login(name: string, password: string): void {
        // this.authenticated = false;
        this.data.sendRequest<any>('post', 'api/account/login', { name, password }).subscribe(response => {
            this.storage.JWTToken = response.token;
            this.storage.name = name;

            this.notificationService.reconnect();

            this.router.navigateByUrl(this.callbackUrl);
        }, error => {

            console.log('login fail');
        });
        // .catch(e => {
        //     this.authenticated = false;
        //     this.name = '';
        // });
    }

    register(user: IdentityUser, pass: string) {
        this.data.sendRequest<any>(
            'post',
            'api/account/register',
            { email: user.email, password: pass, name: user.name, birth: user.birth }
        ).subscribe(response => {
            this.storage.JWTToken = response.token;
            this.storage.name = name;

            this.notificationService.reconnect();

            this.router.navigateByUrl(this.callbackUrl);
        }, error => {

            console.log('register fail');
        });
    }

    logout() {
        this.storage.JWTToken = null;
        this.storage.name = null;

        this.notificationService.reconnect();

        // this.data.sendRequest('post', '/api/account/logout').subscribe(response => { });
        this.router.navigateByUrl('/login');
    }
}
