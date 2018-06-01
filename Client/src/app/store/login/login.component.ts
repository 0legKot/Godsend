import { Component } from '@angular/core';
import { AuthenticationService } from '../../authentication/authentication.service';

@Component({
    selector: 'login',
    templateUrl: './login.component.html'
})
export class LoginComponent {
  name: string;
  pass: string;
  constructor(private auth:AuthenticationService) { }
  login() {
    this.auth.name = this.name;
    this.auth.password = this.pass;
    this.auth.callbackUrl = "/orders";
    this.auth.login();
    
  }
}
