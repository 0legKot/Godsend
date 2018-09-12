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
import { Router } from '@angular/router';
import { RepositoryService } from '../../services/repository.service';
import { Product, ProductInfo, FilterInfoView, DecimalPropertyInfo, StringPropertyInfo, IntPropertyInfo, orderBy } from '../../models/product.model';
import { searchType } from '../search/search.service';
import { SearchInlineComponent } from '../search/search-inline.component';
import { ImageService } from '../../services/image.service';
import { CategoryService } from '../../services/category.service';
import { guidZero } from '../../models/cart.model';
var ProductsComponent = /** @class */ (function () {
    // onFound(products: ProductInfo[]) {
    //    this.templateText = 'Not found';
    //    this.searchProducts = products;
    // }
    function ProductsComponent(repo, imageService, catService, router) {
        this.repo = repo;
        this.imageService = imageService;
        this.catService = catService;
        this.router = router;
        // private selectedId: string;
        // page: number = 1;
        // rpp: number = 10;
        this.type = searchType.product;
        // searchProducts?: ProductInfo[];
        this.templateText = 'Waiting for data...';
        this.comparsionSet = new Array();
        this.filter = new FilterInfoView();
        this.orderBy = orderBy;
        /**
         * images as a dictionary where key is id and value is base64-encoded image
         * */
        this.images = {};
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
        this.repo.getByFilter(function (res) {
            console.log(res);
        });
    };
    Object.defineProperty(ProductsComponent.prototype, "isFilteredByCategory", {
        get: function () {
            return Boolean(this.repo.productFilter.categoryId);
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(ProductsComponent.prototype, "idsForCompare", {
        get: function () {
            return this.comparsionSet.join(',');
        },
        enumerable: true,
        configurable: true
    });
    ProductsComponent.prototype.refreshImages = function () {
        var _this = this;
        var ids = this.repo.products.filter(function (pi) { return pi.preview != null; }).map(function (pi) { return pi.preview.id; });
        this.imageService.getPreviewImages(ids, function (images) { _this.images = images; });
    };
    ProductsComponent.prototype.toggleComparsion = function (id) {
        if (!this.isFilteredByCategory)
            return;
        if (this.comparsionSet.indexOf(id) == -1)
            this.comparsionSet.push(id);
        else
            this.comparsionSet = this.comparsionSet.filter(function (x) { return x != id; });
    };
    /*exp get products(): ProductInfo[] {
         // return this.searchProducts || this.repo.products;
         return this.repo.products;
     }*/
    //getImage(pi: ProductInfo): string {
    //    return this.images[pi.id];
    //}
    ProductsComponent.prototype.createProduct = function (descr, name) {
        var _this = this;
        // TODO create interface with only relevant info
        var prod = new Product('', new ProductInfo('', descr, 0, name), guidZero);
        this.repo.createOrEditEntity('product', prod, 0, 0, function (pi) { return _this.router.navigateByUrl('products/' + pi.id); });
    };
    ProductsComponent.prototype.deleteProduct = function (id) {
        this.repo.deleteEntity('product', id, 0, 0);
    };
    ProductsComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.repo.productsExperiment.subscribe(function (newProducts) {
            console.log('products changed');
            _this.productsExperiment = newProducts;
            _this.refreshImages();
        });
        this.getProducts();
    };
    ProductsComponent.prototype.getCategories = function () {
        this.categories = this.catService.cats ? this.catService.cats.map(function (cws) { return cws.cat; }) : [];
        console.log(this.categories);
    };
    ProductsComponent.prototype.getSubcategories = function (category) {
        this.categories = this.catService.getSubcategories(category);
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
        this.catService.getCategoryProps(category.id, function (filter) { _this.filter = filter; console.log(filter); });
    };
    ProductsComponent.prototype.setCurrentCategory = function (category) {
        this.repo.productFilter.categoryId = category.id;
        this.getProducts();
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
        __metadata("design:paramtypes", [RepositoryService,
            ImageService,
            CategoryService,
            Router])
    ], ProductsComponent);
    return ProductsComponent;
}());
export { ProductsComponent };
//# sourceMappingURL=products.component.js.map