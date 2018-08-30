import { IdentityUser } from './user.model';
var Article = /** @class */ (function () {
    function Article(content, info, id) {
        if (id === void 0) { id = ''; }
        this.content = content;
        this.info = info;
        this.id = id;
    }
    Article.EnsureType = function (art) {
        return new Article(art.content, art.info, art.id);
    };
    Article.prototype.toCreateEdit = function () {
        return {
            id: this.id || undefined,
            content: this.content,
            info: {
                name: this.info.name,
                tags: this.info.tags
            }
        };
    };
    return Article;
}());
export { Article };
var ArticleInfo = /** @class */ (function () {
    function ArticleInfo(name, description, tags, id, created, author, rating, watches) {
        if (description === void 0) { description = ''; }
        if (id === void 0) { id = ''; }
        if (created === void 0) { created = ''; }
        if (author === void 0) { author = new IdentityUser(); }
        if (rating === void 0) { rating = 0; }
        if (watches === void 0) { watches = 0; }
        this.name = name;
        this.description = description;
        this.tags = tags;
        this.id = id;
        this.created = created;
        this.author = author;
        this.rating = rating;
        this.watches = watches;
    }
    return ArticleInfo;
}());
export { ArticleInfo };
var ArticleTags = /** @class */ (function () {
    function ArticleTags(value) {
        this.tag = { value: value };
    }
    return ArticleTags;
}());
export { ArticleTags };
//# sourceMappingURL=article.model.js.map