import { Product } from './product.model';
import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
// import 'rxjs/add/operator/map';
// import 'rxjs/add/operator/toPromise';
// import 'rxjs/add/operator/catch';
import { map } from 'rxjs/internal/operators/';
import { DataService } from './data.service';

const productsUrl = 'api/product';
const suppliersUrl = '/api/supplier';
const ordersUrl = '/api/order';

// TODO: rework

@Injectable()
export class Repository {
    product: Product | {} = {};
    products: Product[] = [];

    constructor(private data: DataService) {
        this.getProducts();
    }

    getProduct(id: string, fn: ((_: Product) => any)) {
        if (id != null) {
            this.data.sendRequest<Product>('get', productsUrl + '/detail/' + id)
                .subscribe(response => {
                    this.product = response;
                    console.log('getproductsent');
                    console.log(response);
                    if (fn) {
                        fn(response);
                    }
                });
        }
    }

    getProducts() {
        const url = productsUrl;

        this.data.sendRequest<Product[]>('get', url + '/all')
            .subscribe(response => {
            this.products = response;
        });
    }

    createProduct(prod: Product) {
        const data = {
            name: prod.info.name,
            description: prod.info.description
        };

        this.data.sendRequest<string>('post', productsUrl, data)
            .subscribe(response => {
                prod.id = response;
                this.products.push(prod);
            });
    }

    replaceProduct(prod: Product) {
        const data = {
            name: prod.info.name,
            description: prod.info.description
        };
        this.data.sendRequest<null>('put', productsUrl + '/' + prod.id, data)
            .subscribe(response => this.getProducts());
    }

    

    updateProduct(id: string, changes: Map<string, any>) {
        const patch: any[] = [];
        changes.forEach((value, key) =>
            patch.push({ op: 'replace', path: key, value: value }));

        this.data.sendRequest<null>('patch', productsUrl + '/' + id, patch)
            .subscribe(response => this.getProducts());
    }

    deleteProduct(id: string) {
        this.data.sendRequest<null>('delete', productsUrl + '/' + id)
            .subscribe(response => this.getProducts());
    }

    storeSessionData(dataType: string, data: any) {
        return this.data.sendRequest<null>('post', '/api/session/' + dataType, data)
            .subscribe(response => { });
    }

    getSessionData(dataType: string): Observable<any> {
        return this.data.sendRequest<any>('get', '/api/session/' + dataType);
    }

}
