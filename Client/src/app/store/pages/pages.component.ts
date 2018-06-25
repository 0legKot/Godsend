import { Component } from '@angular/core';
import { RepositoryService } from '../../services/repository.service';

@Component({
    selector: 'godsend-pages',
    templateUrl: './pages.component.html',
    styleUrls: ['./pages.component.css']
})
export class PagesComponent {
    page: number = 1;
    constructor(repo: RepositoryService) { }
    nextPage() {
        this.page++;
        this.goToPage(this.page);
    }
    prevPage() {
        this.page--;
        this.goToPage(this.page);
    }
    goToPage(pageNumber: number) {
        this.page = pageNumber;
        this.update()
    }
    update() { }
}