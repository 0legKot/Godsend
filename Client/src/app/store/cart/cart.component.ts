import { Component } from '@angular/core';
import { CartService } from '../../services/cart.service';
import { OrderPart } from '../../models/order.model';
import { OrderPartDiscreteView } from '../../models/cart.model';
import { retry } from 'rxjs/operators';

@Component({
    selector: 'godsend-cart',
    templateUrl: './cart.component.html'
})
export class CartComponent {
   // parts: OrderPartSend[];
    constructor(private cartService: CartService) {

    }

    get totalPrice(): string {
        return (this.discreteParts.reduce((prev, cur) => prev + cur.price * cur.quantity, 0))
            .toFixed(2);
    }

    checkout() {
        this.cartService.checkout();
    }

    get discreteParts(): OrderPartDiscreteView[] {
        console.log('getting discrete');
        console.dir(this.cartService.cart.discreteItems);
        return this.cartService.cart.discreteItems;
    }

    // get weightedParts(): OrderPartWeightedView[] {
    //    return this.cartService.cart.weightedItems;
    // }

    delete(part: OrderPartDiscreteView ) {
        this.cartService.removeFromCart(part);
    }

    increment(part: OrderPartDiscreteView) {
        ++part.quantity;
    }

    decrement(part: OrderPartDiscreteView) {
        if (part.quantity <= 0) {
            this.delete(part);
        } else {
            --part.quantity;
        }
    }
}
