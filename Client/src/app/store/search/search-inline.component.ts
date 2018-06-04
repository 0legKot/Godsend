import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { SearchService, searchType, AllSearchResult } from './search.service';
import { SearchBaseComponent } from './search.base.component';


@Component({
    selector: 'godsend-search-inline',
    templateUrl: './search-inline.component.html',
})
export class SearchInlineComponent extends SearchBaseComponent implements OnInit {
    @Input()
    type: number = searchType.all;

    @Output()
    readonly found = new EventEmitter<AllSearchResult>();

    constructor(private ss: SearchService) { super(); }

    ngOnInit() {
        super.ngOnInit();
    }

    doSearch(term: string): void {
        this.ss.findByType(this.type, term, res => this.found.emit(res));
    }
}
