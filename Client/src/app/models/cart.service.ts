import { OrderPart, OrderPartWeighted, OrderPartDiscrete, Order } from "./order.model";
import { Cart, OrderPartWeightedSend, OrderPartDiscreteSend, OrderPartSend } from "./cart.model";
import { IdentityUser } from "./user.model";
import { Repository } from "./repository";
import { Injectable } from "@angular/core";

@Injectable()
export class CartService {
    cart: Cart = new Cart(new Array<OrderPartDiscreteSend>(), new Array<OrderPartWeightedSend>());

    constructor(private repo: Repository) {}

    isInteger(num: number) {
        return (num ^ 0) === num;
    }

    checkout() {
        //const ord = new Order('', this.cart.customer, this.cart.discreteItems, this.cart.weightedItems, '', 0);
        this.repo.createOrder(this.cart);
    }

    addToCart(part: OrderPartSend) {
        if ((<any>part).quantity != null) this.cart.discreteItems.push(part);
        else this.cart.weightedItems.push(part);

    }
}