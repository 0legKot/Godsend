var Article = /** @class */ (function () {
    function Article(id, content, info) {
        this.id = id;
        this.content = content;
        this.info = info;
    }
    Article.EnsureType = function (art) {
        return new Article(art.id, art.content, art.info);
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
    function ArticleInfo(id, name, rating, watches, author, created, tags) {
        this.id = id;
        this.name = name;
        this.rating = rating;
        this.watches = watches;
        this.author = author;
        this.created = created;
        this.tags = tags;
    }
    return ArticleInfo;
}());
export { ArticleInfo };
//# sourceMappingURL=article.model.js.map