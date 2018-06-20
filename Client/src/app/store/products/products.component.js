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
import { ImageService } from '../../services/image.service';
var ProductsComponent = /** @class */ (function () {
    function ProductsComponent(repo, imageService) {
        this.repo = repo;
        this.imageService = imageService;
        // private selectedId: string;
        this.type = searchType.product;
        this.images = {};
        this.templateText = 'Waiting for data...';
        this.searchInline = !;
        this.imagg = {};
    }
    Object.defineProperty(ProductsComponent.prototype, "products", {
        get: function () {
            return this.searchProducts || this.repo.products;
        },
        enumerable: true,
        configurable: true
    });
    ProductsComponent.prototype.getImage = function (pi) {
        return this.images[pi.id];
    };
    ProductsComponent.prototype.createProduct = function (descr, name) {
        var _this = this;
        // TODO create interface with only relevant info
        var prod = new Product('', new ProductInfo('', descr, name, 0, 0));
        this.repo.createOrEditEntity('product', prod, function () { return _this.searchInline.doSearch(); });
    };
    ProductsComponent.prototype.deleteProduct = function (id) {
        var _this = this;
        this.repo.deleteEntity('product', id, function () { return _this.searchInline.doSearch(); });
    };
    ProductsComponent.prototype.onFound = function (products) {
        this.templateText = 'Not found';
        this.searchProducts = products;
    };
    ProductsComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.repo.getEntities('product', function (res) {
            _this.imageService.getPreviewImages(res.map(function (pi) { return pi.id; }), function (smth) { return _this.imagg = smth; });
            /*for (let p of res) {
                this.imageService.getImage(p.id, image => { this.images[p.id] = image; });
            }*/
        });
    };
    ProductsComponent.prototype.getCategories = function () {
        var _this = this;
        this.repo.getCategories(function (cats) { return _this.categories = cats; });
    };
    ProductsComponent.prototype.getSubcategories = function (category) {
        var _this = this;
        this.repo.getSubcategories(category, function (cats) { return _this.categories = cats; });
    };
    ProductsComponent.prototype.getByCategory = function (category) {
        this.repo.getByCategory(category);
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
        __metadata("design:paramtypes", [RepositoryService, ImageService])
    ], ProductsComponent);
    return ProductsComponent;
}());
export { ProductsComponent };
//# sourceMappingURL=products.component.js.map