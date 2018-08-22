import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { LinkRatingEntity } from '../../models/rating.model';

@Component({
    selector: 'godsend-entity-ratings',
    templateUrl: './entity-ratings.component.html',
})
export class EntityRatingsComponent implements OnInit {
    @Input()
    ratings?: LinkRatingEntity

    @Output()
    close = new EventEmitter<void>()

    constructor() { }

    ngOnInit() {
    }

    onClose() {
        this.close.emit();
    }
}
