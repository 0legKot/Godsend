import { Injectable } from '@angular/core';
import { Repository } from '../models/repository';
import { Observable, Subject, asapScheduler, pipe, of, from, interval, merge, fromEvent } from 'rxjs';
import { Router } from '@angular/router';
// import 'rxjs/add/observable/of';
import { map, filter, scan } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { DataService } from '../models/data.service';
// import 'rxjs/add/operator/toPromise';

@Injectable()
export class AuthenticationService {

  constructor(private repo: Repository,
    private router: Router, private data: DataService) { }

    authenticated = false;
    name = '';
    password = '';
    callbackUrl = '';

    login(): Observable<boolean> {
        this.authenticated = true;
        return this.data.sendRequest<boolean>('post','api/account/login', { name: this.name, password: this.password }).pipe(
            map(response => {
                if (response) {
                    this.authenticated = true;
                  this.password = '';
                  this.router.navigateByUrl(this.callbackUrl );
                }
                return this.authenticated;
            }));
            // .catch(e => {
            //     this.authenticated = false;
            //     return Observable.of(false);
            // });

    }

    logout() {
        this.authenticated = false;
        this.http.post('/api/account/logout', null).subscribe(respone => { });
        this.router.navigateByUrl('/login');
    }
}
