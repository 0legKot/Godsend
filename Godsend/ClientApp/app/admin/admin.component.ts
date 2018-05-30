import { Component } from "@angular/core";
import { Repository } from "../models/repository";
import { AuthenticationService } from "../authentication/authentication.service";

@Component({
    templateUrl: "admin.component.html"
})
export class AdminComponent {

    constructor(private repo: Repository,
        public authService: AuthenticationService) {
        
        this.repo.getProducts();
        //this.repo.getOrders();
    }
}