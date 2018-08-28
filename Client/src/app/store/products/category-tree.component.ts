import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { Category, CatsWithSubs } from '../../models/product.model';
import { NestedTreeControl } from '@angular/cdk/tree';
import { MatTreeNestedDataSource } from '@angular/material';
import { BehaviorSubject } from 'rxjs';
import { CategoryService } from '../../services/category.service';

@Component({
    selector: 'godsend-category-tree',
    templateUrl: './category-tree.component.html',
})
export class CategoryTreeComponent implements OnInit {
    @Output()
    categoryChanged = new EventEmitter<Category>();

    dataChange = new BehaviorSubject<CatsWithSubs[]>([]);
    nestedTreeControl: NestedTreeControl<CatsWithSubs>;
    nestedDataSource: MatTreeNestedDataSource<CatsWithSubs>;

    constructor(private catService: CategoryService) {
        this.nestedTreeControl = new NestedTreeControl<CatsWithSubs>(this.getChildren);
        this.nestedDataSource = new MatTreeNestedDataSource();
        this.dataChange.subscribe(data => this.nestedDataSource.data = data);
    }

    ngOnInit() {
        if (this.catService.cats != null) {
            this.dataChange.next(this.catService.cats);
        }
    }

    selectCategory(newCat: Category) {
        this.categoryChanged.emit(newCat);
    }

    private hasNestedChild = (_: number, nodeData: CatsWithSubs) => nodeData.subs && nodeData.subs.length > 0;

    private getChildren = (node: CatsWithSubs) => node.subs;
}
