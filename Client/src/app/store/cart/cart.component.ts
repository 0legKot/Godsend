import { Component } from '@angular/core';
import { Product } from '../../models/product.model';
import { Supplier } from '../../models/supplier.model';
import { Cart } from '../../models/cart.model';
import { OrderPart } from '../../models/order.model';

@Component({
    selector: 'godsend-cart',
    templateUrl: './cart.component.html'
})
export class CartComponent {
    isInteger(num:number) {
    return (num ^ 0) === num;
}
    constructor(private cart: Cart) { }
    addToCart(product: OrderPart, quantity: number = 1) {
        if (this.isInteger(quantity))
            this.cart.discreteItems.push(product);
        else this.cart.weightedItems.push(product); 
    }
}
