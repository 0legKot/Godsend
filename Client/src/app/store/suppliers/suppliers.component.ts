import { Component, ViewChild } from '@angular/core';
import { RepositoryService } from '../../services/repository.service';
import { Supplier, SupplierInfo, Location } from '../../models/supplier.model';
import { OnInit } from '@angular/core';
import { searchType } from '../search/search.service';
import { SearchInlineComponent } from '../search/search-inline.component';
import { ImageService } from '../../services/image.service';

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
    images: { [id: string]: string; } = {};
    searchSuppliers?: SupplierInfo[];
    templateText = 'Waiting for data...';
    imagg: any = {};

    @ViewChild(SearchInlineComponent)
    searchInline!: SearchInlineComponent;

    get suppliers() {
        return this.searchSuppliers || this.repo.suppliers;
    }
    getImage(pi: SupplierInfo): string {
        return this.images[pi.id];
    }
    constructor(private repo: RepositoryService, private imageService: ImageService) { }

    ngOnInit() {
        this.repo.getEntities<SupplierInfo>('supplier', res=>{
            this.imageService.getPreviewImages(res.map(si => si.id), (smth: any) => this.imagg = smth);
            // for(let p of res) {
            //    this.imageService.getImage(p.id, image => { this.images[p.id] = image; });
            // }
        });
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
