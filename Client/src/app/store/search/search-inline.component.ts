import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { SearchService, searchType, AllSearchResult } from './search.service';
import { SearchBaseComponent } from './search.base.component';
import { RepositoryService } from '../../services/repository.service';


@Component({
    selector: 'godsend-search-inline',
    templateUrl: './search-inline.component.html',
})
export class SearchInlineComponent extends SearchBaseComponent implements OnInit {
    @Input()
    type: number = searchType.all;

    @Output()
    readonly found = new EventEmitter<AllSearchResult>();

    constructor(private ss: SearchService, private repo: RepositoryService) { super(); }

    ngOnInit() {
        super.ngOnInit();
    }

    doSearch(term?: string): void {
        if (term == null) {
            term = <string>(this.searchField.value || '');
        }

        if (this.type === searchType.product) {
            this.repo.productFilter.searchTerm = term;
            this.repo.getByFilter();
        } else {
            this.ss.findByType(this.type, term, res => this.found.emit(res));
        }
    }
}
