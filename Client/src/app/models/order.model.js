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
export var orderStatus = [
    'Ready',
    'Shipped',
    'Cancelled',
    'Processing' //3
];
var Order = /** @class */ (function () {
    function Order(id, customer, discreteItems, weightedItems, ordered, status, done) {
        this.id = id;
        this.customer = customer;
        this.discreteItems = discreteItems;
        this.weightedItems = weightedItems;
        this.ordered = ordered;
        this.status = status;
        this.done = done;
    }
    return Order;
}());
export { Order };
var OrderPart = /** @class */ (function () {
    function OrderPart(id, product, supplier) {
    }
    return OrderPart;
}());
export { OrderPart };
var OrderPartDiscrete = /** @class */ (function (_super) {
    __extends(OrderPartDiscrete, _super);
    function OrderPartDiscrete(quantity, id, product, supplier) {
        return _super.call(this, id, product, supplier) || this;
    }
    return OrderPartDiscrete;
}(OrderPart));
export { OrderPartDiscrete };
var OrderPartWeighted = /** @class */ (function (_super) {
    __extends(OrderPartWeighted, _super);
    function OrderPartWeighted(weight, id, product, supplier) {
        return _super.call(this, id, product, supplier) || this;
    }
    return OrderPartWeighted;
}(OrderPart));
export { OrderPartWeighted };
//# sourceMappingURL=order.model.js.map