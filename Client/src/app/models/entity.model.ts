export interface IEntity<T extends IInformation> {
    id: string;
    info: T;

    toCreateEdit(): any;
}

export interface IInformation {
    id: string;
    name: string;
    watches: number;
    rating: number;
}
export class LinkCommentEntity {
    constructor(
        public id: string,
        public comment: string,
        public baseComment?: LinkCommentEntity
    ) { }
}

export class CommentWithSubs {
    constructor(
        public comment: LinkCommentEntity,
        public subs: CommentWithSubs[]
    ) { }
}
