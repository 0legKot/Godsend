import { Component, OnInit } from '@angular/core';
import { RepositoryService } from '../../services/repository.service';
import { ArticleInfo, Article } from '../../models/article.model';

@Component({
    selector: 'godsend-articles',
    templateUrl: './articles.component.html',
    styleUrls: ['./articles.component.css']
})
export class ArticlesComponent implements OnInit {
    get articles() {
        return this.repo.articles;
    }

    constructor(private repo: RepositoryService) { }

    ngOnInit() {
        this.repo.getEntities('article');
    }

    createArticle(content: string, name: string, tags: string[]) {
        const art = new Article(content, new ArticleInfo(name, tags));
        this.repo.createOrEditEntity('article', art);
    }

    deleteArticle(id: string) {

        this.repo.deleteEntity('article', id);
        
    }
}
