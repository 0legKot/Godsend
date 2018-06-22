var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Injectable } from '@angular/core';
import { DataService } from '../../services/data.service';
export var searchType = {
    all: 0,
    product: 1,
    supplier: 2,
    order: 3
};
var SearchService = /** @class */ (function () {
    function SearchService(data) {
        this.data = data;
        this.page = 1;
        this.rpp = 15;
    }
    SearchService.prototype.findProducts = function (term, fn) {
        this.data.sendRequest('get', 'api/search/products/' + term + '/' + this.page + '/' + this.rpp)
            .subscribe(function (products) { return fn(products); });
    };
    SearchService.prototype.findSuppliers = function (term, fn) {
        this.data.sendRequest('get', 'api/search/suppliers/' + term + '/' + this.page + '/' + this.rpp)
            .subscribe(function (suppliers) { return fn(suppliers); });
    };
    SearchService.prototype.findAll = function (term, fn) {
        this.data.sendRequest('get', 'api/search/all/' + term + '/' + this.page + '/' + this.rpp)
            .subscribe(function (result) { return fn(result); });
    };
    SearchService.prototype.findByType = function (type, term, fn) {
        this.data.sendRequest('get', 'api/search/type/' + type + '/' + term + '/' + this.page + '/' + this.rpp)
            .subscribe(function (items) { return fn(items); });
    };
    SearchService = __decorate([
        Injectable(),
        __metadata("design:paramtypes", [DataService])
    ], SearchService);
    return SearchService;
}());
export { SearchService };
var AllSearchResult = /** @class */ (function () {
    function AllSearchResult() {
    }
    return AllSearchResult;
}());
export { AllSearchResult };
//# sourceMappingURL=search.service.js.map