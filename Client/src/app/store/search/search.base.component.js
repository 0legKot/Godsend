import { FormControl } from '@angular/forms';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
var SearchBaseComponent = /** @class */ (function () {
    function SearchBaseComponent() {
        this.searchField = new FormControl();
    }
    SearchBaseComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.searchField = new FormControl();
        this.searchField.valueChanges
            .pipe(debounceTime(400))
            .pipe(distinctUntilChanged())
            .subscribe(function (term) { return _this.doSearch(term); });
    };
    return SearchBaseComponent;
}());
export { SearchBaseComponent };
//# sourceMappingURL=search.base.component.js.map