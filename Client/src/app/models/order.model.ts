import { ProductInfo } from './product.model';
import { SupplierInfo } from './supplier.model';
import { IdentityUser } from './user.model';

export const orderStatus = [
    'Ready', // 0
    'Shipped', // 1
    'Cancelled', // 2
    'Processing' // 3
];

export class Order {
    constructor(
        public id: string,
        public customer: IdentityUser,
        public items: OrderPartProducts[],
        public ordered: string,
        public status: number,
        public done?: string) { }
}

export class OrderPart {
    constructor(
        id: string,
        product: ProductInfo,
        supplier: SupplierInfo
    ) { }
}

export class OrderPartProducts extends OrderPart {
    constructor(
        public quantity: number,
        public multiplier: number,
        public id: string,
        public product: ProductInfo,
        public supplier: SupplierInfo
    ) {
        super(id, product, supplier);
    }
}

// export class OrderPartWeighted extends OrderPart {
//    constructor(
//        weight: number,
//        id: string,
//        product: Product,
//        supplier: Supplier
//    ) {
//        super(id, product, supplier);
//    }
// }
