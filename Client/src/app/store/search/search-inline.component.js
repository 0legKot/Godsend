var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
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
import { Component, Input, Output, EventEmitter } from '@angular/core';
import { SearchService, searchType } from './search.service';
import { SearchBaseComponent } from './search.base.component';
var SearchInlineComponent = /** @class */ (function (_super) {
    __extends(SearchInlineComponent, _super);
    function SearchInlineComponent(ss) {
        var _this = _super.call(this) || this;
        _this.ss = ss;
        _this.type = searchType.all;
        _this.found = new EventEmitter();
        return _this;
    }
    SearchInlineComponent.prototype.ngOnInit = function () {
        _super.prototype.ngOnInit.call(this);
    };
    SearchInlineComponent.prototype.doSearch = function (term) {
        var _this = this;
        this.ss.findByType(this.type, term, function (res) { return _this.found.emit(res); });
    };
    __decorate([
        Input(),
        __metadata("design:type", Number)
    ], SearchInlineComponent.prototype, "type", void 0);
    __decorate([
        Output(),
        __metadata("design:type", Object)
    ], SearchInlineComponent.prototype, "found", void 0);
    SearchInlineComponent = __decorate([
        Component({
            selector: 'godsend-search-inline',
            templateUrl: './search-inline.component.html',
        }),
        __metadata("design:paramtypes", [SearchService])
    ], SearchInlineComponent);
    return SearchInlineComponent;
}(SearchBaseComponent));
export { SearchInlineComponent };
//# sourceMappingURL=search-inline.component.js.map