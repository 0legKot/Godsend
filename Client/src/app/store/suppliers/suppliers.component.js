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
import { RepositoryService } from '../../services/repository.service';
import { searchType } from '../search/search.service';
var SuppliersComponent = /** @class */ (function () {
    function SuppliersComponent(repo) {
        this.repo = repo;
        this.type = searchType.supplier;
        this.suppliers = [];
    }
    SuppliersComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.repo.getEntities('supplier', function (s) { return _this.suppliers = s; });
    };
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