
// import { switchMap } from 'rxjs/operators';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { Repository } from '../../models/repository';
import { Article } from '../../models/article.model';

@Component({
    selector: 'godsend-article-detail',
    templateUrl: 'article-detail.component.html'
})
export class ArticleDetailComponent implements OnInit {
    article?: Article;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private service: Repository) {  }

    gotoArticles(article: Article) {
        const articleId = '1';//TODO article ? article.id : null;
        this.router.navigate(['/articles', { id: articleId }]);
    }

    ngOnInit() {
        this.service.getEntity<Article>(this.route.snapshot.params.id, a => this.article = a, 'article');
    }

}
