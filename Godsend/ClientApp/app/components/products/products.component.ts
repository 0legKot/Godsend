import { Component } from '@angular/core';

import { Repository } from "../../models/repository";
import { Product } from "../../models/product.model";

@Component({
    selector: 'products',
    templateUrl: './products.component.html'
})
export class ProductsComponent {
    constructor(private repo: Repository) { }

    get products(): Product[] {
        if (this.repo.products != null && this.repo.products.length > 0) {
            return this.repo.products;
        }
        return [];
    }
}
