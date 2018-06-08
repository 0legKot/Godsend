var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component, Input, Output, EventEmitter } from '@angular/core';
var StarsComponent = /** @class */ (function () {
    function StarsComponent() {
        this.readOnly = true;
        this.size = 26;
        // Number from 0 to 5
        this.value = 0;
        this.valueChanged = new EventEmitter();
        // Aliases for 1/2/3/4/5 - star rating respectively
        this.ratingAliases = ['awful', 'bad', 'ok', 'good', 'awesome'];
    }
    StarsComponent.prototype.onValueChanged = function (newValue) {
        this.valueChanged.emit(newValue);
    };
    __decorate([
        Input(),
        __metadata("design:type", Object)
    ], StarsComponent.prototype, "readOnly", void 0);
    __decorate([
        Input(),
        __metadata("design:type", Object)
    ], StarsComponent.prototype, "size", void 0);
    __decorate([
        Input(),
        __metadata("design:type", Object)
    ], StarsComponent.prototype, "value", void 0);
    __decorate([
        Output(),
        __metadata("design:type", Object)
    ], StarsComponent.prototype, "valueChanged", void 0);
    StarsComponent = __decorate([
        Component({
            selector: 'godsend-stars',
            templateUrl: './stars.component.html',
            styleUrls: ['./stars.component.css']
        })
    ], StarsComponent);
    return StarsComponent;
}());
export { StarsComponent };
//# sourceMappingURL=stars.component.js.map