import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';

import { RepositoryService } from '../../services/repository.service';
import { Product, ProductInfo } from '../../models/product.model';
import { searchType } from '../search/search.service';

@Component({
    selector: 'godsend-products',
    templateUrl: './products.component.html',
    styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {
    // private selectedId: string;
    type = searchType.product;

    searchProducts?: ProductInfo[];

    get products(): ProductInfo[] | {} {
        return this.searchProducts || this.repo.products;
    };

    createProduct(descr: string, name: string) {
        var prod = new Product('', new ProductInfo('', descr, name,0,0))
        this.repo.createProduct(prod);
    }

    onFound(products: ProductInfo[]) {
        this.searchProducts = products;
        // do something with search
    }

    constructor(private repo: RepositoryService) {
    }

    ngOnInit() {
        this.repo.getEntities<ProductInfo>('product'/*, res => this.products = res*/);
    }

}
