import { IdentityUser } from './user.model';

export interface CommentWithSubs {
    comment: LinkCommentEntity;
    subs: CommentWithSubs[];
}

export interface LinkCommentEntity {
    id: string;
    comment: string;
    baseCommentId: string;
    author: IdentityUser;
}
