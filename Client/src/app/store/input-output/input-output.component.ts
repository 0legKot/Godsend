import { Component, OnInit, Input } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
    selector: 'godsend-input-output',
    templateUrl: './input-output.component.html',
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            useExisting: InputOutputComponent,
            multi: true
        }
    ]
})
export class InputOutputComponent implements OnInit, ControlValueAccessor {
    @Input()
    edit = false;
    @Input()
    size = 20;
    @Input()
    huge = false;

    value = '';

    onChange = (_: string) => { };

    onTouched = (_: string) => { };

    constructor() { }

    changeValue(newValue: string) {
        this.value = newValue;
        this.onChange(this.value);
    }

    writeValue(obj: string): void {
        this.value = obj;
    }

    registerOnChange(fn: (_:string) => any): void {
        this.onChange = fn;
    }

    registerOnTouched(fn: any): void {
        this.onTouched = fn;
    }

    ngOnInit() {
        console.log(this.size);
    }
}
