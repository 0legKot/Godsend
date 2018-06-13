import { Product, ProductInfo } from '../models/product.model';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DataService } from './data.service';
import { Order } from '../models/order.model';
import { Supplier, SupplierInfo, SupplierCreate } from '../models/supplier.model';
import { ArticleInfo, Article } from '../models/article.model';
import { Cart, CartView, OrderPartDiscreteSend } from '../models/cart.model';
import { IEntity, IInformation } from '../models/entity.model';

type supportedClass = 'article' | 'product' | 'supplier' | 'order';

const productsUrl = 'api/product';
const ordersUrl = 'api/order';
const suppliersUrl = 'api/supplier';
const articlesUrl = 'api/article';
// TODO: rework

@Injectable({
    providedIn: 'root'
})
export class RepositoryService {
    product: Product | {} = {};
    products: ProductInfo[] = [];
    orders: Order[] = [];
    order: Order | {} = {};
    suppliers: SupplierInfo[] = [];
    supplier: Supplier | {} = {};
    articles: ArticleInfo[] = [];
    article: Article | {} = {};

    constructor(private data: DataService) {
    }

    getSavedEntities(clas: supportedClass) {
        switch (clas) {
            case 'product':
                return this.products;
            case 'order':
                return this.orders;
            case 'supplier':
                return this.suppliers;
            case 'article':
                return this.articles;
        }
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
                console.log(response);
                this.setEntites<T>(clas, response);
            });
    }

    createOrder(cartView: CartView) {

        console.dir(cartView);

        const cart = new Cart(
            cartView.discreteItems.map(opdv => new OrderPartDiscreteSend(opdv.quantity, opdv.product.id, opdv.supplier.id))
            //cartView.weightedItems.map(opwv => new OrderPartWeightedSend(opwv.weight, opwv.product.id, opwv.supplier.id))
        );

        this.data.sendRequest<Order>('post', ordersUrl + '/createOrUpdate', cart)
            .subscribe(response => {
                this.orders.push(response);
            });
    }

    createOrEditEntity<T extends IEntity<IInformation>>(clas: supportedClass, entity: T, fn?: (_: IInformation) => any) {
        const createEditData = entity.toCreateEdit();
        const url = this.getUrl(clas);

        this.data.sendRequest<string>('post', url + '/CreateOrUpdate', createEditData)
            .subscribe(response => {
                entity.info.id = response;
                if (!entity.id) {
                    this.getSavedEntities(clas).push(entity.info);
                }
                if (fn) {
                    fn(entity.info);
                }
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

    deleteEntity(clas: supportedClass, id: string, fn?: () => any) {
        const url = this.getUrl(clas);
        this.data.sendRequest<null>('delete', url + '/delete/' + id)
            .subscribe(response => { this.getEntities<null>(clas); if (fn) { fn(); } });
    }


    storeSessionData(dataType: string, data: any) {
        return this.data.sendRequest<null>('post', '/api/session/' + dataType, data)
            .subscribe(response => { });
    }

    getSessionData(dataType: string): Observable<any> {
        return this.data.sendRequest<any>('get', '/api/session/' + dataType);
    }
}
