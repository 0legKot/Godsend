var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Cart, isDiscrete } from "../models/cart.model";
import { RepositoryService } from "./repository.service";
import { Injectable } from "@angular/core";
var CartService = /** @class */ (function () {
    function CartService(repo) {
        this.repo = repo;
        this.cart = new Cart(new Array(), new Array());
    }
    CartService.prototype.checkout = function () {
        //const ord = new Order('', this.cart.customer, this.cart.discreteItems, this.cart.weightedItems, '', 0);
        this.repo.createOrder(this.cart);
    };
    CartService.prototype.addToCart = function (part) {
        if (isDiscrete(part))
            this.cart.discreteItems.push(part);
        else
            this.cart.weightedItems.push(part);
    };
    CartService.prototype.removeFromCart = function (part) {
        if (isDiscrete(part))
            this.cart.discreteItems = this.cart.discreteItems.filter(function (p) { return p !== part; });
        else
            this.cart.weightedItems = this.cart.weightedItems.filter(function (p) { return p !== part; });
    };
    CartService = __decorate([
        Injectable(),
        __metadata("design:paramtypes", [RepositoryService])
    ], CartService);
    return CartService;
}());
export { CartService };
//# sourceMappingURL=cart.service.js.map