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
import { CartService } from '../../services/cart.service';
var CartComponent = /** @class */ (function () {
    // parts: OrderPartSend[];
    function CartComponent(cartService) {
        this.cartService = cartService;
    }
    Object.defineProperty(CartComponent.prototype, "totalPrice", {
        get: function () {
            return (this.discreteParts.reduce(function (prev, cur) { return prev + cur.price * cur.quantity; }, 0))
                .toFixed(2);
        },
        enumerable: true,
        configurable: true
    });
    CartComponent.prototype.checkout = function () {
        this.cartService.checkout();
    };
    Object.defineProperty(CartComponent.prototype, "discreteParts", {
        get: function () {
            console.log('getting discrete');
            console.dir(this.cartService.cart.discreteItems);
            return this.cartService.cart.discreteItems;
        },
        enumerable: true,
        configurable: true
    });
    // get weightedParts(): OrderPartWeightedView[] {
    //    return this.cartService.cart.weightedItems;
    // }
    CartComponent.prototype.delete = function (part) {
        this.cartService.removeFromCart(part);
    };
    CartComponent.prototype.increment = function (part) {
        ++part.quantity;
    };
    CartComponent.prototype.decrement = function (part) {
        if (part.quantity <= 0) {
            this.delete(part);
        }
        else {
            --part.quantity;
        }
    };
    CartComponent = __decorate([
        Component({
            selector: 'godsend-cart',
            templateUrl: './cart.component.html'
        }),
        __metadata("design:paramtypes", [CartService])
    ], CartComponent);
    return CartComponent;
}());
export { CartComponent };
//# sourceMappingURL=cart.component.js.map