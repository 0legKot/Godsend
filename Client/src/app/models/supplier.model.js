var Supplier = /** @class */ (function () {
    function Supplier(info, id) {
        if (id === void 0) { id = ''; }
        this.info = info;
        this.id = id;
    }
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
var SupplierCreate = /** @class */ (function () {
    function SupplierCreate(name, address) {
        this.info = new SupplierInfoCreate(name, address);
    }
    SupplierCreate.FromSupplier = function (supplier) {
        return new this(supplier.info.name, supplier.info.location.address);
    };
    return SupplierCreate;
}());
export { SupplierCreate };
var SupplierInfoCreate = /** @class */ (function () {
    function SupplierInfoCreate(name, address) {
        this.name = name;
        this.location = new Location(address);
    }
    return SupplierInfoCreate;
}());
export { SupplierInfoCreate };
//# sourceMappingURL=supplier.model.js.map