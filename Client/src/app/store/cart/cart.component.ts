import { Component } from '@angular/core';
import { CartService } from '../../models/cart.service';
import { OrderPart } from '../../models/order.model';
import { OrderPartSend } from '../../models/cart.model';

@Component({
    selector: 'godsend-cart',
    templateUrl: './cart.component.html'
})
export class CartComponent {
   // parts: OrderPartSend[];
    constructor(private cartService: CartService) {

    }

    checkout() {
        this.cartService.checkout();
    }

    get parts(): OrderPartSend[] {
        return this.cartService.cart.discreteItems;
    }
}
