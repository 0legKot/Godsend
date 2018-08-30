var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    }
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
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
import { SearchService, searchType } from './search.service';
import { SearchBaseComponent } from './search.base.component';
var SearchComponent = /** @class */ (function (_super) {
    __extends(SearchComponent, _super);
    function SearchComponent(ss) {
        var _this = _super.call(this) || this;
        _this.ss = ss;
        return _this;
    }
    SearchComponent.prototype.ngOnInit = function () {
        _super.prototype.ngOnInit.call(this);
    };
    SearchComponent.prototype.doSearch = function (term) {
        var _this = this;
        this.ss.findByType(searchType.all, term, function (res) {
            console.dir(res);
            _this.searchResult = res;
        });
    };
    SearchComponent = __decorate([
        Component({
            selector: 'godsend-search',
            templateUrl: './search.component.html',
            styleUrls: [
                '../suppliers/suppliers.component.css',
                '../products/products.component.css'
            ]
        }),
        __metadata("design:paramtypes", [SearchService])
    ], SearchComponent);
    return SearchComponent;
}(SearchBaseComponent));
export { SearchComponent };
//# sourceMappingURL=search.component.js.map