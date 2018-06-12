import { Component, Input } from '@angular/core';
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

    constructor(private repo: RepositoryService) { }

    delete() {
        if (this.supplierInfo)
            this.repo.deleteEntity('supplier', this.supplierInfo.id)
    }
}
