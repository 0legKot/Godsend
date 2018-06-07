import { IdentityUser } from "./user.model";
import { OrderPartDiscrete, OrderPartWeighted, OrderPart } from "./order.model";

export const guidZero = '00000000-0000-0000-0000-000000000000';

export class Cart {
    constructor(
        public discreteItems: OrderPartDiscreteSend[],
        public weightedItems: OrderPartWeightedSend[],
    ) { }
}

export class OrderPartSend {
    constructor(
        productId: string,
        supplierId: string
    ) { }
}

export class OrderPartDiscreteSend extends OrderPartSend {
    constructor(
        quantity: number,
        productId: string,
        supplierId: string
    ) {
        super(productId, supplierId);
    }
}

export class OrderPartWeightedSend extends OrderPartSend {
    constructor(
        weight: number,
        productId: string,
        supplierId: string
    ) {
        super(productId, supplierId);
    }
}