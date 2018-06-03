import { Product, ProductInfo } from './product.model';
import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
// import 'rxjs/add/operator/map';
// import 'rxjs/add/operator/toPromise';
// import 'rxjs/add/operator/catch';
import { map } from 'rxjs/internal/operators/';
import { DataService } from './data.service';
import { Order } from './order.model';
import { Supplier } from './supplier.model';
import { Type } from '@angular/core';
import { Entity } from './entity';

const productsUrl = 'api/product';
const ordersUrl = 'api/order';
const suppliersUrl = 'api/supplier';
// TODO: rework

@Injectable()
export class Repository {
    product: Product | {} = {};
    products: Product[] = [];
    orders: Order[] = [];
    order: Order | {} = {};
    suppliers: Supplier[] = [];
    supplier: Supplier | {} = {};

    

    constructor(private data: DataService) {
    }

    //getProduct(id: string, fn: ((_: Product) => any)) {
    //    if (id != null) {
    //        this.data.sendRequest<Product>('get', productsUrl + '/detail/' + id)
    //            .subscribe(response => {
    //                this.product = response;
    //                console.log(response);
    //                if (fn) {
    //                    fn(response);
    //                }
    //            });
    //    }
    //}

    setEntity<T>(val: T) {
        switch (typeof(val)) {
            case typeof (Product): this.product = val;
            case typeof (Supplier): this.supplier = val;
            case typeof (Order): this.order = val;
            default: return;
        }
    }
    setEntites<T>(clas: string, val: T[]) {
        switch (clas) {
            //case "product": this.products = val;
            //case "order": this.suppliers = val;
            case "supplier": this.orders = val;
            default: return;
        }
    }
    getUrl(clas: string): string {
        switch (clas.toLowerCase()) {
            case "product": return productsUrl; 
            case "order": return ordersUrl;
            case "supplier": return suppliersUrl; 
        }
        return "";
    }
    getEntity<T>(id: string, fn: ((_: T) => any),clas:string) {
        if (id != null) {
            let url = this.getUrl(clas);
            this.data.sendRequest<T>('get', url + '/detail/' + id)
                .subscribe(response => {
                    this.setEntity<T>(response);
                    console.log(response);
                    if (fn) {
                        fn(response);
                    }
                });
        }
    }
    changeStatus(id: string, status: number) {
        this.data.sendRequest<Product[]>('patch', ordersUrl + '/changeStatus/'+id+'/'+status)
            .subscribe(response => this.getOrders());
    }
    deleteOrder(id: string) {
        this.data.sendRequest<null>('delete', ordersUrl + '/delete/' + id)
            .subscribe(response => this.getOrders());
    }
    getEntities<T>(clas: string, fn?: (_: T[]) => any) {
        let url = this.getUrl(clas);
        this.data.sendRequest<T[]>('get', url + '/all')
            .subscribe(response => {
                if (fn) {
                    console.log(response);
                    fn(response);
                }
                this.setEntites<T>(clas,response);
            });
    }
    getProducts(fn?: (_:Product[]) => any) {
        this.data.sendRequest<Product[]>('get', productsUrl + '/all')
            .subscribe(response => {
                if (fn) {
                    console.log(response);
                    fn(response);
                }
                this.products = response;
        });
    }

    getOrders(fn?: (_: Order[]) => any) {
        this.data.sendRequest<Order[]>('get', ordersUrl + '/all')
            .subscribe(orders => {
                if (fn) {
                    fn(orders);
                }
                this.orders = orders;
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
