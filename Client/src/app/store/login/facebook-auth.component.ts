import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'godsend-facebook-auth',
    templateUrl: './facebook-auth.component.html'
})
export class FacebookAuthComponent implements OnInit {

    constructor(private route: ActivatedRoute) { }

    ngOnInit() {
        let params = this.route.snapshot.fragment.split('&').map(s => s.split('='));

        const tmp = params.find(s => s[0] == 'access_token');
        const accessToken = tmp ? tmp[1] : undefined;

        const message: Message = accessToken ? {
            status: true,
            accessToken: accessToken
        } : {
            status: false,
                error: params.find(s => s[0] == 'error')![1],
                errorDescription: params.find(s => s[0] == 'error_description')![1]
            }

        console.log('on init');
        console.dir(message);
        console.dir(window.opener);

        window.opener.postMessage(message, "https://localhost:4200");
    }

}

export interface Message {
    status: boolean;
    accessToken?: string;
    error?: string;
    errorDescription?: string;
}
