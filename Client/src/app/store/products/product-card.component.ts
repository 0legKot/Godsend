import { Component, Input, Output, EventEmitter } from '@angular/core';

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
    @Input()
    image?: string;
    @Output()
    readonly delete = new EventEmitter<void>();

    constructor(private repo: RepositoryService) { }

    get viewed() {
        return this.productInfo && (this.repo.viewedProductsIds.find(id => id === this.productInfo!.id) !== undefined);
    }

    onDelete() {
        this.delete.emit();
    }
}
