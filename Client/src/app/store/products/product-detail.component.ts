// import { switchMap } from 'rxjs/operators';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';

import { Product, ProductWithSuppliers, SupplierAndPrice } from '../../models/product.model';
import { RepositoryService } from '../../services/repository.service';
import { CartService } from '../../services/cart.service';
import { OrderPartDiscreteSend, guidZero } from '../../models/cart.model';

@Component({
    selector: 'godsend-product-detail',
    templateUrl: 'product-detail.component.html',
    styleUrls: ['./product-detail.component.css']
})
export class ProductDetailComponent implements OnInit {
    data?: ProductWithSuppliers;

    selectedSupplier?: SupplierAndPrice;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private service: RepositoryService,
        private cart: CartService) { }

    gotoProducts(product: Product) {
        const productId = product ? product.id : null;
        this.router.navigate(['/products', { id: productId}]);
    }
    buy() {
        const op: OrderPartDiscreteSend = {
            quantity: 1,
            productId: this.data && this.data.product ? this.data.product.id  : guidZero,
            supplierId: this.selectedSupplier && this.selectedSupplier.supplier ? this.selectedSupplier.supplier.id : guidZero 
        }
        this.cart.addToCart(op);
    }
    ngOnInit() {
        this.service.getEntity<ProductWithSuppliers>(this.route.snapshot.params.id, p => {
            this.data = p;
            this.selectedSupplier = p.suppliers[0];
        }, 'product');
    }

    /*get product(): Product | {} {
        return this.service.product;
    }*/
}
