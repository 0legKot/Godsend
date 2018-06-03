import { Component, OnInit } from '@angular/core';
import { Repository } from '../../models/repository';
import { DataService } from '../../models/data.service';
import { Order, orderStatus, OrderPartDiscrete } from '../../models/order.model';
import { forEach } from '@angular/router/src/utils/collection';

@Component({
    selector: 'godsend-orders',
    templateUrl: './orders.component.html'
})
export class OrdersComponent implements OnInit {
    orders?: Order[];
    status = orderStatus;

    constructor(private repo: Repository) { }

    ngOnInit() {
        this.repo.getEntities < Order>('order', o => this.orders = o);
    }
    cancel(id: string) {
        this.repo.changeOrderStatus(id, 2, res => this.orders = res);
    }
    shipped(id: string) {
        this.repo.changeOrderStatus(id, 1, res => this.orders = res);
    }
    delete(id: string) {
        this.repo.deleteOrder(id, res => this.orders = res);
    }
    // TODO:rework
    getProdInfo(arDProd: OrderPartDiscrete[]): string[] {
        const res: string[] = [];
        for (const p of arDProd) {
            res.push((<any>p).product.info.name);
        }
        return res;
    }


}
