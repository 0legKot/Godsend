import { Injectable } from "@angular/core";
import { Repository } from "../models/repository";
import { Observable, Subject, asapScheduler, pipe, of, from, interval, merge, fromEvent } from 'rxjs';
import { Router } from "@angular/router";
//import "rxjs/add/observable/of";
import { map, filter, scan } from 'rxjs/operators';
import { Http } from "@angular/http/src/http";
//import 'rxjs/add/operator/toPromise';

@Injectable()
export class AuthenticationService {

    constructor(private repo: Repository,
        private router: Router, private http: Http) { }

    authenticated: boolean = false;
    name: string="";
    password: string="";
    callbackUrl: string="";

    login(): Observable<boolean> {
        this.authenticated = false;
        return this.http.post("/api/account/login", { name: this.name, password: this.password }).pipe(
            map(response => {
                if (response.ok) {
                    this.authenticated = true;
                    this.password = "";
                    this.router.navigateByUrl(this.callbackUrl || "/admin/overview");
                }
                return this.authenticated;
            }));
            //.catch(e => {
            //    this.authenticated = false;
            //    return Observable.of(false);
            //});

    }

    logout() {
        this.authenticated = false;
        this.http.post("/api/account/logout", null).subscribe(respone => { });
        this.router.navigateByUrl("/login");
    }
}