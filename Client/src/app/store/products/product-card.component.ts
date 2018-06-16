import { Component, Input, Output, EventEmitter } from '@angular/core';

import { ProductInfo } from '../../models/product.model';

@Component({
    selector: 'godsend-product-card[productInfo]',
    templateUrl: './product-card.component.html',
    styleUrls: ['./products.component.css']
})
export class ProductCardComponent {
    @Input()
    productInfo?: ProductInfo;
    @Input()
    image?: string;
    @Output()
    readonly delete = new EventEmitter<void>();

    constructor() { }

    onDelete() {
        this.delete.emit();
    }
}
