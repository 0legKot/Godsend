import { OrderPart, OrderPartWeighted, OrderPartDiscrete } from "./order.model";
import { Cart } from "./cart.model";
import { IdentityUser } from "./user.model";

export class CartService {
    cart: Cart = new Cart('', new IdentityUser('',''), new Array<OrderPartDiscrete>(), new Array<OrderPartWeighted>(), "");
    constructor() { }
    isInteger(num: number) {
        return (num ^ 0) === num;
    }

    checkout() {

    }

    addToCart(product: OrderPart, quantity: number = 1) {
        if (this.isInteger(quantity))
            this.cart.discreteItems.push(product);
        else this.cart.weightedItems.push(product);

    }
}