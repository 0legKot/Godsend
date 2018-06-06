import { Component, OnInit } from '@angular/core';
import { Repository } from '../../models/repository';
import { ArticleInfo } from '../../models/article.model';

@Component({
    selector: 'godsend-articles',
    templateUrl: './articles.component.html'
})
export class ArticlesComponent implements OnInit {
    articles?: ArticleInfo[];

    constructor(private repo: Repository) { }

    ngOnInit() {
        this.repo.getEntities<ArticleInfo>('article', x => { this.articles = x; console.dir(x); });
    }
}
