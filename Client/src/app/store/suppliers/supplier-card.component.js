var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component, Input, EventEmitter, Output } from '@angular/core';
import { SupplierInfo } from '../../models/supplier.model';
import { RepositoryService } from '../../services/repository.service';
import { ImageService } from '../../services/image.service';
var SupplierCardComponent = /** @class */ (function () {
    function SupplierCardComponent(repo, imageService) {
        this.repo = repo;
        this.imageService = imageService;
        this.delete = new EventEmitter();
    }
    Object.defineProperty(SupplierCardComponent.prototype, "viewed", {
        get: function () {
            var _this = this;
            return this.supplierInfo && (this.repo.viewedSuppliersIds.find(function (id) { return id === _this.supplierInfo.id; }) !== undefined);
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(SupplierCardComponent.prototype, "imagePath", {
        get: function () {
            if (this.supplierInfo && this.supplierInfo.preview) {
                return this.imageService.getImagePath(this.supplierInfo.preview.id);
            }
            else {
                return '';
            }
        },
        enumerable: true,
        configurable: true
    });
    SupplierCardComponent.prototype.onDelete = function () {
        this.delete.emit();
    };
    __decorate([
        Input(),
        __metadata("design:type", SupplierInfo)
    ], SupplierCardComponent.prototype, "supplierInfo", void 0);
    __decorate([
        Output(),
        __metadata("design:type", Object)
    ], SupplierCardComponent.prototype, "delete", void 0);
    SupplierCardComponent = __decorate([
        Component({
            selector: 'godsend-supplier-card[supplierInfo]',
            templateUrl: './supplier-card.component.html',
            styleUrls: ['./suppliers.component.css']
        }),
        __metadata("design:paramtypes", [RepositoryService, ImageService])
    ], SupplierCardComponent);
    return SupplierCardComponent;
}());
export { SupplierCardComponent };
//# sourceMappingURL=supplier-card.component.js.map