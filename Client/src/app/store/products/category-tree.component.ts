import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { Category, CatsWithSubs } from '../../models/product.model';
import { NestedTreeControl } from '@angular/cdk/tree';
import { MatTreeNestedDataSource, MatIconRegistry } from '@angular/material';
import { BehaviorSubject } from 'rxjs';
import { CategoryService } from '../../services/category.service';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
    selector: 'godsend-category-tree',
    templateUrl: './category-tree.component.html',
    styleUrls: ['./category-tree.component.css']
})
export class CategoryTreeComponent implements OnInit {
    @Output()
    categoryChanged = new EventEmitter<Category>();

    dataChange = new BehaviorSubject<CatsWithSubs[]>([]);
    nestedTreeControl: NestedTreeControl<CatsWithSubs>;
    nestedDataSource: MatTreeNestedDataSource<CatsWithSubs>;

    constructor(private catService: CategoryService, iconRegistry: MatIconRegistry, sanitizer: DomSanitizer) {
        iconRegistry.addSvgIcon(
            'chevron_right',
            sanitizer.bypassSecurityTrustResourceUrl('assets/img/chevron_right.svg'));
        iconRegistry.addSvgIcon(
            'expand_more',
            sanitizer.bypassSecurityTrustResourceUrl('assets/img/expand_more.svg'));
        iconRegistry.addSvgIcon(
            'subdirectory',
            sanitizer.bypassSecurityTrustResourceUrl('assets/img/subdirectory.svg'));
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
        console.log(newCat);
        this.categoryChanged.emit(newCat);
        
    }

    public hasNestedChild = (_: number, nodeData: CatsWithSubs) => nodeData.subs && nodeData.subs.length > 0;

    private getChildren = (node: CatsWithSubs) => node.subs;
}
