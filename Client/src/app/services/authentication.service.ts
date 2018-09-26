import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { DataService } from '../services/data.service';
import { IdentityUser } from '../models/user.model';
import { StorageService } from './storage.service';
import { NotificationService } from './notification.service';
import { map } from 'rxjs/operators';

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

    setCreds(jwt: string | null, username: string | null, id: string | null) {
        this.storage.JWTToken = jwt;
        this.storage.name = username;
        this.storage.id = id;
    }

    login(name: string, password: string): void {
        // this.authenticated = false;
        this.data.sendRequest<any>('post', 'api/account/login', { name, password }).subscribe(response => {
            this.setCreds(response.token,name,response.id);

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
            this.setCreds(response.token, name, response.id);

            this.notificationService.reconnect();

            this.router.navigateByUrl(this.callbackUrl);
        }, error => {

            console.log('register fail');
        });
    }

    logout() {
        this.setCreds(null, null, null);

        this.notificationService.reconnect();

        // this.data.sendRequest('post', '/api/account/logout').subscribe(response => { });
        this.router.navigateByUrl('/login');
    }

    facebookLogin(accessToken: string) {
        return this.data.sendRequest<any>('post', 'api/account/facebookLogin', { accessToken })
            .subscribe(res => {
                this.setCreds(res.token, res.name, res.id);

                this.notificationService.reconnect();

                this.router.navigateByUrl(this.callbackUrl);
            });
    }
}
