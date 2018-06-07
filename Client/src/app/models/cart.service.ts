import { OrderPart, OrderPartWeighted, OrderPartDiscrete, Order } from "./order.model";
import { Cart } from "./cart.model";
import { IdentityUser } from "./user.model";
import { Repository } from "./repository";
import { Injectable } from "@angular/core";

@Injectable()
export class CartService {
    cart: Cart = new Cart('', new IdentityUser('', ''), new Array<OrderPartDiscrete>(), new Array<OrderPartWeighted>(), "");

    constructor(private repo: Repository) {}

    isInteger(num: number) {
        return (num ^ 0) === num;
    }

    checkout() {
        const ord = new Order('', this.cart.customer, this.cart.discreteItems, this.cart.weightedItems, '', 0);
        this.repo.createOrder(ord);
    }

    addToCart(product: OrderPart, quantity: number = 1) {
        if (this.isInteger(quantity))
            this.cart.discreteItems.push(product);
        else this.cart.weightedItems.push(product);

    }
}