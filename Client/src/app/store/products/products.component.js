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
import { Product, ProductInfo, FilterInfoView, DecimalPropertyInfo, StringPropertyInfo, IntPropertyInfo, orderBy } from '../../models/product.model';
import { searchType } from '../search/search.service';
import { SearchInlineComponent } from '../search/search-inline.component';
import { ImageService } from '../../services/image.service';
import { CategoryService } from '../../services/category.service';
var ProductsComponent = /** @class */ (function () {
    //onFound(products: ProductInfo[]) {
    //    this.templateText = 'Not found';
    //    this.searchProducts = products;
    //}
    function ProductsComponent(repo, imageService, cattt) {
        this.repo = repo;
        this.imageService = imageService;
        this.cattt = cattt;
        // private selectedId: string;
        // page: number = 1;
        // rpp: number = 10;
        this.type = searchType.product;
        this.images = {};
        //searchProducts?: ProductInfo[];
        this.templateText = 'Waiting for data...';
        this.imagg = {};
        this.filter = new FilterInfoView();
        this.orderBy = orderBy;
    }
    Object.defineProperty(ProductsComponent.prototype, "pagesCount", {
        get: function () {
            return Math.ceil(this.repo.productsCount / this.repo.productFilter.quantity);
        },
        enumerable: true,
        configurable: true
    });
    ProductsComponent.prototype.onPageChanged = function (page) {
        this.repo.productFilter.page = page;
        this.getProducts();
    };
    ProductsComponent.prototype.getProducts = function () {
        var _this = this;
        this.repo.getByFilter(function (res) {
            _this.imageService.getPreviewImages(res.map(function (pi) { return pi.id; }), function (smth) { return _this.imagg = smth; });
        });
    };
    Object.defineProperty(ProductsComponent.prototype, "products", {
        get: function () {
            //return this.searchProducts || this.repo.products;
            return this.repo.products;
        },
        enumerable: true,
        configurable: true
    });
    ProductsComponent.prototype.getImage = function (pi) {
        return this.images[pi.id];
    };
    ProductsComponent.prototype.createProduct = function (descr, name) {
        // TODO create interface with only relevant info
        var prod = new Product('', new ProductInfo('', descr, name, 0, 0));
        this.repo.createOrEditEntity('product', prod, 0, 0);
    };
    ProductsComponent.prototype.deleteProduct = function (id) {
        this.repo.deleteEntity('product', id, 0, 0);
    };
    ProductsComponent.prototype.ngOnInit = function () {
        this.getProducts();
    };
    ProductsComponent.prototype.getCategories = function () {
        this.categories = this.cattt.cats ? this.cattt.cats.map(function (cws) { return cws.cat; }) : [];
        console.log(this.categories);
    };
    ProductsComponent.prototype.getSubcategories = function (category) {
        this.categories = this.cattt.getSubcategories(category);
        this.getCategoryProps(category);
    };
    ProductsComponent.prototype.getByCategory = function (category) {
        this.repo.productFilter.categoryId = category.id;
        this.getProducts();
    };
    ProductsComponent.prototype.getByFilter = function () {
        if (this.filter) {
            if (this.filter.stringProps) {
                this.repo.productFilter.stringProps = this.filter.stringProps
                    .filter(function (prop) { return prop.part !== '' && prop.part != null; })
                    .map(function (prop) { return new StringPropertyInfo(prop.propId, prop.part); });
            }
            if (this.filter.intProps) {
                this.repo.productFilter.intProps = this.filter.intProps
                    .filter(function (prop) { return prop.left != null && prop.right != null; })
                    .map(function (prop) { return new IntPropertyInfo(prop.propId, prop.left, prop.right); });
            }
            if (this.filter.decimalProps) {
                this.repo.productFilter.decimalProps = this.filter.decimalProps
                    .filter(function (prop) { return prop.left != null && prop.right != null; })
                    .map(function (prop) { return new DecimalPropertyInfo(prop.propId, prop.left, prop.right); });
            }
            this.repo.productFilter.orderBy = this.filter.orderBy;
            this.repo.productFilter.sortAscending = this.filter.sortAscending;
            this.getProducts();
        }
    };
    ProductsComponent.prototype.getCategoryProps = function (category) {
        var _this = this;
        this.cattt.getCategoryProps(category, function (filter) { _this.filter = filter; console.log(filter); });
    };
    ProductsComponent.prototype.setCurrentCategory = function (category) {
        this.getCategoryProps(category);
    };
    __decorate([
        ViewChild(SearchInlineComponent),
        __metadata("design:type", SearchInlineComponent)
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