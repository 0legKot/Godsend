import { Component, ViewChild } from '@angular/core';
import { RepositoryService } from '../../services/repository.service';
import { Supplier, SupplierInfo, Location } from '../../models/supplier.model';
import { OnInit } from '@angular/core';
import { searchType } from '../search/search.service';
import { SearchInlineComponent } from '../search/search-inline.component';
import { Router } from '@angular/router';

@Component({
    selector: 'godsend-suppliers',
    templateUrl: './suppliers.component.html',
    styleUrls: [
        './suppliers.component.css',
        '../products/products.component.css'
    ]
})
export class SuppliersComponent implements OnInit {
    type = searchType.supplier;
    page = 1;
    rpp = 10;
    public searchSuppliers?: SupplierInfo[];
    templateText = 'Waiting for data...';

    @ViewChild(SearchInlineComponent)
    searchInline!: SearchInlineComponent;

    get suppliers() {
        return this.searchSuppliers || this.repo.suppliers;
    }

    get pagesCount(): number {
        return Math.ceil(this.repo.suppliersCount / this.rpp);
    }

    onPageChanged(page: number) {
        this.page = page;
        this.getSuppliers();
    }

    getSuppliers() {
        this.repo.getEntities<SupplierInfo>('supplier', this.page, this.rpp);
    }

    constructor(private repo: RepositoryService, private router: Router) { }

    ngOnInit() {
        this.getSuppliers();
    }

    createSupplier(name: string, address: string) {
        // TODO create interface with only relevant info
        const sup = new Supplier(new SupplierInfo(name, new Location(address)), []);
        // if (this.searchInline != undefined)
        this.repo.createOrEditEntity('supplier', sup, this.page, this.rpp, info => this.router.navigateByUrl('suppliers/' + info.id));
    }

    deleteSupplier(id: string) {
        // if (this.searchInline)
        this.repo.deleteEntity('supplier', id, this.page, this.rpp, () => this.searchInline.doSearch());
    }

    onFound(suppliers: SupplierInfo[]) {
        this.templateText = 'Not found';
        this.searchSuppliers = suppliers;
    }

}
