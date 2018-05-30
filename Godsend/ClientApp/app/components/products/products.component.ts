import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';

import { Repository } from "../../models/repository";
import { Product } from "../../models/product.model";

@Component({
    selector: 'products',
    templateUrl: './products.component.html'
})
export class ProductsComponent {
    //private selectedId: string;

    constructor(private repo: Repository) {
    }
    async choose(id: string) {
        /*console.log("choose");
        console.log(id);
        console.log(await this.repo.getProduct("adc1067c-fce7-4bc9-bc51-08d5c2c9ba6a"));
        console.log(this.repo.productget.id);*/
    }
    get products(): Product[] {
        if (this.repo.products != null && this.repo.products.length > 0) {
            return this.repo.products;
        }
        return [];
    }




}
