import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';

import { Repository } from '../../models/repository';
import { Product } from '../../models/product.model';
import { searchType } from '../search/search.service';

@Component({
    selector: 'products',
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
        this.repo.getProducts(res => this.products = res);
    }

    choose(id: string): void {

    }


    /*get products(): Product[] {
        if (this.repo.products != null && this.repo.products.length > 0) {
            return this.repo.products;
        }
        return [];
    }*/




}
