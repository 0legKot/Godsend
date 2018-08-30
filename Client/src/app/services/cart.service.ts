import { CartView, OrderPartDiscreteView } from '../models/cart.model';
import { RepositoryService } from './repository.service';
import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class CartService {
    cart: CartView = new CartView([]);

    constructor(private repo: RepositoryService) {}

    checkout() {
        // const ord = new Order('', this.cart.customer, this.cart.discreteItems, this.cart.weightedItems, '', 0);
        this.repo.createOrder(this.cart);
    }

    addToCart(part: OrderPartDiscreteView ) {
        // if (isDiscrete(part)) {
            const repeat = this.cart.discreteItems.find(opdv =>
                opdv.productInfo === part.productInfo && opdv.supplierInfo === part.supplierInfo && opdv.price === part.price);
            if (repeat) {
                repeat.quantity += part.quantity;
            } else {
                this.cart.discreteItems.push(part);
            }
        // } else {
        //    this.cart.weightedItems.push(part);
        // }

        console.log('added');
        console.dir(this.cart);
    }

    removeFromCart(part: OrderPartDiscreteView ) {
        // if (isDiscrete(part)) {
            this.cart.discreteItems = this.cart.discreteItems.filter(p => p !== part);
        // } else {
        //    this.cart.weightedItems = this.cart.weightedItems.filter(p => p !== part);
        // }
    }
}
