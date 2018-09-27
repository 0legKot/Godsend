import { Component, OnInit, OnDestroy } from '@angular/core';
import { Message } from './facebook-auth.component';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
    selector: 'godsend-facebook-login',
    templateUrl: './facebook-login.component.html'
})
export class FacebookLoginComponent implements OnInit, OnDestroy {

    private authWindow?: Window | null;
    failed: boolean = true;
    error?: string;
    errorDescription?: string;
    isRequesting: boolean = false;

    /**
     * Add/remove event listener methods seamingly need the same object to work
     * Passing handleMessage crashes because 'this' means something else when it is called
     * Calling handleMessage.bind(this) gives another reference and is not removed
     * */
    readonly magic = (x: any) => this.handleMessage(x);

    constructor(private authService: AuthenticationService) {
        
    }

    ngOnInit() {
        if (window.addEventListener) {
            window.addEventListener('message', this.magic);
        } else {
            (<any>window).attachEvent('onmessage', this.magic);
        } 
    }

    ngOnDestroy() {
        if (window.removeEventListener) {
            window.removeEventListener('message', this.magic)
        } else {
            (<any>window).detachEvent('onmessage', this.magic);
        }
    }

    launchFbLogin() {
        // launch facebook login dialog
        this.authWindow = window.open('https://www.facebook.com/v3.1/dialog/oauth?&response_type=token&client_id=1961572050808686&display=popup&redirect_uri=https://localhost:4200/facebook-auth&scope=email', 'godsend-facebook', 'width=600,height=400');
    }

    handleMessage(event: Event) {
        const message = event as MessageEvent;
        // Only trust messages from the below origin.
        if (message.origin !== "https://localhost:4200") return;

        if (this.authWindow) {
            this.authWindow.close();
        }

        const result = message.data;
        if (this.isMessage(result)) {
            console.log('is message');
            if (!result.status) {
                this.failed = true;
                this.error = result.error;
                this.errorDescription = result.errorDescription;
            }
            else {
                this.failed = false;
                this.isRequesting = true;
                if (result.accessToken) {
                    this.authService.facebookLogin(result.accessToken);
                } else {
                    console.log('error: no access token');
                }
            }
        }
    }

    isMessage(msg: any): msg is Message {
        return (msg.status == true && (typeof msg.accessToken == 'string')) ||
            (msg.status == false && (typeof msg.error == 'string') && (typeof msg.errorDescription == 'string'));
    }
}
