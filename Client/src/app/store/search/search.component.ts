import { Component } from '@angular/core';
import { SearchService } from './search.service';
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

        this.ss.findProducts(this.searchTerm, p => this.searchResult = p);
    }

}
