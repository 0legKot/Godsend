import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
    selector: 'godsend-home',
    templateUrl: './home.component.html'
})
export class HomeComponent {
    param = { value: 'world' };

    constructor() {
        
    }
}
