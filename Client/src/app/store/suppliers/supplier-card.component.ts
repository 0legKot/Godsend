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

    @Output()
    readonly delete = new EventEmitter<void>();

    constructor(private repo: RepositoryService) { }

    onDelete() {
        this.delete.emit();
    }
}
