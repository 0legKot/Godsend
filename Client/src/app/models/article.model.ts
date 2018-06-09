import { IdentityUser } from "./user.model";

export class Article {
    constructor(
        id: string,
        content: string,
        info: ArticleInfo
    ) { }
}

export class ArticleInfo {
    constructor(
        id: string,
        name: string,
        rating: number,
        watches: number,
        author: IdentityUser,
        created: string,
        tags: string[]
    ) { }
}