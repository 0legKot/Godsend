// import { switchMap } from 'rxjs/operators';
import { Component, OnInit, HostBinding } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { Observable } from 'rxjs';

import { Product } from '../../models/product.model';
import { Repository } from '../../models/repository';
import { forEach } from '@angular/router/src/utils/collection';
import { CartService } from '../../models/cart.service';
import { OrderPartDiscrete } from '../../models/order.model';

@Component({
    selector: 'godsend-product-detail',
    templateUrl: 'product-detail.component.html',
    styleUrls: ['./product-detail.component.css']
})
export class ProductDetailComponent implements OnInit {
    prod?: Product;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private service: Repository,
        private cart: CartService) { }

    gotoProducts(product: Product) {
        const productId = product ? product.id : null;
        this.router.navigate(['/products', { id: productId}]);
    }
    buy() {
        const op: OrderPartDiscrete = {
            quantity:1,
            id:'',
            product: this.prod,
            supplier: ''
        }
        this.cart.addToCart(op, 1);
    }
    ngOnInit() {
        this.service.getEntity<Product>(this.route.snapshot.params.id, p => this.prod = p, 'product');
    }

    /*get product(): Product | {} {
        return this.service.product;
    }*/
}
