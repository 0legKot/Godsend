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
import { Supplier } from '../../models/supplier.model';
import { ImageService } from '../../services/image.service';
import { StorageService } from '../../services/storage.service';
var SupplierDetailComponent = /** @class */ (function () {
    function SupplierDetailComponent(route, router, repo, imageService, storage) {
        this.route = route;
        this.router = router;
        this.repo = repo;
        this.imageService = imageService;
        this.storage = storage;
        this.image = '';
        this.backup = {
            name: '',
            address: ''
        };
        this.edit = false;
        this.clas = 'supplier';
    }
    Object.defineProperty(SupplierDetailComponent.prototype, "authenticated", {
        get: function () {
            return this.storage.authenticated;
        },
        enumerable: true,
        configurable: true
    });
    SupplierDetailComponent.prototype.deleteSupplier = function () {
        if (this.supp) {
            this.repo.deleteEntity('supplier', this.supp.info.id, 1, 10);
            this.gotoSuppliers(undefined);
        }
    };
    SupplierDetailComponent.prototype.gotoSuppliers = function (supplier) {
        var supplierId = supplier ? supplier.id : null;
        this.router.navigate(['/suppliers', { id: supplierId }]);
    };
    SupplierDetailComponent.prototype.gotoProduct = function (prodId) {
        this.router.navigate(['/products/' + prodId]);
    };
    SupplierDetailComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.repo.getEntity('supplier', this.route.snapshot.params.id, function (s) {
            _this.supp = s;
            console.log(s.productsAndPrices);
        });
        this.imageService.getImage(this.route.snapshot.params.id, function (image) { _this.image = image; });
    };
    SupplierDetailComponent.prototype.editMode = function () {
        if (this.supp == null) {
            console.log('no data');
            return;
        }
        this.backup = {
            name: this.supp.info.name,
            address: this.supp.info.location.address
        };
        this.edit = true;
    };
    SupplierDetailComponent.prototype.save = function () {
        if (this.supp) {
            console.log('EDIT');
            console.log(this.supp);
            this.repo.createOrEditEntity('supplier', Supplier.EnsureType(this.supp), 1, 10);
        }
        this.edit = false;
    };
    SupplierDetailComponent.prototype.discard = function () {
        if (this.supp) {
            this.supp.info.name = this.backup.name;
            this.supp.info.location.address = this.backup.address;
        }
        this.edit = false;
    };
    SupplierDetailComponent = __decorate([
        Component({
            selector: 'godsend-supplier-detail',
            templateUrl: 'supplier-detail.component.html',
            styleUrls: ['./supplier-detail.component.css']
        }),
        __metadata("design:paramtypes", [ActivatedRoute,
            Router,
            RepositoryService,
            ImageService,
            StorageService])
    ], SupplierDetailComponent);
    return SupplierDetailComponent;
}());
export { SupplierDetailComponent };
//# sourceMappingURL=supplier-detail.component.js.map