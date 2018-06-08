import { Injectable, Inject } from '@angular/core';
import { HttpClient } from 'selenium-webdriver/http';
import { Product, ProductInfo } from '../../models/product.model';
import { DataService } from '../../services/data.service';
import { map } from 'rxjs/operators';
import { Supplier, SupplierInfo } from '../../models/supplier.model';

export const searchType = {
    all: 0,
    product: 1,
    supplier: 2,
    order: 3
};

@Injectable()
export class SearchService {
    constructor(private data: DataService) { }

    public findProducts(term: string, fn: (_: Product[]) => any) {
        this.data.sendRequest<Product[]>('get', 'api/search/products/' + term)
            .subscribe(products => fn(products));
    }

    public findSuppliers(term: string, fn: (_: Supplier[]) => any) {
        this.data.sendRequest<Supplier[]>('get', 'api/search/suppliers/' + term)
            .subscribe(suppliers => fn(suppliers));
    }

    public findAll(term: string, fn: (_: AllSearchResult) => any) {
        this.data.sendRequest<AllSearchResult>('get', 'api/search/all/' + term)
            .subscribe(result => fn(result));
    }

    public findByType(type: number, term: string, fn: (_: AllSearchResult) => any) {
        this.data.sendRequest<AllSearchResult>('get', 'api/search/type/' + type + '/' + term)
            .subscribe(items => fn(items));
    }
}

export class AllSearchResult {
    productsInfo?: ProductInfo[];
    suppliersInfo?: SupplierInfo[];
}

