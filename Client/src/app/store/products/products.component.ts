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

    get products(): Product[] | {} {
        return this.repo.products;
    };

    createProduct(descr: string, name: string) {
        var prod = new Product('', new ProductInfo('', descr, name,0,0))
        this.repo.createProduct(prod);
    }

    constructor(private repo: RepositoryService) {
    }

    ngOnInit() {
        this.repo.getEntities<Product>('product'/*, res => this.products = res*/);
    }

}
