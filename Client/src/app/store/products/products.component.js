var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component, ViewChild } from '@angular/core';
import { RepositoryService } from '../../services/repository.service';
import { Product, ProductInfo } from '../../models/product.model';
import { searchType } from '../search/search.service';
import { SearchInlineComponent } from '../search/search-inline.component';
var ProductsComponent = /** @class */ (function () {
    function ProductsComponent(repo) {
        this.repo = repo;
        // private selectedId: string;
        this.type = searchType.product;
        this.templateText = 'Waiting for data...';
        this.searchInline = !;
    }
    Object.defineProperty(ProductsComponent.prototype, "products", {
        get: function () {
            return this.searchProducts || this.repo.products;
        },
        enumerable: true,
        configurable: true
    });
    ProductsComponent.prototype.createProduct = function (descr, name) {
        var _this = this;
        // TODO create interface with oly relevant info
        var prod = new Product('', new ProductInfo('', descr, name, 0, 0));
        this.repo.createProduct(prod, function () { return _this.searchInline.doSearch(); });
    };
    ProductsComponent.prototype.onFound = function (products) {
        this.templateText = 'Not found';
        this.searchProducts = products;
    };
    ProductsComponent.prototype.ngOnInit = function () {
        this.repo.getEntities('product' /*, res => this.products = res*/);
    };
    __decorate([
        ViewChild(SearchInlineComponent),
        __metadata("design:type", Object)
    ], ProductsComponent.prototype, "searchInline", void 0);
    ProductsComponent = __decorate([
        Component({
            selector: 'godsend-products',
            templateUrl: './products.component.html',
            styleUrls: ['./products.component.css']
        }),
        __metadata("design:paramtypes", [RepositoryService])
    ], ProductsComponent);
    return ProductsComponent;
}());
export { ProductsComponent };
//# sourceMappingURL=products.component.js.map