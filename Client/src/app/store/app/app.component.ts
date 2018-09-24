import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
    selector: 'godsend-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {
    constructor(translate: TranslateService) {
        translate.addLangs(['ru', 'en']);
        // this language will be used as a fallback when a translation isn't found in the current language
        translate.setDefaultLang('ru');
        // translate.setTranslation('ru', {
        //    HELLO: 'привет {{value}}'
        // });
        // the lang to use, if the lang isn't available, it will use the current loader to get them
        translate.use('ru');
    }
}
