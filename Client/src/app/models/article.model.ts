import { IdentityUser } from './user.model';
import { IEntity } from './entity.model';

export class Article implements IEntity<ArticleInfo> {
    static EnsureType(art: Article): Article {
        return new Article(art.id, art.content, art.info);
    }

    public toCreateEdit() {
        return {
            id: this.id || undefined,
            content: this.content,
            info: {
                name: this.info.name,
                tags: this.info.tags
            }
        };
    }

    constructor(
        public id: string,
        public content: string,
        public info: ArticleInfo
    ) { }
}

export class ArticleInfo {
    constructor(
        public id: string,
        public name: string,
        public rating: number,
        public watches: number,
        public author: IdentityUser,
        public created: string,
        public tags: string[]
    ) { }
}
