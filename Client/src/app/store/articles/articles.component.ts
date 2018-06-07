import { Component, OnInit } from '@angular/core';
import { RepositoryService } from '../../services/repository.service';
import { ArticleInfo } from '../../models/article.model';

@Component({
    selector: 'godsend-articles',
    templateUrl: './articles.component.html'
})
export class ArticlesComponent implements OnInit {
    articles?: ArticleInfo[];

    constructor(private repo: RepositoryService) { }

    ngOnInit() {
        this.repo.getEntities<ArticleInfo>('article', x => { this.articles = x; console.dir(x); });
    }
}
