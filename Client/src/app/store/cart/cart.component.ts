import { Component } from '@angular/core';
import { CartService } from '../../services/cart.service';
import { OrderPart } from '../../models/order.model';
import { OrderPartSend, OrderPartWeightedSend, OrderPartDiscreteSend, OrderPartDiscreteView, OrderPartWeightedView } from '../../models/cart.model';
import { retry } from 'rxjs/operators';

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

    get discreteParts(): OrderPartDiscreteView[] {
        console.log('getting discrete');
        console.dir(this.cartService.cart.discreteItems);
        return this.cartService.cart.discreteItems;
    }

    get weightedParts(): OrderPartWeightedView[] {
        return this.cartService.cart.weightedItems;
    }

    delete(part: OrderPartDiscreteView | OrderPartWeightedView) {
        this.cartService.removeFromCart(part)
    }

    increment(part: OrderPartDiscreteView) {
        ++part.quantity; //max?
    }

    decrement(part: OrderPartDiscreteView) {
        if (part.quantity <= 0) return;
        --part.quantity;
    }
}
