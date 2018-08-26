import { IdentityUser } from './user.model';

export interface LinkRatingEntity {
    id: string;

    rating: number;

    author: IdentityUser;
}
