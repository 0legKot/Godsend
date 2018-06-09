import { Component, Input } from '@angular/core';
import { SupplierInfo } from '../../models/supplier.model';

@Component({
    selector: 'godsend-supplier-card[supplierInfo]',
    templateUrl: './supplier-card.component.html',
    styleUrls: ['./suppliers.component.css']
})
export class SupplierCardComponent {
    @Input()
    supplierInfo?: SupplierInfo;
}
