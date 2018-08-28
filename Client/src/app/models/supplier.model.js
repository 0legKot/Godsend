var Supplier = /** @class */ (function () {
    function Supplier(info, productsAndPrices, id) {
        if (id === void 0) { id = ''; }
        this.info = info;
        this.productsAndPrices = productsAndPrices;
        this.id = id;
    }
    Supplier.EnsureType = function (sup) {
        return new Supplier(sup.info, sup.productsAndPrices, sup.id);
    };
    Supplier.prototype.toCreateEdit = function () {
        return {
            id: this.id || undefined,
            info: {
                name: this.info.name,
                location: {
                    address: this.info.location.address
                }
            }
        };
    };
    return Supplier;
}());
export { Supplier };
var SupplierInfo = /** @class */ (function () {
    function SupplierInfo(name, location, id, watches, rating) {
        if (id === void 0) { id = ''; }
        if (watches === void 0) { watches = 0; }
        if (rating === void 0) { rating = 0; }
        this.name = name;
        this.location = location;
        this.id = id;
        this.watches = watches;
        this.rating = rating;
    }
    return SupplierInfo;
}());
export { SupplierInfo };
var Location = /** @class */ (function () {
    function Location(address, id) {
        if (id === void 0) { id = ''; }
        this.address = address;
        this.id = id;
    }
    return Location;
}());
export { Location };
var ProductAndPrice = /** @class */ (function () {
    function ProductAndPrice(productInfo, price, id) {
        this.productInfo = productInfo;
        this.price = price;
        this.id = id;
    }
    return ProductAndPrice;
}());
export { ProductAndPrice };
//# sourceMappingURL=supplier.model.js.map