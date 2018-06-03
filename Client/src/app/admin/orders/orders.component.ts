import { Component, OnInit } from '@angular/core';
import { Repository } from '../../models/repository';
import { DataService } from '../../models/data.service';
import { Order, status, OrderPartDiscrete } from '../../models/order.model';
import { forEach } from '@angular/router/src/utils/collection';

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
    cancel(id: string) {
        this.repo.changeStatus(id, 2, res => this.orders = res);
    }
    shipped(id: string) {
        this.repo.changeStatus(id, 1, res => this.orders = res);
    }
    delete(id: string) {
        this.repo.deleteOrder(id, res => this.orders = res);
    }
    //TODO:rework
    getProdInfo(arDProd: OrderPartDiscrete[]): string[] {
        let res: string[] = [];
        for (let p of arDProd)
            res.push(p.product.info.name);
        return res;
    }


}
