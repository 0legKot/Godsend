import { Component } from '@angular/core';
import { CartService } from '../../services/cart.service';
import { OrderPart } from '../../models/order.model';
import { OrderPartSend, OrderPartWeightedSend, OrderPartDiscreteSend } from '../../models/cart.model';

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

    delete(part: OrderPartDiscreteSend | OrderPartWeightedSend) {
        this.cartService.removeFromCart(part)
    }
}
