import { Component, Input, EventEmitter, Output } from '@angular/core';
import { SupplierInfo } from '../../models/supplier.model';
import { RepositoryService } from '../../services/repository.service';
import { ImageService } from '../../services/image.service';

@Component({
    selector: 'godsend-supplier-card[supplierInfo]',
    templateUrl: './supplier-card.component.html',
    styleUrls: ['./suppliers.component.css']
})
export class SupplierCardComponent {
    @Input()
    supplierInfo?: SupplierInfo;

    @Output()
    readonly delete = new EventEmitter<void>();

    constructor(private repo: RepositoryService, private imageService: ImageService) { }

    get viewed() {
        return this.supplierInfo && (this.repo.viewedSuppliersIds.find(id => id === this.supplierInfo!.id) !== undefined);
    }

    get imagePath(): string {
        if (this.supplierInfo && this.supplierInfo.preview) {
            return this.imageService.getImagePath(this.supplierInfo.preview.id);
        } else {
            return '';
        }
    }

    onDelete() {
        this.delete.emit();
    }
}
