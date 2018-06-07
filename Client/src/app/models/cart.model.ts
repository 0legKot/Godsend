import { IdentityUser } from "./user.model";
import { OrderPartDiscrete, OrderPartWeighted, OrderPart } from "./order.model";

export class Cart {
    constructor(
        id: string,
        public customer: IdentityUser,
        public discreteItems: OrderPartDiscrete[],
        public weightedItems: OrderPartWeighted[],
        public ordered: string
    ) { }
}