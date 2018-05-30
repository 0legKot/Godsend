//import { switchMap } from 'rxjs/operators';
import { Component, OnInit, HostBinding } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { Observable } from 'rxjs';

import { Product } from "../../models/product.model";
import { Repository } from "../../models/repository";
import { forEach } from '@angular/router/src/utils/collection';

@Component({
    selector: "productDetail",
    providers: [Repository],
    templateUrl: "productDetail.component.html"
})
export class ProductDetailComponent  {

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private service: Repository){}

    
    gotoProducts(product: Product) {
        let productId = product ? product.id : null;    
        this.router.navigate(['/products', { id: productId}]);
    }
    get product() {
        console.log(this.route.url.last.name);
        console.log(this.route.url.last.name);

        this.service.getProduct(this.route.snapshot.params["id"]);
        
        //this.route.queryParams.subscribe(params => { this.service.getProduct(params["id"]) });
        return this.service.product;
    }
}