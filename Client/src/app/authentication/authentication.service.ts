import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { DataService } from '../services/data.service';

@Injectable({
    providedIn: 'root'
})
export class AuthenticationService {
    constructor(private router: Router, private data: DataService) { }

    authenticated = false;
    name = '';
    password = '';
    callbackUrl = '';

    login(): void {
        this.authenticated = false;
        this.data.sendRequest<any>('post', 'api/account/login', { name: this.name, password: this.password }).subscribe(response => {
            console.log(response);
            if (response) {
                this.authenticated = true;
                this.name += ' ';
                this.password = '';
                this.router.navigateByUrl(this.callbackUrl);
            } else {
                this.authenticated = false;
                this.name = '';
                console.log('login fail');
            }
        });
        // .catch(e => {
        //     this.authenticated = false;
        //     this.name = '';
        // });
    }

    logout() {
        this.authenticated = false;
        this.name = '';
        this.data.sendRequest('post', '/api/account/logout').subscribe(response => { });
        this.router.navigateByUrl('/login');
    }
}
