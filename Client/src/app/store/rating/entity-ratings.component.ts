import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { LinkRatingEntity } from '../../models/rating.model';
import { entityClass, RepositoryService } from '../../services/repository.service';

@Component({
    selector: 'godsend-entity-ratings[clas][id]',
    templateUrl: './entity-ratings.component.html',
})
export class EntityRatingsComponent implements OnInit {
    @Input()
    clas!: entityClass;

    @Input()
    id?: string;

    ratings?: LinkRatingEntity[];
    showAllRatings = false;

    constructor(private repo: RepositoryService) { }

    ngOnInit() {
    }

    showAll() {
        if (this.id != null) {
            this.repo.getAllRatings(this.clas, this.id, ratings => {
                this.ratings = ratings;
                this.showAllRatings = true;
            });
        }
    }

    hideAll() {
        this.showAllRatings = false;
    }
}
