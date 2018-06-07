import { Product, ProductInfo } from '../models/product.model';
import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
// import 'rxjs/add/operator/map';
// import 'rxjs/add/operator/toPromise';
// import 'rxjs/add/operator/catch';
import { map } from 'rxjs/internal/operators/';
import { DataService } from './data.service';
import { Order } from '../models/order.model';
import { Supplier } from '../models/supplier.model';
import { Type } from '@angular/core';
import { ArticleInfo, Article } from '../models/article.model';
import { Cart } from '../models/cart.model';

type supportedClass = 'article' | 'product' | 'supplier' | 'order';

const productsUrl = 'api/product';
const ordersUrl = 'api/order';
const suppliersUrl = 'api/supplier';
const articlesUrl = 'api/article';
// TODO: rework

@Injectable()
export class RepositoryService {
    product: Product | {} = {};
    products: Product[] = [];
    orders: Order[] = [];
    order: Order | {} = {};
    suppliers: Supplier[] = [];
    supplier: Supplier | {} = {};
    articles: ArticleInfo[] = [];
    article: Article | {} = {};

    constructor(private data: DataService) {
    }

    setEntity<T>(val: T) {
        switch (typeof(val)) {
            case typeof (Product):
                this.product = val;
                break;
            case typeof (Supplier):
                this.supplier = val;
                break;
            case typeof (Order):
                this.order = val;
                break;
            default: return;
        }
    }
    setEntites<T>(clas: supportedClass, val: T[]) {
        switch (clas) {
            case 'product':
                this.products = <any>val;
                break;
            case 'order':
                this.orders = <any>val;
                break;
            case 'supplier':
                this.suppliers = <any>val;
                break;
            case 'article':
                this.articles = val;
                break;
        }
    }

    getUrl(clas: supportedClass): string {
        switch (clas) {
            case 'product': return productsUrl;
            case 'order': return ordersUrl;
            case 'supplier': return suppliersUrl;
            case 'article': return articlesUrl;
        }
        return 'urlNotDetected';
    }

    getEntity<T>(id: string, fn: ((_: T) => any), clas: supportedClass) {
        if (id != null) {
            const url = this.getUrl(clas);
            this.data.sendRequest<T>('get', url + '/detail/' + id)
                .subscribe(response => {
                    this.setEntity<T>(response);
                    if (fn) {
                        fn(response);
                    }
                });
        }
    }

    changeOrderStatus(id: string, status: number, fn?: ((_: Order[]) => any)) {
        this.data.sendRequest<Order[]>('patch', ordersUrl + '/changeStatus/' + id + '/' + status)
            .subscribe(response => {
                this.getEntities<Order>('order', fn);
            });
    }

    deleteOrder(id: string, fn?: ((_: Order[]) => any)) {
        this.data.sendRequest<Order[]>('delete', ordersUrl + '/delete/' + id)
            .subscribe(response => {
                this.getEntities<Order>('order', fn);
            });
    }

    getEntities<T>(clas: supportedClass, fn?: (_: T[]) => any) {
        const url = this.getUrl(clas);
        this.data.sendRequest<T[]>('get', url + '/all')
            .subscribe(response => {
                if (fn) {
                    fn(response);
                }
                this.setEntites<T>(clas, response);
            });
    }

    createOrder(cart: Cart) {
        /*const data = {
            customer: 'test',
            discreteItems: ord.discreteItems,
            weightedItems: ord.weightedItems
        };*/

        console.dir(cart);

        this.data.sendRequest<Order>('post', ordersUrl +'/createOrUpdate', cart)
            .subscribe(response => {
                this.orders.push(response);
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
            .subscribe(response => this.getEntities < Product>('product'));
    }

    updateProduct(id: string, changes: Map<string, any>) {
        const patch: any[] = [];
        changes.forEach((value, key) =>
            patch.push({ op: 'replace', path: key, value: value }));

        this.data.sendRequest<null>('patch', productsUrl + '/' + id, patch)
            .subscribe(response => this.getEntities<Product>('product'));
    }

    deleteProduct(id: string) {
        this.data.sendRequest<null>('delete', productsUrl + '/' + id)
            .subscribe(response => this.getEntities<Product>('product'));
    }

    storeSessionData(dataType: string, data: any) {
        return this.data.sendRequest<null>('post', '/api/session/' + dataType, data)
            .subscribe(response => { });
    }

    getSessionData(dataType: string): Observable<any> {
        return this.data.sendRequest<any>('get', '/api/session/' + dataType);
    }



}
