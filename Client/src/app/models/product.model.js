var Product = /** @class */ (function () {
    function Product(id, info) {
        this.id = id;
        this.info = info;
    }
    return Product;
}());
export { Product };
var ProductInfo = /** @class */ (function () {
    function ProductInfo(id, description, name, watches, rating) {
        this.id = id;
        this.description = description;
        this.name = name;
        this.watches = watches;
        this.rating = rating;
    }
    return ProductInfo;
}());
export { ProductInfo };
var ProductWithSuppliers = /** @class */ (function () {
    function ProductWithSuppliers(product, suppliers) {
        this.product = product;
        this.suppliers = suppliers;
    }
    return ProductWithSuppliers;
}());
export { ProductWithSuppliers };
var SupplierAndPrice = /** @class */ (function () {
    function SupplierAndPrice(supplier, price) {
        this.supplier = supplier;
        this.price = price;
    }
    return SupplierAndPrice;
}());
export { SupplierAndPrice };
//# sourceMappingURL=product.model.js.map