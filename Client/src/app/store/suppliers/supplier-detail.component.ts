
// import { switchMap } from 'rxjs/operators';
import { Component, OnInit, HostBinding } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { Observable } from 'rxjs';

import { RepositoryService } from '../../services/repository.service';
import { forEach } from '@angular/router/src/utils/collection';
import { Supplier } from '../../models/supplier.model';
import { ImageService } from '../../services/image.service';
import { StorageService } from '../../services/storage.service';
import { LinkRatingEntity } from '../../models/rating.model';

@Component({
    selector: 'godsend-supplier-detail',
    templateUrl: 'supplier-detail.component.html',
    styleUrls: ['./supplier-detail.component.css']
})
export class SupplierDetailComponent implements OnInit {
    supp?: Supplier;
    image = '';
    backup = {
        name: '',
        address: ''
    };

    edit = false;
    showAllRatings = false;
    allRatings?: LinkRatingEntity[];

    get authenticated() {
        return this.storage.authenticated;
    }

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private repo: RepositoryService,
        private imageService: ImageService,
        private storage: StorageService
    ) { }

    deleteSupplier() {
        if (this.supp) {
            this.repo.deleteEntity('supplier', this.supp.info.id,1,10);
            this.gotoSuppliers(undefined);
        }
    }

    gotoSuppliers(supplier?: Supplier) {
        const supplierId = supplier ? supplier.id : null;
        this.router.navigate(['/suppliers', { id: supplierId }]);
    }

    ngOnInit() {
        this.repo.getEntity<Supplier>(this.route.snapshot.params.id, s => this.supp = s, 'supplier');
        this.imageService.getImage(this.route.snapshot.params.id, image => { this.image = image; });
    }

    editMode() {
        if (this.supp == null) {
            console.log('no data');
            return;
        }

        this.backup = {
            name: this.supp.info.name,
            address: this.supp.info.location.address
        };

        this.edit = true;
    }

    save() {
        if (this.supp) {
            this.repo.createOrEditEntity('supplier', Supplier.EnsureType(this.supp),1,10);
        }

        this.edit = false;
    }

    discard() {
        if (this.supp) {
            this.supp.info.name = this.backup.name;
            this.supp.info.location.address = this.backup.address;
        }

        this.edit = false;
    }

    saveRating(newRating: number) {
        if (this.supp != null) {
            this.repo.saveRating('supplier', this.supp.id, newRating, newAvg => {
                if (this.supp != null) {
                    this.supp.info.rating = newAvg;
                }
            });
        }
    }

    getAllRatings() {
        if (this.supp != null) {
            this.repo.getAllRatings('supplier', this.supp.id, ratings => {
                this.allRatings = ratings;
                this.showAllRatings = true;
            });
        }
    }

    hideRatings() {
        this.showAllRatings = false;
    }
}
