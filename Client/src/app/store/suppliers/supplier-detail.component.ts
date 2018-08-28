
// import { switchMap } from 'rxjs/operators';
import { Component, OnInit, HostBinding } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { Observable } from 'rxjs';

import { RepositoryService, entityClass } from '../../services/repository.service';
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
    readonly clas: entityClass = 'supplier';

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
            this.repo.deleteEntity('supplier', this.supp.info.id, 1, 10);
            this.gotoSuppliers(undefined);
        }
    }

    gotoSuppliers(supplier?: Supplier) {
        const supplierId = supplier ? supplier.id : null;
        this.router.navigate(['/suppliers', { id: supplierId }]);
    }

    gotoProduct(prodId: string) {
        this.router.navigate(['/products/'+prodId]);
    }

    ngOnInit() {
        this.repo.getEntity<Supplier>('supplier', this.route.snapshot.params.id, s => {
            this.supp = s;
            console.log(s.products);
        });
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
            console.log('EDIT');
            console.log(this.supp);
            this.repo.createOrEditEntity('supplier', Supplier.EnsureType(this.supp), 1, 10);
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
}
