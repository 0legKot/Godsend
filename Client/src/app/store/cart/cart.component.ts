import { Component } from '@angular/core';
import { CartService } from '../../models/cart.service';
import { OrderPart } from '../../models/order.model';

@Component({
    selector: 'godsend-cart',
    templateUrl: './cart.component.html'
})
export class CartComponent {
    constructor(private cart: CartService) { }
    parts: OrderPart[];
    getParts(): OrderPart[] {
        return this.parts;
    }
}
