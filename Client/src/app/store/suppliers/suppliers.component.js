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
import { Supplier, SupplierInfo, Location } from '../../models/supplier.model';
import { searchType } from '../search/search.service';
import { SearchInlineComponent } from '../search/search-inline.component';
import { ImageService } from '../../services/image.service';
import { Router } from '@angular/router';
var SuppliersComponent = /** @class */ (function () {
    function SuppliersComponent(repo, imageService, router) {
        this.repo = repo;
        this.imageService = imageService;
        this.router = router;
        this.type = searchType.supplier;
        this.page = 1;
        this.rpp = 10;
        this.images = {};
        this.templateText = 'Waiting for data...';
        this.imagg = {};
    }
    Object.defineProperty(SuppliersComponent.prototype, "suppliers", {
        get: function () {
            return this.searchSuppliers || this.repo.suppliers;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(SuppliersComponent.prototype, "pagesCount", {
        get: function () {
            return Math.ceil(this.repo.suppliersCount / this.rpp);
        },
        enumerable: true,
        configurable: true
    });
    SuppliersComponent.prototype.onPageChanged = function (page) {
        this.page = page;
        this.getSuppliers();
    };
    SuppliersComponent.prototype.getSuppliers = function () {
        var _this = this;
        this.repo.getEntities('supplier', this.page, this.rpp, function (res) {
            _this.imageService.getPreviewImages(res.map(function (si) { return si.id; }), function (smth) { return _this.imagg = smth; });
        });
    };
    SuppliersComponent.prototype.getImage = function (pi) {
        return this.images[pi.id];
    };
    SuppliersComponent.prototype.ngOnInit = function () {
        this.getSuppliers();
    };
    SuppliersComponent.prototype.createSupplier = function (name, address) {
        var _this = this;
        // TODO create interface with only relevant info
        var sup = new Supplier(new SupplierInfo(name, new Location(address)), []);
        // if (this.searchInline != undefined)
        this.repo.createOrEditEntity('supplier', sup, this.page, this.rpp, function (info) { return _this.router.navigateByUrl('suppliers/' + info.id); });
    };
    SuppliersComponent.prototype.deleteSupplier = function (id) {
        var _this = this;
        // if (this.searchInline)
        this.repo.deleteEntity('supplier', id, this.page, this.rpp, function () { return _this.searchInline.doSearch(); });
    };
    SuppliersComponent.prototype.onFound = function (suppliers) {
        this.templateText = 'Not found';
        this.searchSuppliers = suppliers;
    };
    __decorate([
        ViewChild(SearchInlineComponent),
        __metadata("design:type", SearchInlineComponent)
    ], SuppliersComponent.prototype, "searchInline", void 0);
    SuppliersComponent = __decorate([
        Component({
            selector: 'godsend-suppliers',
            templateUrl: './suppliers.component.html',
            styleUrls: [
                './suppliers.component.css',
                '../products/products.component.css'
            ]
        }),
        __metadata("design:paramtypes", [RepositoryService, ImageService, Router])
    ], SuppliersComponent);
    return SuppliersComponent;
}());
export { SuppliersComponent };
//# sourceMappingURL=suppliers.component.js.map