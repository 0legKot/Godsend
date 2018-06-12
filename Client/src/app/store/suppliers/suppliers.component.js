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
var SuppliersComponent = /** @class */ (function () {
    function SuppliersComponent(repo) {
        this.repo = repo;
        this.type = searchType.supplier;
        this.templateText = 'Waiting for data...';
        this.searchInline = !;
    }
    Object.defineProperty(SuppliersComponent.prototype, "suppliers", {
        get: function () {
            return this.searchSuppliers || this.repo.suppliers;
        },
        enumerable: true,
        configurable: true
    });
    SuppliersComponent.prototype.ngOnInit = function () {
        this.repo.getEntities('supplier');
    };
    SuppliersComponent.prototype.createSupplier = function (name, address) {
        var _this = this;
        // TODO create interface with oly relevant info
        var sup = new Supplier(new SupplierInfo(name, new Location(address)));
        this.repo.createSupplier(sup, function () { return _this.searchInline.doSearch(); });
    };
    SuppliersComponent.prototype.onFound = function (suppliers) {
        this.templateText = 'Not found';
        this.searchSuppliers = suppliers;
    };
    __decorate([
        ViewChild(SearchInlineComponent),
        __metadata("design:type", Object)
    ], SuppliersComponent.prototype, "searchInline", void 0);
    SuppliersComponent = __decorate([
        Component({
            selector: 'godsend-suppliers',
            templateUrl: './suppliers.component.html',
            styleUrls: ['./suppliers.component.css']
        }),
        __metadata("design:paramtypes", [RepositoryService])
    ], SuppliersComponent);
    return SuppliersComponent;
}());
export { SuppliersComponent };
//# sourceMappingURL=suppliers.component.js.map