import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
    selector: 'godsend-home',
    templateUrl: './home.component.html'
})
export class HomeComponent {
    param = { value: 'world' };

    constructor(public translate: TranslateService) {
        translate.addLangs(['ru']);
        // this language will be used as a fallback when a translation isn't found in the current language
        translate.setDefaultLang('ru');
        //translate.setTranslation('ru', {
        //    HELLO: 'привет {{value}}'
        //});
        // the lang to use, if the lang isn't available, it will use the current loader to get them
        translate.use('ru');
    }
}
