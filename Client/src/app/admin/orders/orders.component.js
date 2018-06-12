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
import { orderStatus } from '../../models/order.model';
var OrdersComponent = /** @class */ (function () {
    function OrdersComponent(repo) {
        this.repo = repo;
        this.status = orderStatus;
    }
    OrdersComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.repo.getEntities('order', function (o) { return _this.orders = o; });
    };
    OrdersComponent.prototype.cancel = function (id) {
        var _this = this;
        this.repo.changeOrderStatus(id, 2, function (res) { return _this.orders = res; });
    };
    OrdersComponent.prototype.shipped = function (id) {
        var _this = this;
        this.repo.changeOrderStatus(id, 1, function (res) { return _this.orders = res; });
    };
    OrdersComponent.prototype.delete = function (id) {
        var _this = this;
        this.repo.deleteOrder(id, function (res) { return _this.orders = res; });
    };
    // TODO:rework
    OrdersComponent.prototype.getProdInfo = function (arDProd) {
        var res = [];
        for (var _i = 0, arDProd_1 = arDProd; _i < arDProd_1.length; _i++) {
            var p = arDProd_1[_i];
            res.push(p.product.info.name);
        }
        return res;
    };
    OrdersComponent = __decorate([
        Component({
            selector: 'godsend-orders',
            templateUrl: './orders.component.html'
        }),
        __metadata("design:paramtypes", [RepositoryService])
    ], OrdersComponent);
    return OrdersComponent;
}());
export { OrdersComponent };
//# sourceMappingURL=orders.component.js.map