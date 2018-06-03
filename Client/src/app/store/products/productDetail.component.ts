// import { switchMap } from 'rxjs/operators';
import { Component, OnInit, HostBinding } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { Observable } from 'rxjs';

import { Product } from '../../models/product.model';
import { Repository } from '../../models/repository';
import { forEach } from '@angular/router/src/utils/collection';

@Component({
    selector: 'productDetail',
    templateUrl: 'productDetail.component.html'
})
export class ProductDetailComponent implements OnInit {
    prod?: Product;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private service: Repository) { }

    gotoProducts(product: Product) {
        const productId = product ? product.id : null;
        this.router.navigate(['/products', { id: productId}]);
    }

    ngOnInit() {
        this.service.getEntity<Product>(this.route.snapshot.params['id'], p => this.prod = p, "product");
    }

    /*get product(): Product | {} {
        return this.service.product;
    }*/
}
