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
    'Processing' // 3
];
var Order = /** @class */ (function () {
    function Order(id, customer, items, 
        //public weightedItems: OrderPartWeighted[],
        ordered, status, done) {
        this.id = id;
        this.customer = customer;
        this.items = items;
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
var OrderPartProducts = /** @class */ (function (_super) {
    __extends(OrderPartProducts, _super);
    function OrderPartProducts(quantity, multiplier, id, product, supplier) {
        var _this = _super.call(this, id, product, supplier) || this;
        _this.quantity = quantity;
        _this.multiplier = multiplier;
        _this.id = id;
        _this.product = product;
        _this.supplier = supplier;
        return _this;
    }
    return OrderPartProducts;
}(OrderPart));
export { OrderPartProducts };
//export class OrderPartWeighted extends OrderPart {
//    constructor(
//        weight: number,
//        id: string,
//        product: Product,
//        supplier: Supplier
//    ) {
//        super(id, product, supplier);
//    }
//}
//# sourceMappingURL=order.model.js.map