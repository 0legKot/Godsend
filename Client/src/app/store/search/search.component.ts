import { Component, OnInit } from '@angular/core';
import { SearchService, searchType, AllSearchResult } from './search.service';
import { Product } from '../../models/product.model';
import { SearchBaseComponent } from './search.base.component';

@Component({
    selector: 'godsend-search',
    templateUrl: './search.component.html',
    styleUrls: [
        '../suppliers/suppliers.component.css',
        '../products/products.component.css'
    ]
})
export class SearchComponent extends SearchBaseComponent implements OnInit {
    searchResult?: AllSearchResult;

    constructor(private ss: SearchService) { super(); }

    ngOnInit() {
        super.ngOnInit();
    }

    doSearch(term: string) {
        this.ss.findByType(searchType.all, term, res => {
            console.dir(res);
            this.searchResult = res;
        });
    }

}
