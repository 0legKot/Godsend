import { Component, Output, EventEmitter, Input } from '@angular/core';

@Component({
    selector: 'godsend-pages',
    templateUrl: './pages.component.html',
    styleUrls: ['./pages.component.css']
})
export class PagesComponent {
    page: number = 1;

    @Input()
    pagesCount: number=1;

    @Output()
    pageChanged = new EventEmitter<number>();

    constructor() { }

    nextPage() {
        if (this.page < this.pagesCount) {
            this.page++;
            this.pageChanged.emit(this.page);
        }
    }

    prevPage() {
        if (this.page > 1) {
            this.page--;
            this.pageChanged.emit(this.page);
        }
    }
}