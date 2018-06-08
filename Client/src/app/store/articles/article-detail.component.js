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
var ArticleDetailComponent = /** @class */ (function () {
    function ArticleDetailComponent(route, router, service) {
        this.route = route;
        this.router = router;
        this.service = service;
    }
    ArticleDetailComponent.prototype.gotoArticles = function (article) {
        // smth wrong with article here
        var articleId = article ? article.id : null;
        this.router.navigate(['/articles', { id: articleId }]);
    };
    ArticleDetailComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.service.getEntity(this.route.snapshot.params.id, function (a) { return _this.article = a; }, 'article');
    };
    ArticleDetailComponent = __decorate([
        Component({
            selector: 'godsend-article-detail',
            templateUrl: 'article-detail.component.html'
        }),
        __metadata("design:paramtypes", [ActivatedRoute,
            Router,
            RepositoryService])
    ], ArticleDetailComponent);
    return ArticleDetailComponent;
}());
export { ArticleDetailComponent };
//# sourceMappingURL=article-detail.component.js.map