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
import { Product, ProductInfo, FilterInfo, DecimalPropertyInfo, StringPropertyInfo, IntPropertyInfo } from '../../models/product.model';
import { searchType } from '../search/search.service';
import { SearchInlineComponent } from '../search/search-inline.component';
import { ImageService } from '../../services/image.service';
import { CategoryService } from '../../services/category.service';
var ProductsComponent = /** @class */ (function () {
    function ProductsComponent(repo, imageService, cattt) {
        this.repo = repo;
        this.imageService = imageService;
        this.cattt = cattt;
        // private selectedId: string;
        this.page = 1;
        this.rpp = 10;
        this.type = searchType.product;
        this.images = {};
        this.templateText = 'Waiting for data...';
        this.searchInline = !;
        this.imagg = {};
    }
    ProductsComponent.prototype.prevPage = function () {
        var _this = this;
        this.page--;
        this.repo.getEntities('product', this.page, this.rpp, function (res) {
            _this.imageService.getPreviewImages(res.map(function (pi) { return pi.id; }), function (smth) { return _this.imagg = smth; });
        });
    };
    ProductsComponent.prototype.nextPage = function () {
        var _this = this;
        this.page++;
        this.repo.getEntities('product', this.page, this.rpp, function (res) {
            _this.imageService.getPreviewImages(res.map(function (pi) { return pi.id; }), function (smth) { return _this.imagg = smth; });
        });
    };
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
        this.repo.createOrEditEntity('product', prod, this.page, this.rpp, function () { return _this.searchInline.doSearch(); });
    };
    ProductsComponent.prototype.deleteProduct = function (id) {
        var _this = this;
        this.repo.deleteEntity('product', id, this.page, this.rpp, function () { return _this.searchInline.doSearch(); });
    };
    ProductsComponent.prototype.onFound = function (products) {
        this.templateText = 'Not found';
        this.searchProducts = products;
    };
    ProductsComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.repo.getEntities('product', this.page, this.rpp, function (res) {
            _this.imageService.getPreviewImages(res.map(function (pi) { return pi.id; }), function (smth) { return _this.imagg = smth; });
        });
    };
    ProductsComponent.prototype.getCategories = function () {
        this.categories = this.cattt.cats ? this.cattt.cats.map(function (cws) { return cws.cat; }) : [];
    };
    ProductsComponent.prototype.getSubcategories = function (category) {
        this.categories = this.cattt.getSubcategories(category);
        this.getCategoryProps(category);
    };
    ProductsComponent.prototype.getByCategory = function (category) {
        this.repo.getByCategory(category);
    };
    ProductsComponent.prototype.getByFilter = function () {
        if (this.filter) {
            var trimmedFilter = new FilterInfo();
            if (this.filter.stringProps) {
                trimmedFilter.stringProps = this.filter.stringProps
                    .filter(function (prop) { return prop.part !== '' && prop.part != null; })
                    .map(function (prop) { return new StringPropertyInfo(prop.propId, prop.part); });
            }
            if (this.filter.intProps) {
                trimmedFilter.intProps = this.filter.intProps
                    .filter(function (prop) { return prop.left != null && prop.right != null; })
                    .map(function (prop) { return new IntPropertyInfo(prop.propId, prop.left, prop.right); });
            }
            if (this.filter.decimalProps) {
                trimmedFilter.decimalProps = this.filter.decimalProps
                    .filter(function (prop) { return prop.left != null && prop.right != null; })
                    .map(function (prop) { return new DecimalPropertyInfo(prop.propId, prop.left, prop.right); });
            }
            this.repo.getByFilter(trimmedFilter);
        }
    };
    ProductsComponent.prototype.getCategoryProps = function (category) {
        var _this = this;
        this.cattt.getCategoryProps(category, function (filter) { return _this.filter = filter; });
    };
    ProductsComponent.prototype.setCurrentCategory = function (category) {
        this.getCategoryProps(category);
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
        __metadata("design:paramtypes", [RepositoryService, ImageService, CategoryService])
    ], ProductsComponent);
    return ProductsComponent;
}());
export { ProductsComponent };
//# sourceMappingURL=products.component.js.map