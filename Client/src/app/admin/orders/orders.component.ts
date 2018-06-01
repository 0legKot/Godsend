import { Component } from '@angular/core';
import { Repository } from '../../models/repository';

@Component({
    selector: 'orders',
    templateUrl: './orders.component.html'
})
export class OrdersComponent {
    constructor(private repo: Repository) { }

    get orders(): any[] {//Order[] {
        return [];//this.repo.orders;
    }

}
