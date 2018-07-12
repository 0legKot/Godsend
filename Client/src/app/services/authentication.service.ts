import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { DataService } from '../services/data.service';
import { IdentityUser } from '../models/user.model';

@Injectable({
    providedIn: 'root'
})
export class AuthenticationService {
    constructor(private router: Router, private data: DataService) { }

    get authenticated(): boolean {
        return !!localStorage.getItem('godsend_authtoken');
    }

    get name(): string {
        return localStorage.getItem('godsend_authname') || '';
    }

    callbackUrl = '';

    // todo remove email/name inconsistency
    login(email: string, password: string): void {
        // this.authenticated = false;
        this.data.sendRequest<any>('post', 'api/account/login', { email, password }).subscribe(response => {
            // todo remove copypaste
            localStorage.setItem('godsend_authtoken', response.token);
            localStorage.setItem('godsend_authname', email);

            this.router.navigateByUrl(this.callbackUrl);
        }, error => {

            console.log('login fail');
        });
        // .catch(e => {
        //     this.authenticated = false;
        //     this.name = '';
        // });
    }

    register(user: IdentityUser) {
        this.data.sendRequest<any>('post', 'api/account/register', { user }).subscribe(response => {
            // todo remove copypaste
            localStorage.setItem('godsend_authtoken', response.token);
            localStorage.setItem('godsend_authname', user.email);

            this.router.navigateByUrl(this.callbackUrl);
        }, error => {

            console.log('register fail');
        });
    }

    logout() {
        localStorage.removeItem('godsend_authtoken');
        localStorage.removeItem('godsend_authname');

        // this.data.sendRequest('post', '/api/account/logout').subscribe(response => { });
        this.router.navigateByUrl('/login');
    }
}
