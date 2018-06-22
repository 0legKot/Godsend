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
import { Product } from '../../models/product.model';
import { RepositoryService } from '../../services/repository.service';
import { CartService } from '../../services/cart.service';
import { ImageService } from '../../services/image.service';
var ProductDetailComponent = /** @class */ (function () {
    function ProductDetailComponent(route, router, service, cart, imageService) {
        this.route = route;
        this.router = router;
        this.service = service;
        this.cart = cart;
        this.imageService = imageService;
        this.quantity = 1;
        this.edit = false;
        this.images = [];
        this.backup = {
            name: '',
            description: ''
        };
    }
    Object.defineProperty(ProductDetailComponent.prototype, "price", {
        get: function () {
            return this.selectedSupplier ? (this.selectedSupplier.price * this.quantity).toFixed(2) : '';
        },
        enumerable: true,
        configurable: true
    });
    ProductDetailComponent.prototype.gotoProducts = function (product) {
        var productId = this.route.snapshot.params.id;
        this.router.navigate(['/products', { id: productId }]);
    };
    ProductDetailComponent.prototype.deleteProduct = function () {
        if (this.data) {
            this.service.deleteEntity('product', this.data.product.info.id, 1, 10);
            this.gotoProducts();
        }
    };
    ProductDetailComponent.prototype.buy = function () {
        // Todo make button disabled if no data?
        if (this.data == null || this.selectedSupplier == null) {
            console.log('ERROR: no data');
            return;
        }
        var op = {
            quantity: this.quantity,
            product: this.data.product,
            supplier: this.selectedSupplier.supplier,
            price: this.selectedSupplier.price
        };
        this.cart.addToCart(op);
    };
    ProductDetailComponent.prototype.editMode = function () {
        if (this.data == null) {
            console.log('no data');
            return;
        }
        this.backup = {
            name: this.data.product.info.name,
            description: this.data.product.info.description
        };
        this.edit = true;
    };
    ProductDetailComponent.prototype.save = function () {
        if (this.data) {
            this.service.createOrEditEntity('product', Product.EnsureType(this.data.product), 1, 10);
        }
        this.edit = false;
    };
    ProductDetailComponent.prototype.discard = function () {
        if (this.data) {
            this.data.product.info.name = this.backup.name;
            this.data.product.info.description = this.backup.description;
        }
        this.edit = false;
    };
    ProductDetailComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.service.getEntity(this.route.snapshot.params.id, function (p) {
            _this.data = p;
            _this.selectedSupplier = p.suppliers[0];
        }, 'product');
        this.imageService.getImages(this.route.snapshot.params.id, function (images) { _this.images = images; });
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
            CartService,
            ImageService])
    ], ProductDetailComponent);
    return ProductDetailComponent;
}());
export { ProductDetailComponent };
//# sourceMappingURL=product-detail.component.js.map