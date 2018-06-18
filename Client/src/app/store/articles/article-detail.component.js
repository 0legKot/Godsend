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
import { Article } from '../../models/article.model';
var ArticleDetailComponent = /** @class */ (function () {
    function ArticleDetailComponent(route, router, service) {
        this.route = route;
        this.router = router;
        this.service = service;
        this.edit = false;
        this.backup = {
            name: '',
            content: '',
            tags: ['']
        };
    }
    ArticleDetailComponent.prototype.gotoArticles = function (article) {
        var articleId = article ? article.id : null;
        this.router.navigate(['/articles', { id: articleId }]);
    };
    ArticleDetailComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.service.getEntity(this.route.snapshot.params.id, function (a) { return _this.article = a; }, 'article');
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
            this.service.createOrEditEntity('article', Article.EnsureType(this.article));
        }
        this.edit = false;
    };
    ArticleDetailComponent.prototype.discard = function () {
        if (this.article) {
            this.article.info.name = this.backup.name;
            this.article.content = this.backup.content;
            this.article.info.tags = this.backup.tags;
        }
        this.edit = false;
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
            RepositoryService])
    ], ArticleDetailComponent);
    return ArticleDetailComponent;
}());
export { ArticleDetailComponent };
//# sourceMappingURL=article-detail.component.js.map