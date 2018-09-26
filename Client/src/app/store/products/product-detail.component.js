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
import { Router, ActivatedRoute } from '@angular/router';
import { Product, SupplierAndPrice, EAV, propertyType } from '../../models/product.model';
import { RepositoryService } from '../../services/repository.service';
import { CartService } from '../../services/cart.service';
import { StorageService } from '../../services/storage.service';
import { CategoryService } from '../../services/category.service';
import { searchType } from '../search/search.service';
var ProductDetailComponent = /** @class */ (function () {
    function ProductDetailComponent(route, router, repo, cart, 
    // private imageService: ImageService,
    storage, catService) {
        this.route = route;
        this.router = router;
        this.repo = repo;
        this.cart = cart;
        this.storage = storage;
        this.catService = catService;
        this.quantity = 1;
        this.edit = false;
        this.searchTypeSupplier = searchType.supplier;
        this.clas = 'product';
        // images: string[] = [];
        this.backup = {
            name: '',
            description: '',
        };
    }
    Object.defineProperty(ProductDetailComponent.prototype, "price", {
        get: function () {
            return this.selectedSupplier ? (this.selectedSupplier.price * this.quantity).toFixed(2) : '';
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(ProductDetailComponent.prototype, "authenticated", {
        get: function () {
            return this.storage.authenticated;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(ProductDetailComponent.prototype, "cats", {
        get: function () {
            return this.catService.cats;
        },
        enumerable: true,
        configurable: true
    });
    ProductDetailComponent.prototype.deleteProduct = function () {
        if (this.product) {
            this.repo.deleteEntity('product', this.product.info.id, 1, 10);
            this.router.navigate(['/products']);
        }
    };
    ProductDetailComponent.prototype.buy = function () {
        // Todo make button disabled if no data?
        if (this.product == null || this.selectedSupplier == null) {
            console.log('ERROR: no data');
            return;
        }
        var op = {
            quantity: this.quantity,
            productInfo: this.product.info,
            supplierInfo: this.selectedSupplier.supplierInfo,
            price: this.selectedSupplier.price
        };
        this.cart.addToCart(op);
    };
    ProductDetailComponent.prototype.editMode = function () {
        if (this.product == null) {
            console.log('no data');
        }
        else {
            this.backup = {
                name: this.product.info.name,
                description: this.product.info.description,
                cat: this.product.jsonCategory,
                decimalProps: this.product.decimalProps,
                intProps: this.product.intProps,
                stringProps: this.product.stringProps,
                images: this.product.images
            };
            this.edit = true;
        }
    };
    ProductDetailComponent.prototype.save = function () {
        if (this.product) {
            this.repo.createOrEditEntity('product', Product.EnsureType(this.product), 1, 10);
        }
        this.edit = false;
    };
    ProductDetailComponent.prototype.discard = function () {
        if (this.product) {
            this.product.info.name = this.backup.name;
            this.product.info.description = this.backup.description;
            this.product.jsonCategory = this.backup.cat;
            this.product.stringProps = this.backup.stringProps;
            this.product.intProps = this.backup.intProps;
            this.product.decimalProps = this.backup.decimalProps;
            this.product.images = this.backup.images;
        }
        this.edit = false;
    };
    ProductDetailComponent.prototype.ngOnInit = function () {
        var _this = this;
        var id = this.route.snapshot.params.id;
        this.repo.getEntity('product', id, function (p) {
            _this.product = p;
            _this.selectedSupplier = p.suppliersAndPrices ? p.suppliersAndPrices[0] : undefined;
            /*if (this.product.images) {
                this.imageService.getImages(this.product.images.map(i => i.id), images => { this.images = images; });
            }*/
        });
        if (this.repo.viewedProductsIds.find(function (x) { return x === id; }) === undefined) {
            this.repo.viewedProductsIds.push(this.route.snapshot.params.id);
        }
    };
    ProductDetailComponent.prototype.changeCategory = function (newCat) {
        if (this.product) {
            this.product.jsonCategory = newCat;
            this.refreshProperties(newCat.id);
        }
    };
    ProductDetailComponent.prototype.refreshProperties = function (catId) {
        var _this = this;
        this.catService.getCategoryProps(catId, function (filter) {
            if (_this.product) {
                if (filter.decimalProps) {
                    _this.product.decimalProps = filter.decimalProps.map(function (dp) {
                        return new EAV(_this.product.id, { id: dp.propId, name: dp.name, type: propertyType.indexOf('decimal') }, 0);
                    });
                }
                else {
                    _this.product.decimalProps = [];
                }
                if (filter.stringProps) {
                    _this.product.stringProps = filter.stringProps.map(function (sp) {
                        return new EAV(_this.product.id, { id: sp.propId, name: sp.name, type: propertyType.indexOf('string') }, '');
                    });
                }
                else {
                    _this.product.stringProps = [];
                }
                if (filter.intProps) {
                    _this.product.intProps = filter.intProps.map(function (ip) {
                        return new EAV(_this.product.id, { id: ip.propId, name: ip.name, type: propertyType.indexOf('int') }, 0);
                    });
                }
                else {
                    _this.product.intProps = [];
                }
            }
        });
    };
    ProductDetailComponent.prototype.refreshFoundSuppliers = function (newData) {
        this.foundSuppliers = newData.suppliersInfo;
    };
    ProductDetailComponent.prototype.addSupplier = function () {
        // do something
        if (this.product && this.supplierToAdd && this.priceToAdd) {
            if (this.product.suppliersAndPrices == null) {
                this.product.suppliersAndPrices = [];
            }
            this.product.suppliersAndPrices.push(new SupplierAndPrice(this.supplierToAdd, this.priceToAdd));
            this.supplierToAdd = undefined;
            this.priceToAdd = 0;
        }
    };
    ProductDetailComponent.prototype.removeSupplier = function (snp) {
        if (this.product && this.product.suppliersAndPrices) {
            this.product.suppliersAndPrices = this.product.suppliersAndPrices.filter(function (s) { return s !== snp; });
        }
    };
    ProductDetailComponent.prototype.setImages = function (images) {
        if (this.product) {
            this.product.images = images;
        }
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
            StorageService,
            CategoryService])
    ], ProductDetailComponent);
    return ProductDetailComponent;
}());
export { ProductDetailComponent };
//# sourceMappingURL=product-detail.component.js.map