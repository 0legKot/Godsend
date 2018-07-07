var Product = /** @class */ (function () {
    function Product(id, info) {
        this.id = id;
        this.info = info;
    }
    Product.EnsureType = function (product) {
        return new this(product.id, product.info);
    };
    Product.prototype.toCreateEdit = function () {
        return {
            id: this.id || undefined,
            info: {
                name: this.info.name,
                description: this.info.description
            }
        };
    };
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
var Category = /** @class */ (function () {
    function Category(id, name, baseCategory) {
        this.id = id;
        this.name = name;
        this.baseCategory = baseCategory;
    }
    return Category;
}());
export { Category };
var CatsWithSubs = /** @class */ (function () {
    function CatsWithSubs(cat, subs) {
        this.cat = cat;
        this.subs = subs;
    }
    return CatsWithSubs;
}());
export { CatsWithSubs };
var FilterInfo = /** @class */ (function () {
    function FilterInfo() {
        this.orderBy = 0;
    }
    return FilterInfo;
}());
export { FilterInfo };
var DecimalPropertyInfo = /** @class */ (function () {
    function DecimalPropertyInfo(propId, left, right) {
        this.propId = propId;
        this.left = left;
        this.right = right;
    }
    return DecimalPropertyInfo;
}());
export { DecimalPropertyInfo };
var IntPropertyInfo = /** @class */ (function () {
    function IntPropertyInfo(propId, left, right) {
        this.propId = propId;
        this.left = left;
        this.right = right;
    }
    return IntPropertyInfo;
}());
export { IntPropertyInfo };
var StringPropertyInfo = /** @class */ (function () {
    function StringPropertyInfo(propId, part) {
        this.propId = propId;
        this.part = part;
    }
    return StringPropertyInfo;
}());
export { StringPropertyInfo };
var FilterInfoView = /** @class */ (function () {
    function FilterInfoView() {
        this.orderBy = 0;
        this.sortAscending = true;
    }
    return FilterInfoView;
}());
export { FilterInfoView };
var DecimalPropertyInfoView = /** @class */ (function () {
    function DecimalPropertyInfoView(propId, name) {
        this.propId = propId;
        this.name = name;
        this.left = null;
        this.right = null;
    }
    return DecimalPropertyInfoView;
}());
export { DecimalPropertyInfoView };
var IntPropertyInfoView = /** @class */ (function () {
    function IntPropertyInfoView(propId, name) {
        this.propId = propId;
        this.name = name;
        this.left = null;
        this.right = null;
    }
    return IntPropertyInfoView;
}());
export { IntPropertyInfoView };
var StringPropertyInfoView = /** @class */ (function () {
    function StringPropertyInfoView(propId, name) {
        this.propId = propId;
        this.name = name;
        this.part = '';
    }
    return StringPropertyInfoView;
}());
export { StringPropertyInfoView };
var Property = /** @class */ (function () {
    function Property() {
        this.id = '';
        this.name = '';
        this.type = -1;
    }
    return Property;
}());
export { Property };
var ProductFilterInfo = /** @class */ (function () {
    function ProductFilterInfo(quantity, page) {
        this.quantity = quantity;
        this.page = page;
        this.orderBy = 0;
        this.sortAscending = false;
    }
    return ProductFilterInfo;
}());
export { ProductFilterInfo };
export var propertyType = [
    'int',
    'string',
    'decimal' //2
];
export var orderBy = [
    'name',
    'rating',
    'watches'
];
//# sourceMappingURL=product.model.js.map