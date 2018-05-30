import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';

import { Repository } from "../../models/repository";
import { Product } from "../../models/product.model";

@Component({
    selector: 'products',
    providers: [Repository],
    templateUrl: './products.component.html'
})
export class ProductsComponent {
    //private selectedId: string;

    constructor(private repo: Repository, private route: ActivatedRoute) {
    }
    choose(id: string) {
        console.log("choose");
        console.log(id);
        this.repo.getProduct("bd52b3c9-bbe7-4f2d-dde9-08d5c270269a");
        console.log((<Product>this.repo.productget).id);
    }
    get products(): Product[] {
        if (this.repo.products != null && this.repo.products.length > 0) {
            return this.repo.products;
        }
        return [];
    }




}
