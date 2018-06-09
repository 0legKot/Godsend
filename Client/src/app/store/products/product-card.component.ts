import { Component, Input } from '@angular/core';

import { ProductInfo } from '../../models/product.model';

@Component({
    selector: 'godsend-product-card[productInfo]',
    templateUrl: './product-card.component.html',
    styleUrls: ['./products.component.css']
})
export class ProductCardComponent {
    @Input()
    productInfo?: ProductInfo;
}
