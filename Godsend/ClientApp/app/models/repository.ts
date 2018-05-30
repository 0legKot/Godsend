import { Product } from "./product.model";
import { Injectable, Inject } from "@angular/core";
import { Http, RequestMethod, Request, Response } from "@angular/http";
import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/map";
import "rxjs/add/operator/catch";


const productsUrl = "api/product";
const suppliersUrl = "/api/supplier";
const ordersUrl = "/api/order";

//TODO: rework

@Injectable()
export class Repository {

    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string) {
        this.getProducts();
    }

    getProduct(id: string|null) {
        if (id!=null)
        this.sendRequest(RequestMethod.Get, productsUrl + "/all/one?id=" + id)
            .subscribe(response => this.product = response);
    }

    getProducts() {
        let url = productsUrl;
        
        this.sendRequest(RequestMethod.Get, url + "/all")
            .subscribe(response => {
                this.products = response;
            });
    }

    createProduct(prod: Product) {
        let data = {
            name: prod.info.name, 
            description: prod.info.description
        };

        this.sendRequest(RequestMethod.Post, productsUrl, data)
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
        this.sendRequest(RequestMethod.Put, productsUrl + "/" + prod.id, data)
            .subscribe(response => this.getProducts());
    }

    

    private sendRequest(verb: RequestMethod, url: string, data?: any)
        : Observable<any> {

        return this.http.request(new Request({
            method: verb, url: this.baseUrl+url, body: data
        })).map(response => {
            if (response.headers != null) {
                console.log(this.baseUrl + url);
                console.log(response);
                return response.headers.get("Content-Length") != "0"
                    ? response.json() : null;
            }
            else return null;
        });
    }

    updateProduct(id: string, changes: Map<string, any>) {
        var patch:any[]=[];
        changes.forEach((value, key) =>
            patch.push({ op: "replace", path: key, value: value }));

        this.sendRequest(RequestMethod.Patch, productsUrl + "/" + id, patch)
            .subscribe(response => this.getProducts());
    }

    deleteProduct(id: string) {
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


    product?: Product;
    products: Product[] = [];

}