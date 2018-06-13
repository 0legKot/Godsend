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
    function Cart(discreteItems) {
        this.discreteItems = discreteItems;
    }
    return Cart;
}());
export { Cart };
var CartView = /** @class */ (function () {
    function CartView(discreteItems) {
        this.discreteItems = discreteItems;
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
//export class OrderPartWeightedSend extends OrderPartSend {
//    constructor(
//        public weight: number,
//        public productId: string,
//        public supplierId: string
//    ) {
//        super(productId, supplierId);
//    }
//}
//export function isDiscrete(part: OrderPartDiscreteView | OrderPartWeightedView): part is OrderPartDiscreteView {
//    return ((<OrderPartDiscreteView>part).quantity !== undefined);
//}
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
//export class OrderPartWeightedView extends OrderPartView {
//    constructor(
//        public product: Product,
//        public supplier: Supplier,
//        public price: number,
//        public weight: number
//    ) { super(product, supplier, price); }
//}
//# sourceMappingURL=cart.model.js.map