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

    suppliers: SupplierInfo[] = [];

    constructor(private repo: RepositoryService) { }

    ngOnInit() {
        this.repo.getEntities<SupplierInfo>('supplier', s => this.suppliers = s);
    }

    onFound(suppliers: SupplierInfo[]) {
        console.log('found');
        console.dir(suppliers);
    }
}
