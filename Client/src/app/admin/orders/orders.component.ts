import { Component, OnInit } from '@angular/core';
import { Repository } from '../../models/repository';
import { DataService } from '../../models/data.service';
import { Order, status } from '../../models/order.model';

@Component({
    selector: 'orders',
    templateUrl: './orders.component.html'
})
export class OrdersComponent implements OnInit {
    orders?: Order[];
    status = status;

    constructor(private repo: Repository) { }

    ngOnInit() {
        this.repo.getOrders(o => this.orders = o);
    }



    /* get orders(): any[] { // Order[] {
        //this.dataService.sendRequest<any>('get', "api/order/all")
        // .subscribe(response => {
        //   return [];
        // });
       return ['1','2','3'];
       // this.repo.orders;
       }*/

}
