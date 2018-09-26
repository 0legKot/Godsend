import { Component, Input, Output, EventEmitter } from '@angular/core';

import { ProductInfo } from '../../models/product.model';
import { RepositoryService } from '../../services/repository.service';
import { ImageService } from '../../services/image.service';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
    selector: 'godsend-product-card[productInfo]',
    templateUrl: './product-card.component.html',
    styleUrls: ['./products.component.css']
})
export class ProductCardComponent {
    @Input()
    productInfo?: ProductInfo;

    @Output()
    readonly delete = new EventEmitter<void>();

    get canDelete(): boolean {
        return Boolean(this.auth.roles.find(x => x == 'Administrator' || x == 'Moderator'));
    }

    constructor(private repo: RepositoryService, private imageService: ImageService, private auth: AuthenticationService) { }

    get viewed() {
        return this.productInfo && (this.repo.viewedProductsIds.find(id => id === this.productInfo!.id) !== undefined);
    }

    get imagePath(): string {
        if (this.productInfo && this.productInfo.preview) {
            return this.imageService.getImagePath(this.productInfo.preview.id);
        } else {
            return '';
        }
    }

    onDelete() {
        this.delete.emit();
    }
}
