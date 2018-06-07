import { Supplier } from "./supplier.model";

export class Product {
    constructor(
        public id: string,
        public info: ProductInfo,
    ) { }
}

export class ProductInfo {
    constructor(
        public id: string,
        public description: string,
        public name: string,
        public watches: number,
        public rating: number
    ) { }
}

export class ProductWithSuppliers {
    constructor(
        public product: Product,
        public suppliers: SupplierAndPrice[]
    ) { }
}

export class SupplierAndPrice {
    constructor(
        public supplier: Supplier,
        public price: number
    ) { }
}
