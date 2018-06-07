import { Component } from '@angular/core';
import { RepositoryService } from '../../services/repository.service';
import { Supplier } from '../../models/supplier.model';
import { OnInit } from '@angular/core';

@Component({
    selector: 'godsend-suppliers',
    templateUrl: './suppliers.component.html'
})
export class SuppliersComponent implements OnInit {
    suppliers: Supplier[] = [];
    constructor(private repo: RepositoryService)  { }
    ngOnInit() {
        this.repo.getEntities<Supplier>('supplier', s => this.suppliers = s);
    }
}
