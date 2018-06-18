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
var SupplierDetailComponent = /** @class */ (function () {
    function SupplierDetailComponent(route, router, service, imageService) {
        this.route = route;
        this.router = router;
        this.service = service;
        this.imageService = imageService;
        this.image = '';
        this.backup = {
            name: '',
            address: ''
        };
        this.edit = false;
    }
    SupplierDetailComponent.prototype.deleteSupplier = function () {
        if (this.supp) {
            this.service.deleteEntity('supplier', this.supp.info.id);
            this.gotoSuppliers(undefined);
        }
    };
    SupplierDetailComponent.prototype.gotoSuppliers = function (supplier) {
        var supplierId = supplier ? supplier.id : null;
        this.router.navigate(['/suppliers', { id: supplierId }]);
    };
    SupplierDetailComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.service.getEntity(this.route.snapshot.params.id, function (s) { return _this.supp = s; }, 'supplier');
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
            this.service.createOrEditEntity('supplier', Supplier.EnsureType(this.supp));
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
            ImageService])
    ], SupplierDetailComponent);
    return SupplierDetailComponent;
}());
export { SupplierDetailComponent };
//# sourceMappingURL=supplier-detail.component.js.map