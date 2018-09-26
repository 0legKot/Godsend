import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { RepositoryService, entityClass } from '../../services/repository.service';
import { Article, ArticleTags } from '../../models/article.model';
import { StorageService } from '../../services/storage.service';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
    selector: 'godsend-article-detail',
    templateUrl: 'article-detail.component.html',
    styleUrls: [
        './article-detail.component.css',
        '../products/product-detail.component.css',
        '../input-output/input-output.component.css'
]
})
export class ArticleDetailComponent implements OnInit {
    article?: Article;
    comments?: any;
    edit = false;
    readonly clas: entityClass = 'article';
    /**
     * Tags as a single string, where tags begin with a '#' and are separated by spaces
     */
    stringTags?: string;

    backup: ArticleBackup = {
        name: '',
        content: '',
        tags: []
    };

    get authenticated() {
        return this.storage.authenticated;
    }

    get canDelete(): boolean {
        return Boolean(this.auth.roles.find(x => x == 'Administrator' || x == 'Moderator'));
    }

    get canEdit(): boolean {
        return Boolean(this.auth.roles.find(x => x == 'Administrator' || x == 'Moderator'));
    }

    constructor(
        private route: ActivatedRoute,
        private auth: AuthenticationService,
        private storage: StorageService,
        private repo: RepositoryService
    ) { }

    ngOnInit() {
        const id = this.route.snapshot.params.id;
        this.repo.getEntity<Article>('article', id, a => {
            this.article = a;
            this.stringifyTags();
        });
        if (this.repo.viewedArticlesIds.find(x => x === id) === undefined) {
            this.repo.viewedArticlesIds.push(this.route.snapshot.params.id);
        }
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
            if (this.stringTags) {
                this.article.info.tags = this.stringTags.split(' ')
                    .filter(str => str.startsWith('#'))
                    .map(str => str.substring(1))
                    .map(str => new ArticleTags(str));
            } else {
                this.article.info.tags = [];
            }

            console.log('TAGS: ');
            console.dir(this.article.info.tags);

            this.repo.createOrEditEntity('article', Article.EnsureType(this.article), 1, 10);
        }

        this.edit = false;
    }

    discard() {
        if (this.article) {
            this.article.info.name = this.backup.name;
            this.article.content = this.backup.content;
            this.article.info.tags = this.backup.tags;
            this.stringifyTags();
        }

        this.edit = false;
    }

    private stringifyTags() {
        if (this.article) {
            this.stringTags = this.article.info.tags.map(tag => '#' + tag.tag.value).reduce((prev, next) => prev + ' ' + next);
        }
    }
}

export interface ArticleBackup {
    name: string;
    content: string;
    tags: ArticleTags[];
}
