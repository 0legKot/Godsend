import { Component, OnInit, Input } from '@angular/core';
import { entityClass, RepositoryService } from '../../services/repository.service';
import { CustomControlValueAccessor } from '../shared/custom-control-value-accessor';
import { NG_VALUE_ACCESSOR } from '@angular/forms';
import { StorageService } from '../../services/storage.service';

@Component({
    selector: 'godsend-ratings[clas][id]',
    templateUrl: './ratings.component.html',
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            useExisting: RatingsComponent,
            multi: true
        }
    ]
})
export class RatingsComponent extends CustomControlValueAccessor<number> implements OnInit {
    @Input()
    clas!: entityClass;

    // value: number - in base class - avg rating

    @Input()
    id!: string; // entity id

    userRating?: number;

    get authenticated() {
        return this.storage.authenticated;
    }

    constructor(
        private repo: RepositoryService,
        private storage: StorageService
    ) { super(); }

    ngOnInit() {
        if (this.authenticated) {
            this.repo.getUserRating(this.clas, this.id, rating => this.userRating = rating);
        }
    }

    saveRating(newRating: number) {
        if (this.id != null) {
            this.repo.saveRating(this.clas, this.id, newRating, newAvg => {
                if (this.id != null) {
                    this.changeValue(newAvg);
                    this.userRating = newRating;
                }
            });
        }
    }
}
