import { Component, OnInit, Input } from '@angular/core';
import { RepositoryService, entityClass } from '../../services/repository.service';
import { NestedTreeControl } from '@angular/cdk/tree';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { BehaviorSubject } from 'rxjs';
import { CommentWithSubs } from '../../models/comment.model';
import { MatIconRegistry } from '@angular/material';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'godsend-comment-wrapper',
    templateUrl: './comment-wrapper.component.html',
    styleUrls: ['./comment-wrapper.component.css', '../products/category-tree.component.css']
})
export class CommentWrapperComponent implements OnInit {
    @Input()
    id!: string;

    @Input()
    clas!: entityClass;

    dataChange = new BehaviorSubject<CommentWithSubs[]>([]);
    nestedTreeControl: NestedTreeControl<CommentWithSubs>;
    nestedDataSource: MatTreeNestedDataSource<CommentWithSubs>;

    constructor(private repo: RepositoryService, iconRegistry: MatIconRegistry, sanitizer: DomSanitizer) {
        iconRegistry.addSvgIcon(
            'chevron_right',
            sanitizer.bypassSecurityTrustResourceUrl('assets/img/chevron_right.svg'));
        iconRegistry.addSvgIcon(
            'expand_more',
            sanitizer.bypassSecurityTrustResourceUrl('assets/img/expand_more.svg'));
        iconRegistry.addSvgIcon(
            'subdirectory',
            sanitizer.bypassSecurityTrustResourceUrl('assets/img/subdirectory.svg'));
        this.nestedTreeControl = new NestedTreeControl<CommentWithSubs>(this.getChildren);
        this.nestedDataSource = new MatTreeNestedDataSource();
        this.dataChange.subscribe(data => this.nestedDataSource.data = data);
    }

    ngOnInit() {
        this.refreshComments();
    }

    send(parentId: string | null, content: string) {
        this.repo.sendComment(this.clas, this.id, parentId, content, _ => {
            this.refreshComments();
        });
    }

    delete(commentId: string): void {
        this.repo.deleteComment(this.clas, this.id, commentId, _ => {
            this.refreshComments();
        });
    }

    edit(commentId: string, content: string) {
        this.repo.editComment(this.clas, commentId, content, _ => {
            this.refreshComments();
        });
    }

    refreshComments() {
        this.repo.getEntityComments(this.clas, this.id, commentsWithSubs => {
            this.dataChange.next(commentsWithSubs);
        });
    }

    hasNestedChild = (_: number, nodeData: CommentWithSubs) => nodeData.subs && nodeData.subs.length > 0;

    private getChildren = (node: CommentWithSubs) => node.subs;
}
