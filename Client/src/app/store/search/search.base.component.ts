import { OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';

export abstract class SearchBaseComponent implements OnInit {
    public searchField: FormControl = new FormControl();

    ngOnInit() {
        this.searchField = new FormControl();
        this.searchField.valueChanges
            .pipe(debounceTime(400))
            .pipe(distinctUntilChanged())
            .subscribe(term => this.doSearch(term));
    }

    abstract doSearch(term: string): void;
}
