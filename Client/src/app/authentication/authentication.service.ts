import { Injectable } from '@angular/core';
import { Repository } from '../models/repository';
import { Observable, Subject, asapScheduler, pipe, of, from, interval, merge, fromEvent } from 'rxjs';
import { Router } from '@angular/router';
// import 'rxjs/add/observable/of';
import { map, filter, scan } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { DataService } from '../models/data.service';
// import {catch } from 'rxjs';
// import 'rxjs/add/operator/toPromise';

@Injectable()
export class AuthenticationService {

  constructor(private repo: Repository,
    private router: Router, private data: DataService) { }

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
