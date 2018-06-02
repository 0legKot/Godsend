import { Component, Input, Output, EventEmitter } from '@angular/core';
import { SearchService, searchType, AllSearchResult } from './search.service';
import { Observable } from 'rxjs';


@Component({
    selector: 'search-inline',
    templateUrl: './search.inline.component.html',
})
export class SearchInlineComponent {

    searchTerm = '';

    @Input()
    type: number = 0;

    @Output()
    onResult = new EventEmitter<AllSearchResult>()

    constructor(private ss: SearchService) { }

    doSearch(): void {
        this.ss.findByType(this.type, this.searchTerm, res => this.onResult.emit(res));
    }
}
