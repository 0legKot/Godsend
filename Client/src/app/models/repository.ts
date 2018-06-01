import { Product } from "./product.model";
import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http"
import { Observable } from "rxjs";
//import "rxjs/add/operator/map";
//import 'rxjs/add/operator/toPromise';
//import "rxjs/add/operator/catch";
import { map } from "rxjs/internal/operators/";

const productsUrl = "api/product";
const suppliersUrl = "/api/supplier";
const ordersUrl = "/api/order";

//TODO: rework

@Injectable()
export class Repository {

    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
        this.getProducts();
    }

    getProduct(id: string, fn: ((_: Product) => any)) {
        if (id != null) {
            this.sendRequest<Product>('get', productsUrl + "/detail/" + id)
                .subscribe(response => {
                    this.product = response;
                    console.log("getproductsent");
                    console.log(response);
                    if (fn) fn(response)
                });
        }
    }

    getProducts() {
        let url = productsUrl;

        this.sendRequest<Product[]>('get', url + '/all')
            .subscribe(response => {
            this.products = response;
        });
        
    }

    createProduct(prod: Product) {
        let data = {
            name: prod.info.name, 
            description: prod.info.description
        };

        this.sendRequest<string>('post', productsUrl, data)
            .subscribe(response => {
                prod.id = response;
                this.products.push(prod);
            });
    }    

    replaceProduct(prod: Product) {
        let data = {
            name: prod.info.name, 
            description: prod.info.description

        };
        this.sendRequest<null>('put', productsUrl + "/" + prod.id, data)
            .subscribe(response => this.getProducts());
    }  

    private sendRequest<T>(method: string, url: string, data?: any)
        : Observable<T> {

        return this.http.request<T>(method, this.baseUrl + url, { body: data, responseType: "json" }).pipe(
            map(response => {
                console.log(this.baseUrl + url);
                console.log(response);
                return response;
            }));
    }
    
    updateProduct(id: string, changes: Map<string, any>) {
        var patch:any[]=[];
        changes.forEach((value, key) =>
            patch.push({ op: "replace", path: key, value: value }));

        this.sendRequest<null>('patch', productsUrl + "/" + id, patch)
            .subscribe(response => this.getProducts());
    }

    deleteProduct(id: string) {
        this.sendRequest<null>('delete', productsUrl + "/" + id)
            .subscribe(response => this.getProducts());
    }

    storeSessionData(dataType: string, data: any) {
        return this.sendRequest<null>('post', "/api/session/" + dataType, data)
            .subscribe(response => { });
    }

    getSessionData(dataType: string): Observable<any> {
        return this.sendRequest<any>('get', "/api/session/" + dataType);
    }

    product: Product | {} = { };
    products: Product[] = [];
}
