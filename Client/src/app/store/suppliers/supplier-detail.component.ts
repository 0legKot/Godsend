
// import { switchMap } from 'rxjs/operators';
import { Component, OnInit, HostBinding } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { Observable } from 'rxjs';

import { RepositoryService } from '../../services/repository.service';
import { forEach } from '@angular/router/src/utils/collection';
import { Supplier } from '../../models/supplier.model';

@Component({
    selector: 'godsend-supplier-detail',
    templateUrl: 'supplier-detail.component.html',
    styleUrls: ['./supplier-detail.component.css']
})
export class SupplierDetailComponent implements OnInit {
    supp?: Supplier;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private service: RepositoryService) {  }

    gotoSuppliers(supplier: Supplier) {
        const supplierId = supplier ? supplier.id : null;
        this.router.navigate(['/suppliers', { id: supplierId }]);
    }

    ngOnInit() {
        this.service.getEntity<Supplier>(this.route.snapshot.params.id, s => this.supp = s, 'supplier');
    }

}
