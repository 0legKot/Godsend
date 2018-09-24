import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { Category, CatsWithSubs, FilterInfoView, StringPropertyInfo, IntPropertyInfo, DecimalPropertyInfo, orderBy } from '../../models/product.model';
import { NestedTreeControl } from '@angular/cdk/tree';
import { MatTreeNestedDataSource } from '@angular/material';
import { BehaviorSubject } from 'rxjs';
import { CategoryService } from '../../services/category.service';
import { RepositoryService } from '../../services/repository.service';
import {
 orderBy
} from '../../models/product.model';

@Component({
    selector: 'godsend-filter',
    templateUrl: './filter.component.html',
    //styleUrls: ['./filter.component.css']
})
export class Filter implements OnInit {

    @Output()
    updated = new EventEmitter<null>();

    constructor(private repo: RepositoryService) { }
    ngOnInit(): void {
        //throw new Error("Method not implemented.");
    }
    orderBy: allowedOrderBy[] = orderBy;

    filter: FilterInfoView = new FilterInfoView();
    getByFilter(): void {
        if (this.filter) {
            if (this.filter.stringProps) {
                this.repo.productFilter.stringProps = this.filter.stringProps
                    .filter(prop => prop.part !== '' && prop.part != null)
                    .map(prop => new StringPropertyInfo(prop.propId, prop.part));
            }
            if (this.filter.intProps) {
                this.repo.productFilter.intProps = this.filter.intProps
                    .filter(prop => prop.left != null && prop.right != null)
                    .map(prop => new IntPropertyInfo(prop.propId, prop.left, prop.right));
            }
            if (this.filter.decimalProps) {
                this.repo.productFilter.decimalProps = this.filter.decimalProps
                    .filter(prop => prop.left != null && prop.right != null)
                    .map(prop => new DecimalPropertyInfo(prop.propId, prop.left, prop.right));
            }

            this.repo.productFilter.orderBy = this.filter.orderBy;

            this.repo.productFilter.sortAscending = this.filter.sortAscending;

            this.updated.emit();
        }
    }
}
