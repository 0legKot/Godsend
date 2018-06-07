import { Component } from '@angular/core';
import { CartService } from '../../models/cart.service';
import { OrderPart } from '../../models/order.model';

@Component({
    selector: 'godsend-cart',
    templateUrl: './cart.component.html'
})
export class CartComponent {
    parts: OrderPart[];
    constructor(private cart: CartService) {
        this.parts = this.cart.cart.discreteItems;
    }

    getParts(): OrderPart[] {
        console.log(this.cart.cart.discreteItems);
        return this.parts;
    }
}
