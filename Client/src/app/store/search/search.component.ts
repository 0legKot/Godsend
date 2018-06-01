import { Component } from '@angular/core';
import { SearchService, searchType } from './search.service';
import { Product } from '../../models/product.model';

@Component({
    selector: 'search',
    templateUrl: './search.component.html'
})
export class SearchComponent {
    searchTerm: string = '';
    searchResult?: Product[];

    constructor(private ss: SearchService) { }

    doSearch() {
        if (this.searchTerm === '') return;

        this.ss.findByType(searchType.product, this.searchTerm, p => { console.dir(p); this.searchResult = p.products });
    }

}
