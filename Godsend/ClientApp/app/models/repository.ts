import { Product } from "./product.model";
import { Injectable, Inject } from "@angular/core";
import { Http, RequestMethod, Request, Response } from "@angular/http";
import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/map";
import "rxjs/add/operator/catch";

const productsUrl = "api/products";
const suppliersUrl = "/api/suppliers";
const ordersUrl = "/api/orders";

//TODO: rework

@Injectable()
export class Repository {

    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string) {
        this.getProducts();
    }

    getProduct(id: number) {
        this.sendRequest(RequestMethod.Get, productsUrl + "/" + id)
            .subscribe(response => this.product = response);
    }

    getProducts() {
        let url = productsUrl;
        
        this.sendRequest(RequestMethod.Get, url + "/All")
            .subscribe(response => {
                this.products = response.data;
            });
    }

    createProduct(prod: Product) {
        let data = {
            name: prod.name, 
            description: prod.description, price: prod.price
        };

        this.sendRequest(RequestMethod.Post, productsUrl, data)
            .subscribe(response => {
                prod.id = response;
                this.products.push(prod);
            });
    }

    

    replaceProduct(prod: Product) {
        let data = {
            name: prod.name, 
            description: prod.description, price: prod.price

        };
        this.sendRequest(RequestMethod.Put, productsUrl + "/" + prod.id, data)
            .subscribe(response => this.getProducts());
    }

    

    private sendRequest(verb: RequestMethod, url: string, data?: any)
        : Observable<any> {

        return this.http.request(new Request({
            method: verb, url: this.baseUrl+url, body: data
        })).map(response => {
            if (response.headers != null) {
                console.log(response);
                return response.headers.get("Content-Length") != "0"
                    ? response.json() : null;
            }
            else return null;
        });
    }

    updateProduct(id: number, changes: Map<string, any>) {
        var patch:any[]=[];
        changes.forEach((value, key) =>
            patch.push({ op: "replace", path: key, value: value }));

        this.sendRequest(RequestMethod.Patch, productsUrl + "/" + id, patch)
            .subscribe(response => this.getProducts());
    }

    deleteProduct(id: number) {
        this.sendRequest(RequestMethod.Delete, productsUrl + "/" + id)
            .subscribe(response => this.getProducts());
    }



    storeSessionData(dataType: string, data: any) {
        return this.sendRequest(RequestMethod.Post, "/api/session/" + dataType, data)
            .subscribe(response => { });
    }

    getSessionData(dataType: string): Observable<any> {
        return this.sendRequest(RequestMethod.Get, "/api/session/" + dataType);
    }


    product: Product;
    products: Product[];

}