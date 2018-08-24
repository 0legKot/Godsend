import { Component, OnInit, Input, Injectable } from '@angular/core';
import { RepositoryService, entityClass } from '../../services/repository.service';
import { NestedTreeControl } from '@angular/cdk/tree';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { BehaviorSubject } from 'rxjs';
import { CommentWithSubs } from '../../models/comment.model';

@Component({
  selector: 'godsend-comment-wrapper',
    templateUrl: './comment-wrapper.component.html',
    styleUrls: ['./comment-wrapper.component.css']
})
export class CommentWrapperComponent implements OnInit {
    @Input()
    id!: string;

    @Input()
    clas!: entityClass;

    newComment = '';

    dataChange = new BehaviorSubject<CommentWithSubs[]>([]);
    nestedTreeControl: NestedTreeControl<CommentWithSubs>;
    nestedDataSource: MatTreeNestedDataSource<CommentWithSubs>;

    constructor(private repo: RepositoryService) {
        this.nestedTreeControl = new NestedTreeControl<CommentWithSubs>(this._getChildren);
        this.nestedDataSource = new MatTreeNestedDataSource();
        this.dataChange.subscribe(data => this.nestedDataSource.data = data);
    }

    ngOnInit() {
        this.refreshComments();
    }

    public send(parentId: string | null) {
        this.repo.sendComment(this.clas, this.id, parentId, this.newComment, _ => {
            this.refreshComments();
        });
    }    

    refreshComments() {
        this.repo.getEntityComments(this.clas, this.id, commentsWithSubs => {
            this.dataChange.next(commentsWithSubs);
        });
    }

    hasNestedChild = (_: number, nodeData: CommentWithSubs) => nodeData.subs && nodeData.subs.length > 0;

    private _getChildren = (node: CommentWithSubs) => node.subs;
}
