import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class StorageService {
    private readonly tokenPath = 'godsend_authtoken';
    private readonly namePath = 'godsend_authname';
    private readonly idPath = 'godsend_authid';

    constructor() { }

    get authenticated(): boolean {
        return !!localStorage.getItem(this.tokenPath);
    }

    get name(): string | null {
        return localStorage.getItem(this.namePath);
    }

    get id(): string | null {
        return localStorage.getItem(this.idPath);
    }

    set id(newId) {
        if (newId == null) {
            localStorage.removeItem(this.idPath);
        } else {
            localStorage.setItem(this.idPath, newId);
        }
    }

    set name(newName: string | null) {
        if (newName == null) {
            localStorage.removeItem(this.namePath);
        } else {
            localStorage.setItem(this.namePath, newName);
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
