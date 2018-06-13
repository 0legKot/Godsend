
// import { switchMap } from 'rxjs/operators';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { RepositoryService } from '../../services/repository.service';
import { Article, ArticleInfo } from '../../models/article.model';

@Component({
    selector: 'godsend-article-detail',
    templateUrl: 'article-detail.component.html'
})
export class ArticleDetailComponent implements OnInit {
    article?: Article;
    edit = false;

    backup = {
        name: '',
        content: '',
        tags:['']
    }
    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private service: RepositoryService) {  }

    gotoArticles(article: Article) {
        const articleId = article ? article.id : null;
        this.router.navigate(['/articles', { id: articleId }]);
    }

    ngOnInit() {
        this.service.getEntity<Article>(this.route.snapshot.params.id, a => this.article = a, 'article');
    }
    editMode() {
        if (this.article == null) {
            console.log('no data');
            return;
        }

        this.backup = {
            name: this.article.info.name,
            content: this.article.content,
            tags: this.article.info.tags
        };

        this.edit = true;
    }

    save() {
        if (this.article) {
            this.service.createOrEditEntity('article', Article.EnsureType(this.article));
        }

        this.edit = false;
    }

    discard() {
        if (this.article) {
            this.article.info.name = this.backup.name;
            this.article.content = this.backup.content;
            this.article.info.tags = this.backup.tags;
        }

        this.edit = false;
    }


}
