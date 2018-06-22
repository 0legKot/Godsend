var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Product } from '../models/product.model';
import { Injectable } from '@angular/core';
import { DataService } from './data.service';
import { Order } from '../models/order.model';
import { Supplier } from '../models/supplier.model';
import { Cart, OrderPartDiscreteSend } from '../models/cart.model';
var productsUrl = 'api/product';
var ordersUrl = 'api/order';
var suppliersUrl = 'api/supplier';
var articlesUrl = 'api/article';
// TODO: rework
var RepositoryService = /** @class */ (function () {
    function RepositoryService(data) {
        this.data = data;
        this.product = {};
        this.products = [];
        this.orders = [];
        this.order = {};
        this.suppliers = [];
        this.supplier = {};
        this.articles = [];
        this.article = {};
    }
    RepositoryService.prototype.getSavedEntities = function (clas) {
        switch (clas) {
            case 'product':
                return this.products;
            case 'order':
                return this.orders;
            case 'supplier':
                return this.suppliers;
            case 'article':
                return this.articles;
        }
    };
    RepositoryService.prototype.setEntity = function (val) {
        switch (typeof (val)) {
            case typeof (Product):
                this.product = val;
                break;
            case typeof (Supplier):
                this.supplier = val;
                break;
            case typeof (Order):
                this.order = val;
                break;
            default: return;
        }
    };
    RepositoryService.prototype.setEntites = function (clas, val) {
        switch (clas) {
            case 'product':
                this.products = val;
                break;
            case 'order':
                this.orders = val;
                break;
            case 'supplier':
                this.suppliers = val;
                break;
            case 'article':
                this.articles = val;
                break;
        }
    };
    RepositoryService.prototype.getUrl = function (clas) {
        switch (clas) {
            case 'product': return productsUrl;
            case 'order': return ordersUrl;
            case 'supplier': return suppliersUrl;
            case 'article': return articlesUrl;
        }
        return 'urlNotDetected';
    };
    RepositoryService.prototype.getEntity = function (id, fn, clas) {
        var _this = this;
        if (id != null) {
            var url = this.getUrl(clas);
            this.data.sendRequest('get', url + '/detail/' + id)
                .subscribe(function (response) {
                _this.setEntity(response);
                if (fn) {
                    fn(response);
                }
            });
        }
    };
    RepositoryService.prototype.changeOrderStatus = function (id, status, page, rpp, fn) {
        var _this = this;
        this.data.sendRequest('patch', ordersUrl + '/changeStatus/' + id + '/' + status)
            .subscribe(function (response) {
            _this.getEntities('order', page, rpp, fn);
        });
    };
    RepositoryService.prototype.deleteOrder = function (id, page, rpp, fn) {
        var _this = this;
        this.data.sendRequest('delete', ordersUrl + '/delete/' + id)
            .subscribe(function (response) {
            _this.getEntities('order', page, rpp, fn);
        });
    };
    RepositoryService.prototype.getEntities = function (clas, page, rpp, fn) {
        var _this = this;
        var url = this.getUrl(clas);
        //const page = 1;
        //const rpp = 15;
        this.data.sendRequest('get', url + '/all/' + page + '/' + rpp)
            .subscribe(function (response) {
            if (fn) {
                fn(response);
            }
            console.log(response);
            _this.setEntites(clas, response);
        });
    };
    RepositoryService.prototype.createOrder = function (cartView) {
        var _this = this;
        console.dir(cartView);
        var cart = new Cart(cartView.discreteItems.map(function (opdv) { return new OrderPartDiscreteSend(opdv.quantity, opdv.product.id, opdv.supplier.id); }));
        this.data.sendRequest('post', ordersUrl + '/createOrUpdate', cart)
            .subscribe(function (response) {
            _this.orders.push(response);
        });
    };
    RepositoryService.prototype.createOrEditEntity = function (clas, entity, page, rpp, fn) {
        var _this = this;
        var createEditData = entity.toCreateEdit();
        var url = this.getUrl(clas);
        this.data.sendRequest('post', url + '/CreateOrUpdate', createEditData)
            .subscribe(function (response) {
            entity.info.id = response;
            _this.getEntities(clas, page, rpp);
            if (fn) {
                fn(entity.info);
            }
        });
    };
    // deprecated?
    RepositoryService.prototype.replaceProduct = function (prod, page, rpp) {
        var _this = this;
        var data = {
            name: prod.info.name,
            description: prod.info.description
        };
        this.data.sendRequest('put', productsUrl + '/' + prod.id, data)
            .subscribe(function (response) { return _this.getEntities('product', page, rpp); });
    };
    RepositoryService.prototype.updateEntity = function (clas, id, changes, page, rpp) {
        var _this = this;
        var url = this.getUrl(clas);
        var patch = [];
        changes.forEach(function (value, key) {
            return patch.push({ op: 'replace', path: key, value: value });
        });
        this.data.sendRequest('patch', url + '/' + id, patch)
            .subscribe(function (response) { return _this.getEntities(clas, page, rpp); });
    };
    RepositoryService.prototype.deleteEntity = function (clas, id, page, rpp, fn) {
        var _this = this;
        var url = this.getUrl(clas);
        this.data.sendRequest('delete', url + '/delete/' + id)
            .subscribe(function (response) { _this.getEntities(clas, page, rpp); if (fn) {
            fn();
        } });
    };
    RepositoryService.prototype.storeSessionData = function (dataType, data) {
        return this.data.sendRequest('post', '/api/session/' + dataType, data)
            .subscribe(function (response) { });
    };
    RepositoryService.prototype.getSessionData = function (dataType) {
        return this.data.sendRequest('get', '/api/session/' + dataType);
    };
    RepositoryService.prototype.getByCategory = function (cat, fn) {
        var _this = this;
        this.data.sendRequest('get', 'api/product/getByCategory/' + cat.id)
            .subscribe(function (products) {
            _this.products = products;
            if (fn)
                fn(products);
        });
    };
    RepositoryService = __decorate([
        Injectable({
            providedIn: 'root'
        }),
        __metadata("design:paramtypes", [DataService])
    ], RepositoryService);
    return RepositoryService;
}());
export { RepositoryService };
//# sourceMappingURL=repository.service.js.map