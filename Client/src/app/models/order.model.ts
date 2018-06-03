import { Product } from "./product.model";
import { Supplier } from "./supplier.model";

export const status = [
    'Ready', // 0
    'Shipped', // 1
    'Cancelled' // 2
]

export class Order {
    constructor(
        id: string,
        customer: IdentityUser,
        discreteItems: OrderPartDiscrete[],
        weightedItems: OrderPartWeighted[],
        ordered: string,
        status: number,
        done?: string) { }
}

export class IdentityUser {
    constructor(
        id: string,
        userName: string
    ) { }
}

export class OrderPart {
    constructor(
        id: string,
        product: Product,
        supplier: Supplier
    ) { }
}

export class OrderPartDiscrete extends OrderPart {
    constructor(quantity: number,
        id: string,
        product: Product,
        supplier: Supplier
        
    ) {
        super(id, product, supplier);
    }
}

export class OrderPartWeighted extends OrderPart {
    constructor(
        weight: number,
        id: string,
        product: Product,
        supplier: Supplier
    ) {
        super(id, product, supplier);
    }
}
