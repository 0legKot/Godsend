
// import { switchMap } from 'rxjs/operators';
import { Component, OnInit, HostBinding } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';

import { RepositoryService, entityClass } from '../../services/repository.service';
import { Supplier } from '../../models/supplier.model';
import { ImageService } from '../../services/image.service';
import { StorageService } from '../../services/storage.service';
import { Image } from '../../models/image.model';

@Component({
    selector: 'godsend-supplier-detail',
    templateUrl: 'supplier-detail.component.html',
    styleUrls: ['./supplier-detail.component.css']
})
export class SupplierDetailComponent implements OnInit {
    supp?: Supplier;
    backup: SupplierBackup = {
        name: '',
        address: '',
        images: []
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
        private storage: StorageService
    ) { }

    deleteSupplier() {
        if (this.supp) {
            this.repo.deleteEntity('supplier', this.supp.info.id, 1, 10);
            this.router.navigate(['/suppliers']);
        }
    }

    gotoProduct(prodId: string) {
        this.router.navigate(['/products/'+prodId]);
    }

    ngOnInit() {
        const id = this.route.snapshot.params.id;
        this.repo.getEntity<Supplier>('supplier', id, s => {
            this.supp = s;
            console.log(s.productsAndPrices);
            //if (this.supp.images) {
            //    this.imageService.getImages(this.supp.images.map(i => i.id), images => { this.images = images; });
            //}
        });
        if (this.repo.viewedSuppliersIds.find(x => x === id) === undefined) {
            this.repo.viewedSuppliersIds.push(this.route.snapshot.params.id);
        }
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

    setImages(newImages: Image[]) {
        if (this.supp) {
            this.supp.images = newImages;
        }
    }

    discard() {
        if (this.supp) {
            this.supp.info.name = this.backup.name;
            this.supp.info.location.address = this.backup.address;
            this.supp.images = this.backup.images;
        }

        this.edit = false;
    }
}

interface SupplierBackup {
    name: string;
    address: string;
    images?: Image[];
}
