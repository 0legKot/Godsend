import { Component } from '@angular/core';
import { RepositoryService } from '../services/repository.service';
import { AuthenticationService } from '../authentication/authentication.service';
import { Product } from '../models/product.model';

@Component({
    templateUrl: 'admin.component.html',
    selector: 'godsend-admin'
})
export class AdminComponent {

    constructor(private repo: RepositoryService,
        public authService: AuthenticationService) {
        this.repo.getEntities < Product>('product');
        // this.repo.getOrders();
    }
}
