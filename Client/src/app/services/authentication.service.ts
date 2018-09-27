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
    ) {
        if (this.storage.JWTToken != null) {
            this.refreshRoles();
        }
    }

    callbackUrl = '';
    roles: Role[] = [];

    setCreds(jwt: string | null, username: string | null, id: string | null) {
        this.storage.JWTToken = jwt;
        this.storage.name = username;
        this.storage.id = id;
    }

    login(name: string, password: string): void {
            this.data.sendRequest<any>('post', 'api/account/login', { name, password }).subscribe(response => {
                this.setCreds(response.token, name, response.id);
                this.notificationService.reconnect();
                this.router.navigateByUrl(this.callbackUrl);
                this.refreshRoles();
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
            this.login(user.name, pass); // todo remove
            this.router.navigateByUrl(this.callbackUrl);
        }, error => {

            console.log('register fail');
        });
    }

    refreshRoles() {
        this.data.sendRequest<Role[]>('get', 'api/account/getroles')
            .subscribe(roles => roles.forEach(x => this.roles.push(x)));
    }

    logout() {
        this.setCreds(null, null, null);
        this.roles = [];
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

export type Role = 'Administrator' | 'Moderator' | 'Supplier';
