import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "selenium-webdriver/http";
import { Product } from "../../models/product.model";
import { DataService } from "../../models/data.service";
import { map } from "rxjs/operators";

@Injectable()
export class SearchService {
    constructor(private data: DataService) { }

    public findProducts(name: string, fn: (_: Product[]) => any) {
        this.data.sendRequest<Product[]>('get', 'api/product/find/' + name)
            .subscribe(products => fn(products));
    }
}