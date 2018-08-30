import { IdentityUser } from './user.model';
import { IEntity } from './entity.model';

export class Article implements IEntity<ArticleInfo> {
    static EnsureType(art: Article): Article {
        return new Article(art.content, art.info, art.id);
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
        public content: string,
        public info: ArticleInfo,
        public id: string = '',
    ) { }
}

export class ArticleInfo {
    constructor(
        public name: string,
        public description: string = '',
        public tags: ArticleTags[],
        public id: string = '',
        public created: string = '',
        public author: IdentityUser = new IdentityUser(),
        public rating: number = 0,
        public watches: number = 0,
        public commentsCount: number = 0
    ) { }
}

export class ArticleTags {
    id?: string;
    tag: Tag;

    constructor(value: string) {
        this.tag = { value: value };
    }
}

export interface Tag {
    id?: string;
    value: string;
}
