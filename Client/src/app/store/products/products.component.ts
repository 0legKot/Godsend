import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';

import { RepositoryService } from '../../services/repository.service';
import { Product, ProductInfo } from '../../models/product.model';
import { searchType } from '../search/search.service';
import { SearchInlineComponent } from '../search/search-inline.component';

@Component({
    selector: 'godsend-products',
    templateUrl: './products.component.html',
    styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {
    // private selectedId: string;
    type = searchType.product;

    searchProducts?: ProductInfo[];
    templateText = 'Waiting for data...';

    @ViewChild(SearchInlineComponent)
    searchInline!: SearchInlineComponent;

    get products(): ProductInfo[] | {} {
        return this.searchProducts || this.repo.products;
    }

    createProduct(descr: string, name: string) {
        // TODO create interface with oly relevant info
        const prod = new Product('', new ProductInfo('', descr, name, 0, 0));
        this.repo.createProduct(prod, () => this.searchInline.doSearch());
    }

    deleteProduct(id: string) {
        this.repo.deleteEntity('product', id, () => this.searchInline.doSearch());
    }

    onFound(products: ProductInfo[]) {
        this.templateText = 'Not found';
        this.searchProducts = products;
    }

    constructor(private repo: RepositoryService) {
    }

    ngOnInit() {
        this.repo.getEntities<ProductInfo>('product'/*, res => this.products = res*/);
    }

}
