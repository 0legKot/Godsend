import { Component } from '@angular/core';
import { Repository } from '../../models/repository';
import { Supplier } from '../../models/supplier.model';
import { OnInit } from '@angular/core';

@Component({
    selector: 'godsend-suppliers',
    templateUrl: './suppliers.component.html'
})
export class SuppliersComponent implements OnInit {
    suppliers: Supplier[] = [];
    constructor(private repo: Repository)  { }
    ngOnInit() {
        this.repo.getEntities<Supplier>('supplier', s => this.suppliers = s);
    }
}
