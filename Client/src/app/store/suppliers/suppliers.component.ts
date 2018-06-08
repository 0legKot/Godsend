import { Component } from '@angular/core';
import { RepositoryService } from '../../services/repository.service';
import { Supplier, SupplierInfo } from '../../models/supplier.model';
import { OnInit } from '@angular/core';
import { searchType } from '../search/search.service';

@Component({
    selector: 'godsend-suppliers',
    templateUrl: './suppliers.component.html',
    styleUrls: ['./suppliers.component.css']
})
export class SuppliersComponent implements OnInit {
    type = searchType.supplier;

    searchSuppliers?: SupplierInfo[];
    templateText = 'Waiting for data...';

    get suppliers() {
        return this.searchSuppliers || this.repo.suppliers;
    }

    constructor(private repo: RepositoryService) { }

    ngOnInit() {
        this.repo.getEntities<SupplierInfo>('supplier');
    }

    onFound(suppliers: SupplierInfo[]) {
        this.templateText = 'Not found';
        this.searchSuppliers = suppliers;
    }
}
