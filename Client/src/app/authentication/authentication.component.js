var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component } from '@angular/core';
import { AuthenticationService } from './authentication.service';
var AuthenticationComponent = /** @class */ (function () {
    function AuthenticationComponent(authService) {
        this.authService = authService;
        this.showError = false;
    }
    AuthenticationComponent.prototype.login = function () {
        this.showError = false;
        this.authService.login();
        // .subscribe(result => {
        //     this.showError = !result;
        // });
    };
    AuthenticationComponent = __decorate([
        Component({
            templateUrl: 'authentication.component.html',
            selector: 'godsend-auth'
        }),
        __metadata("design:paramtypes", [AuthenticationService])
    ], AuthenticationComponent);
    return AuthenticationComponent;
}());
export { AuthenticationComponent };
//# sourceMappingURL=authentication.component.js.map