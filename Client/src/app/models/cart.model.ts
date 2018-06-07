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
        public productId: string,
        public supplierId: string
    ) { }
}

export class OrderPartDiscreteSend extends OrderPartSend {
    constructor(
        public quantity: number,
        public productId: string,
        public supplierId: string
    ) {
        super(productId, supplierId);
    }
}

export class OrderPartWeightedSend extends OrderPartSend {
    constructor(
        public weight: number,
        public productId: string,
        public supplierId: string
    ) {
        super(productId, supplierId);
    }
}

export function isDiscrete(part: OrderPartDiscreteSend | OrderPartWeightedSend): part is OrderPartDiscreteSend {
    return ((<OrderPartDiscreteSend>part).quantity !== undefined);
}