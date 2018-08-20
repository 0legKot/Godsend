import { Component, OnInit } from '@angular/core';
import { RepositoryService } from '../../services/repository.service';
import { ArticleInfo, Article } from '../../models/article.model';
import { Router } from '@angular/router';

@Component({
    selector: 'godsend-articles',
    templateUrl: './articles.component.html',
    styleUrls: ['./articles.component.css']
})
export class ArticlesComponent implements OnInit {
    page: number = 1;
    rpp: number = 5;

    get articles() {
        return this.repo.articles;
    }

    get pagesCount(): number {
        return Math.ceil(this.repo.articlesCount / this.rpp);
    }

    onPageChanged(page: number) {
        this.page = page;
        this.getArticles();
    }

    constructor(private repo: RepositoryService, private router: Router) { }

    getArticles() {
        this.repo.getEntities('article', this.page, this.rpp);
    }

    ngOnInit() {
        this.getArticles();
    }

    createArticle(content: string, name: string, tags: string[]) {
        const art = new Article(content, new ArticleInfo(name, "Provide description", tags));
        this.repo.createOrEditEntity('article', art, this.page, this.rpp, info => this.router.navigateByUrl('articles/' + info.id));
    }

    deleteArticle(id: string) {
        this.repo.deleteEntity('article', id, this.page, this.rpp);
    }
}
