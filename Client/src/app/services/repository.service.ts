import { Product, ProductInfo, Category, CatsWithSubs } from '../models/product.model';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DataService } from './data.service';
import { Order } from '../models/order.model';
import { Supplier, SupplierInfo } from '../models/supplier.model';
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
                this.articles = <any>val;
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

    changeOrderStatus(id: string, status: number, page: number, rpp: number, fn?: ((_: Order[]) => any)) {
        this.data.sendRequest<Order[]>('patch', ordersUrl + '/changeStatus/' + id + '/' + status)
            .subscribe(response => {
                this.getEntities<Order>('order', page, rpp, fn);
            });
    }

    deleteOrder(id: string, page: number, rpp: number, fn?: ((_: Order[]) => any)) {
        this.data.sendRequest<Order[]>('delete', ordersUrl + '/delete/' + id)
            .subscribe(response => {
                this.getEntities<Order>('order', page, rpp, fn);
            });
    }

    getEntities<T>(clas: supportedClass, page: number,rpp:number, fn?: (_: T[]) => any) {
        const url = this.getUrl(clas);

        //const page = 1;
        //const rpp = 15;

        this.data.sendRequest<T[]>('get', url + '/all/' + page + '/' + rpp)
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
        );

        this.data.sendRequest<Order>('post', ordersUrl + '/createOrUpdate', cart)
            .subscribe(response => {
                this.orders.push(response);
            });
    }

    createOrEditEntity<T extends IEntity<IInformation>>(clas: supportedClass, entity: T, page: number, rpp: number, fn?: (_: IInformation) => any) {
        const createEditData = entity.toCreateEdit();
        const url = this.getUrl(clas);

        this.data.sendRequest<string>('post', url + '/CreateOrUpdate', createEditData)
            .subscribe(response => {
                entity.info.id = response;
                this.getEntities(clas, page, rpp);
                if (fn) {
                    fn(entity.info);
                }
            });

    }

    // deprecated?
    replaceProduct(prod: Product, page: number, rpp: number) {
        const data = {
            name: prod.info.name,
            description: prod.info.description
        };
        this.data.sendRequest<null>('put', productsUrl + '/' + prod.id, data)
            .subscribe(response => this.getEntities<Product>('product', page, rpp));
    }

    updateEntity<T>(clas: supportedClass, id: string, changes: Map<string, any>, page: number, rpp: number) {
        const url = this.getUrl(clas);
        const patch: any[] = [];
        changes.forEach((value, key) =>
            patch.push({ op: 'replace', path: key, value: value }));

        this.data.sendRequest<null>('patch', url + '/' + id, patch)
            .subscribe(response => this.getEntities<T>(clas,page,rpp));
    }

    deleteEntity(clas: supportedClass, id: string, page: number, rpp: number, fn?: () => any) {
        const url = this.getUrl(clas);
        this.data.sendRequest<null>('delete', url + '/delete/' + id)
            .subscribe(response => { this.getEntities<null>(clas, page, rpp); if (fn) { fn(); } });
    }


    storeSessionData(dataType: string, data: any) {
        return this.data.sendRequest<null>('post', '/api/session/' + dataType, data)
            .subscribe(response => { });
    }

    getSessionData(dataType: string): Observable<any> {
        return this.data.sendRequest<any>('get', '/api/session/' + dataType);
    }

    getByCategory(cat: Category, fn?: (_: ProductInfo[]) => any): void {
        this.data.sendRequest<ProductInfo[]>('get', 'api/product/getByCategory/' + cat.id)
            .subscribe(products => {
                this.products = products;
                if (fn) fn(products);
            })
    }
}
