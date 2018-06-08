var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
// import { switchMap } from 'rxjs/operators';
import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { RepositoryService } from '../../services/repository.service';
import { CartService } from '../../services/cart.service';
import { guidZero } from '../../models/cart.model';
var ProductDetailComponent = /** @class */ (function () {
    function ProductDetailComponent(route, router, service, cart) {
        this.route = route;
        this.router = router;
        this.service = service;
        this.cart = cart;
        this.quantity = 1;
    }
    Object.defineProperty(ProductDetailComponent.prototype, "price", {
        get: function () {
            return this.selectedSupplier ? (this.selectedSupplier.price * this.quantity).toFixed(2) : '';
        },
        enumerable: true,
        configurable: true
    });
    ProductDetailComponent.prototype.gotoProducts = function (product) {
        var productId = product ? product.id : null;
        this.router.navigate(['/products', { id: productId }]);
    };
    ProductDetailComponent.prototype.buy = function () {
        var op = {
            quantity: this.quantity,
            productId: this.data && this.data.product ? this.data.product.id : guidZero,
            supplierId: this.selectedSupplier && this.selectedSupplier.supplier ? this.selectedSupplier.supplier.id : guidZero
        };
        this.cart.addToCart(op);
    };
    ProductDetailComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.service.getEntity(this.route.snapshot.params.id, function (p) {
            _this.data = p;
            _this.selectedSupplier = p.suppliers[0];
        }, 'product');
    };
    ProductDetailComponent = __decorate([
        Component({
            selector: 'godsend-product-detail',
            templateUrl: 'product-detail.component.html',
            styleUrls: ['./product-detail.component.css']
        }),
        __metadata("design:paramtypes", [ActivatedRoute,
            Router,
            RepositoryService,
            CartService])
    ], ProductDetailComponent);
    return ProductDetailComponent;
}());
export { ProductDetailComponent };
//# sourceMappingURL=product-detail.component.js.map