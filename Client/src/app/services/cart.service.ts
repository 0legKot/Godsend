import { Cart, OrderPartWeightedSend, OrderPartDiscreteSend, isDiscrete } from "../models/cart.model";
import { IdentityUser } from "../models/user.model";
import { RepositoryService } from "./repository.service";
import { Injectable } from "@angular/core";

@Injectable()
export class CartService {
    cart: Cart = new Cart(new Array<OrderPartDiscreteSend>(), new Array<OrderPartWeightedSend>());

    constructor(private repo: RepositoryService) {}

    checkout() {
        //const ord = new Order('', this.cart.customer, this.cart.discreteItems, this.cart.weightedItems, '', 0);
        this.repo.createOrder(this.cart);
    }

    addToCart(part: OrderPartDiscreteSend | OrderPartWeightedSend) {
        if (isDiscrete(part)) this.cart.discreteItems.push(part);
        else this.cart.weightedItems.push(part);
    }    
}