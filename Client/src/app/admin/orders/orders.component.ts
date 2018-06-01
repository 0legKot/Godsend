import { Component } from '@angular/core';
import { Repository } from '../../models/repository';
import { DataService } from '../../models/data.service';

@Component({
    selector: 'orders',
    templateUrl: './orders.component.html'
})
export class OrdersComponent {
  constructor(private repo: Repository, private dataService: DataService) { }

  get orders(): any[] { // Order[] {
     //this.dataService.sendRequest<any>('get', "api/order/all")
     // .subscribe(response => {
     //   return [];
     // });
    return ['1','2','3'];
    // this.repo.orders;
    }

}
