import { Component, Input, EventEmitter, Output } from '@angular/core';
import { SupplierInfo } from '../../models/supplier.model';
import { RepositoryService } from '../../services/repository.service';

@Component({
    selector: 'godsend-supplier-card[supplierInfo]',
    templateUrl: './supplier-card.component.html',
    styleUrls: ['./suppliers.component.css']
})
export class SupplierCardComponent {
    @Input()
    supplierInfo?: SupplierInfo;
    @Input()
    image?: string;
    @Output()
    readonly delete = new EventEmitter<void>();

    constructor(private repo: RepositoryService) { }

    get viewed() {
        return this.supplierInfo && (this.repo.viewedSuppliersIds.find(id => id === this.supplierInfo!.id) != undefined);
    }

    onDelete() {
        this.delete.emit();
    }
}
