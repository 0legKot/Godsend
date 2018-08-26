import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { LinkCommentEntity } from '../../models/comment.model';

@Component({
    selector: 'godsend-comment[comment]',
    templateUrl: './comment.component.html',
})
export class CommentComponent implements OnInit {
    @Input()
    comment!: LinkCommentEntity

    @Output()
    send = new EventEmitter<string>();

    @Output()
    delete = new EventEmitter<void>();

    isReplyMode = false;

    newComment?: string;

    constructor() { }

    ngOnInit() {
    }

    reply() {
        this.isReplyMode = true;
    }

    sendComment() {
        if (this.newComment) {
            this.send.emit(this.newComment);
        }
        this.isReplyMode = false;
    }

    deleteComment() {
        this.delete.emit();
    }

    cancel() {
        this.isReplyMode = false;
    }
}
