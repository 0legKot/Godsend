import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { LinkCommentEntity } from '../../models/comment.model';
import { last } from '@angular/router/src/utils/collection';

@Component({
    selector: 'godsend-comment[comment]',
    templateUrl: './comment.component.html',
})
export class CommentComponent implements OnInit {
    @Input()
    comment!: LinkCommentEntity;

    @Output()
    readonly send = new EventEmitter<string>();

    @Output()
    readonly delete = new EventEmitter<void>();

    @Output()
    readonly edit = new EventEmitter<string>();

    isReplyMode = false;
    isEditMode = false;

    newComment?: string;
    editedComment?: string;

    constructor() { }

    ngOnInit() {
        this.editedComment = this.comment.comment;
    }

    sendComment() {
        if (this.newComment) {
            this.send.emit(this.newComment);
        }
        this.isReplyMode = false;
    }

    editComment() {
        if (this.editedComment) {
            this.edit.emit(this.editedComment);
        }
        this.isEditMode = false;
    }

    discardEdit() {
        this.editedComment = this.comment.comment;
        this.isEditMode = false;
    }

    deleteComment() {
        this.delete.emit();
    }

    enterEditMode() {
        this.isEditMode = true;
    }

    exitEditMode() {
        this.isEditMode = false;
    }

    enterReplyMode() {
        this.isReplyMode = true;
    }

    exitReplyMode() {
        this.isReplyMode = false;
    }
}
