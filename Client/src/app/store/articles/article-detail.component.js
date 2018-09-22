var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
// import { switchMap } from 'rxjs/operators';
import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { RepositoryService } from '../../services/repository.service';
import { Article, ArticleTags } from '../../models/article.model';
import { StorageService } from '../../services/storage.service';
var ArticleDetailComponent = /** @class */ (function () {
    function ArticleDetailComponent(route, router, storage, repo) {
        this.route = route;
        this.router = router;
        this.storage = storage;
        this.repo = repo;
        this.edit = false;
        this.clas = 'article';
        this.backup = {
            name: '',
            content: '',
            tags: []
        };
    }
    Object.defineProperty(ArticleDetailComponent.prototype, "authenticated", {
        get: function () {
            return this.storage.authenticated;
        },
        enumerable: true,
        configurable: true
    });
    ArticleDetailComponent.prototype.ngOnInit = function () {
        var _this = this;
        var id = this.route.snapshot.params.id;
        this.repo.getEntity('article', id, function (a) {
            _this.article = a;
            _this.stringifyTags();
        });
        if (this.repo.viewedArticlesIds.find(function (x) { return x === id; }) === undefined) {
            this.repo.viewedArticlesIds.push(this.route.snapshot.params.id);
        }
    };
    ArticleDetailComponent.prototype.editMode = function () {
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
    };
    ArticleDetailComponent.prototype.save = function () {
        if (this.article) {
            if (this.stringTags) {
                this.article.info.tags = this.stringTags.split(' ')
                    .filter(function (str) { return str.startsWith('#'); })
                    .map(function (str) { return str.substring(1); })
                    .map(function (str) { return new ArticleTags(str); });
            }
            else {
                this.article.info.tags = [];
            }
            console.log('TAGS: ');
            console.dir(this.article.info.tags);
            this.repo.createOrEditEntity('article', Article.EnsureType(this.article), 1, 10);
        }
        this.edit = false;
    };
    ArticleDetailComponent.prototype.discard = function () {
        if (this.article) {
            this.article.info.name = this.backup.name;
            this.article.content = this.backup.content;
            this.article.info.tags = this.backup.tags;
            this.stringifyTags();
        }
        this.edit = false;
    };
    ArticleDetailComponent.prototype.stringifyTags = function () {
        if (this.article) {
            this.stringTags = this.article.info.tags.map(function (tag) { return '#' + tag.tag.value; }).reduce(function (prev, next) { return prev + ' ' + next; });
        }
    };
    ArticleDetailComponent = __decorate([
        Component({
            selector: 'godsend-article-detail',
            templateUrl: 'article-detail.component.html',
            styleUrls: [
                './article-detail.component.css',
                '../products/product-detail.component.css',
                '../input-output/input-output.component.css'
            ]
        }),
        __metadata("design:paramtypes", [ActivatedRoute,
            Router,
            StorageService,
            RepositoryService])
    ], ArticleDetailComponent);
    return ArticleDetailComponent;
}());
export { ArticleDetailComponent };
//# sourceMappingURL=article-detail.component.js.map