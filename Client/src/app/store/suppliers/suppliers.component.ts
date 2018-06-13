import { Component, ViewChild } from '@angular/core';
import { RepositoryService } from '../../services/repository.service';
import { Supplier, SupplierInfo, Location } from '../../models/supplier.model';
import { OnInit } from '@angular/core';
import { searchType } from '../search/search.service';
import { SearchInlineComponent } from '../search/search-inline.component';

@Component({
    selector: 'godsend-suppliers',
    templateUrl: './suppliers.component.html',
    styleUrls: ['./suppliers.component.css']
})
export class SuppliersComponent implements OnInit {
    type = searchType.supplier;

    searchSuppliers?: SupplierInfo[];
    templateText = 'Waiting for data...';

    @ViewChild(SearchInlineComponent)
    searchInline!: SearchInlineComponent;

    get suppliers() {
        return this.searchSuppliers || this.repo.suppliers;
    }

    constructor(private repo: RepositoryService) { }

    ngOnInit() {
        this.repo.getEntities<SupplierInfo>('supplier');
    }

    createSupplier(name: string, address: string) {
        // TODO create interface with oly relevant info
        const sup = new Supplier(new SupplierInfo(name, new Location(address)));
        this.repo.createOrEditEntity('supplier', sup, () => this.searchInline.doSearch());
    }

    deleteSupplier(id: string) {
        this.repo.deleteEntity('supplier', id, () => this.searchInline.doSearch());
    }

    onFound(suppliers: SupplierInfo[]) {
        this.templateText = 'Not found';
        this.searchSuppliers = suppliers;
    }
}
