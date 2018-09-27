import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { CustomControlValueAccessor } from '../custom-control-value-accessor';
import { NG_VALUE_ACCESSOR } from '@angular/forms';

/**
 * A component to manage editing and deleting of anything.
 * ngModel is a boolean indicating current mode (true == editing)
 * */
@Component({
    selector: 'godsend-edit-delete',
    templateUrl: './edit-delete.component.html',
    styleUrls: ['./edit-delete.component.css'],
    providers: [
        { provide: NG_VALUE_ACCESSOR, useExisting: EditDeleteComponent, multi: true }
    ],
})
export class EditDeleteComponent extends CustomControlValueAccessor<boolean> {
    @Input()
    canEdit = true;

    @Input()
    canDelete = true;

    @Output()
    readonly save = new EventEmitter<void>();

    @Output()
    readonly discard = new EventEmitter<void>();

    @Output()
    readonly delete = new EventEmitter<void>();

    @Output()
    readonly edit = new EventEmitter<void>();

    constructor() {
        super();
    }

    toEditMode() {
        this.edit.emit();
        this.changeValue(true);
    }

    onSave() {
        this.save.emit();
        this.changeValue(false);
    }

    onDelete() {
        this.delete.emit();
        this.changeValue(false);
    }

    onDiscard() {
        this.discard.emit();
        this.changeValue(false);
    }
}
