var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component } from '@angular/core';
import { RepositoryService } from '../../services/repository.service';
import { ArticleInfo, Article } from '../../models/article.model';
import { Router } from '@angular/router';
var ArticlesComponent = /** @class */ (function () {
    function ArticlesComponent(repo, router) {
        this.repo = repo;
        this.router = router;
        this.page = 1;
        this.rpp = 5;
    }
    Object.defineProperty(ArticlesComponent.prototype, "articles", {
        get: function () {
            return this.repo.articles;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(ArticlesComponent.prototype, "pagesCount", {
        get: function () {
            return Math.ceil(this.repo.articlesCount / this.rpp);
        },
        enumerable: true,
        configurable: true
    });
    ArticlesComponent.prototype.onPageChanged = function (page) {
        this.page = page;
        this.getArticles();
    };
    ArticlesComponent.prototype.getArticles = function () {
        this.repo.getEntities('article', this.page, this.rpp);
    };
    ArticlesComponent.prototype.ngOnInit = function () {
        this.getArticles();
    };
    ArticlesComponent.prototype.createArticle = function (content, name, tags) {
        var _this = this;
        var art = new Article(content, new ArticleInfo(name, 'Provide description', tags));
        this.repo.createOrEditEntity('article', art, this.page, this.rpp, function (info) { return _this.router.navigateByUrl('articles/' + info.id); });
    };
    ArticlesComponent.prototype.deleteArticle = function (id) {
        this.repo.deleteEntity('article', id, this.page, this.rpp);
    };
    ArticlesComponent = __decorate([
        Component({
            selector: 'godsend-articles',
            templateUrl: './articles.component.html',
            styleUrls: ['./articles.component.css']
        }),
        __metadata("design:paramtypes", [RepositoryService, Router])
    ], ArticlesComponent);
    return ArticlesComponent;
}());
export { ArticlesComponent };
//# sourceMappingURL=articles.component.js.map