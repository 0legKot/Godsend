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
export var guidZero = '00000000-0000-0000-0000-000000000000';
var Cart = /** @class */ (function () {
    function Cart(discreteItems, weightedItems) {
        this.discreteItems = discreteItems;
        this.weightedItems = weightedItems;
    }
    return Cart;
}());
export { Cart };
var CartView = /** @class */ (function () {
    function CartView(discreteItems, weightedItems) {
        this.discreteItems = discreteItems;
        this.weightedItems = weightedItems;
    }
    return CartView;
}());
export { CartView };
var OrderPartSend = /** @class */ (function () {
    function OrderPartSend(productId, supplierId) {
        this.productId = productId;
        this.supplierId = supplierId;
    }
    return OrderPartSend;
}());
export { OrderPartSend };
var OrderPartDiscreteSend = /** @class */ (function (_super) {
    __extends(OrderPartDiscreteSend, _super);
    function OrderPartDiscreteSend(quantity, productId, supplierId) {
        var _this = _super.call(this, productId, supplierId) || this;
        _this.quantity = quantity;
        _this.productId = productId;
        _this.supplierId = supplierId;
        return _this;
    }
    return OrderPartDiscreteSend;
}(OrderPartSend));
export { OrderPartDiscreteSend };
var OrderPartWeightedSend = /** @class */ (function (_super) {
    __extends(OrderPartWeightedSend, _super);
    function OrderPartWeightedSend(weight, productId, supplierId) {
        var _this = _super.call(this, productId, supplierId) || this;
        _this.weight = weight;
        _this.productId = productId;
        _this.supplierId = supplierId;
        return _this;
    }
    return OrderPartWeightedSend;
}(OrderPartSend));
export { OrderPartWeightedSend };
export function isDiscrete(part) {
    return (part.quantity !== undefined);
}
var OrderPartView = /** @class */ (function () {
    function OrderPartView(product, supplier, price) {
        this.product = product;
        this.supplier = supplier;
        this.price = price;
    }
    return OrderPartView;
}());
export { OrderPartView };
var OrderPartDiscreteView = /** @class */ (function (_super) {
    __extends(OrderPartDiscreteView, _super);
    function OrderPartDiscreteView(product, supplier, price, quantity) {
        var _this = _super.call(this, product, supplier, price) || this;
        _this.product = product;
        _this.supplier = supplier;
        _this.price = price;
        _this.quantity = quantity;
        return _this;
    }
    return OrderPartDiscreteView;
}(OrderPartView));
export { OrderPartDiscreteView };
var OrderPartWeightedView = /** @class */ (function (_super) {
    __extends(OrderPartWeightedView, _super);
    function OrderPartWeightedView(product, supplier, price, weight) {
        var _this = _super.call(this, product, supplier, price) || this;
        _this.product = product;
        _this.supplier = supplier;
        _this.price = price;
        _this.weight = weight;
        return _this;
    }
    return OrderPartWeightedView;
}(OrderPartView));
export { OrderPartWeightedView };
//# sourceMappingURL=cart.model.js.map