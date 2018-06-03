import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/internal/operators';

@Injectable()
export class DataService {
    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

    public sendRequest<T>(method: string, url: string, data?: any)
        : Observable<T> {
        return this.http.request<T>(method, this.baseUrl + url, { body: data, responseType: 'json' }).pipe(
            map(response => {
                console.log(this.baseUrl + url);
                console.log(response);
                return response;
            }));
    }
}
