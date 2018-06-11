var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { DataService } from '../services/data.service';
var AuthenticationService = /** @class */ (function () {
    function AuthenticationService(router, data) {
        this.router = router;
        this.data = data;
        this.authenticated = false;
        this.name = '';
        this.password = '';
        this.callbackUrl = '';
    }
    AuthenticationService.prototype.login = function () {
        var _this = this;
        this.authenticated = false;
        this.data.sendRequest('post', 'api/account/login', { name: this.name, password: this.password }).subscribe(function (response) {
            console.log(response);
            if (response) {
                _this.authenticated = true;
                _this.name += ' ';
                _this.password = '';
                _this.router.navigateByUrl(_this.callbackUrl);
            }
            else {
                _this.authenticated = false;
                _this.name = '';
                console.log('login fail');
            }
        });
        // .catch(e => {
        //     this.authenticated = false;
        //     this.name = '';
        // });
    };
    AuthenticationService.prototype.logout = function () {
        this.authenticated = false;
        this.name = '';
        this.data.sendRequest('post', '/api/account/logout').subscribe(function (response) { });
        this.router.navigateByUrl('/login');
    };
    AuthenticationService = __decorate([
        Injectable({
            providedIn: 'root'
        }),
        __metadata("design:paramtypes", [Router, DataService])
    ], AuthenticationService);
    return AuthenticationService;
}());
export { AuthenticationService };
//# sourceMappingURL=authentication.service.js.map