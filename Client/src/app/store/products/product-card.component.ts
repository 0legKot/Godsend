import { Component, Input } from '@angular/core';

import { ProductInfo } from '../../models/product.model';
import { RepositoryService } from '../../services/repository.service';

@Component({
    selector: 'godsend-product-card[productInfo]',
    templateUrl: './product-card.component.html',
    styleUrls: ['./products.component.css']
})
export class ProductCardComponent {
    @Input()
    productInfo?: ProductInfo;

    constructor(private repo: RepositoryService) { }
    delete() {
        if (this.productInfo)
        this.repo.deleteEntity("product", this.productInfo.id)
    }
}
