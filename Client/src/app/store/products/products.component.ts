import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';

import { Repository } from '../../models/repository';
import { Product } from '../../models/product.model';
import { searchType } from '../search/search.service';

@Component({
    selector: 'godsend-products',
    templateUrl: './products.component.html',
    styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {
    // private selectedId: string;
    type = searchType.product;

    products?: Product[];

    constructor(private repo: Repository) {
    }

    ngOnInit() {
        this.repo.getEntities<Product>('product', res => this.products = res);
    }

}
