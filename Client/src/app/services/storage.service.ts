import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class StorageService {
    private readonly tokenPath = 'godsend_authtoken';
    private readonly namePath = 'godsend_authname';

    constructor() { }

    get authenticated(): boolean {
        return !!localStorage.getItem(this.tokenPath);
    }

    get name(): string | null {
        return localStorage.getItem(this.namePath);
    }

    set name(newName: string | null) {
        if (newName == null) {
            localStorage.removeItem(this.namePath);
        } else {
            localStorage.setItem(this.namePath, newName)
        }
    }

    get JWTToken(): string | null {
        return localStorage.getItem(this.tokenPath);
    }

    set JWTToken(newToken: string | null) {
        if (newToken == null) {
            localStorage.removeItem(this.tokenPath);
        } else {
            localStorage.setItem(this.tokenPath, newToken);
        }
    }
}
