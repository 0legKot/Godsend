
// import { switchMap } from 'rxjs/operators';
import { Component, OnInit, HostBinding } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { Observable } from 'rxjs';

import { RepositoryService } from '../../services/repository.service';
import { forEach } from '@angular/router/src/utils/collection';
import { Supplier } from '../../models/supplier.model';
import { ImageService } from '../../services/image.service';

@Component({
    selector: 'godsend-supplier-detail',
    templateUrl: 'supplier-detail.component.html',
    styleUrls: ['./supplier-detail.component.css']
})
export class SupplierDetailComponent implements OnInit {
    supp?: Supplier;
    image: string = '';
    backup = {
        name: '',
        address: ''
    };

    edit = false;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private service: RepositoryService,
        private imageService: ImageService) { }

    deleteSupplier() {
        if (this.supp) {
            this.service.deleteEntity('supplier', this.supp.info.id);
            this.gotoSuppliers(undefined);
        }
    }

    gotoSuppliers(supplier?: Supplier) {
        const supplierId = supplier ? supplier.id : null;
        this.router.navigate(['/suppliers', { id: supplierId }]);
    }

    ngOnInit() {
        this.service.getEntity<Supplier>(this.route.snapshot.params.id, s => this.supp = s, 'supplier');
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
            this.service.createOrEditEntity('supplier', Supplier.EnsureType(this.supp));
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
