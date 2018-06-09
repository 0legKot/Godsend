import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/internal/operators';

type allowedMethod = 'get' | 'post' | 'put' | 'patch' | 'delete';

@Injectable({
    providedIn: 'root'
})
export class DataService {
    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

    public sendRequest<T>(method: allowedMethod, url: string, data?: any)
        : Observable<T> {
        return this.http.request<T>(method, this.baseUrl + url, { body: data, responseType: 'json' }).pipe(
            map(response => {
                console.log(this.baseUrl + url);
                console.log(response);
                return response;
            }));
    }
}
