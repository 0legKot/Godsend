import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'godsend-stars',
  templateUrl: './stars.component.html',
  styleUrls: ['./stars.component.css']
})
export class StarsComponent {
    @Input()
    readOnly = true;

    @Input()
    size = 26;

    // Number from 0 to 5
    @Input()
    value = 0;

    @Output()
    readonly valueChanged = new EventEmitter<number>();

    // Aliases for 1/2/3/4/5 - star rating respectively
    ratingAliases: string[] = ['awful', 'bad', 'ok', 'good', 'awesome'];

    onValueChanged(newValue: number) {
        this.valueChanged.emit(newValue);
    }
}
