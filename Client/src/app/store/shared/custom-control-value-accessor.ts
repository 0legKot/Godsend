import { ControlValueAccessor } from '@angular/forms';

export abstract class CustomControlValueAccessor<T> implements ControlValueAccessor {
    protected value?: T;

    onChange = (_: T) => { };

    onTouched = (_: T) => { };

    constructor() { }

    changeValue(newValue: T) {
        this.value = newValue;
        this.onChange(this.value);
    }

    writeValue(obj: T): void {
        this.value = obj;
    }

    registerOnChange(fn: (_: T) => any): void {
        this.onChange = fn;
    }

    registerOnTouched(fn: any): void {
        this.onTouched = fn;
    }
}
