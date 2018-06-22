import { Component, OnInit } from '@angular/core';
import { RepositoryService } from '../../services/repository.service';
import { DataService } from '../../services/data.service';
import { Order, orderStatus, OrderPartProducts } from '../../models/order.model';
import { forEach } from '@angular/router/src/utils/collection';

@Component({
    selector: 'godsend-orders',
    templateUrl: './orders.component.html',
    styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {
    orders?: Order[];
    status = orderStatus;
    page: number = 1;
    rpp: number = 10;
    constructor(private repo: RepositoryService) { }

    ngOnInit() {
        this.repo.getEntities < Order>('order',this.page,this.rpp, o => this.orders = o);
    }
    cancel(id: string) {
        this.repo.changeOrderStatus(id, 2, this.page, this.rpp, res => this.orders = res);
    }
    shipped(id: string) {
        this.repo.changeOrderStatus(id, 1, this.page, this.rpp, res => this.orders = res);
    }
    delete(id: string) {
        this.repo.deleteOrder(id, this.page, this.rpp, res => this.orders = res);
    }
    // TODO:rework
    getProdInfo(arDProd: OrderPartProducts[]): string[] {
        const res: string[] = [];
        for (const p of arDProd) {
            res.push(p.product.info.name);
        }
        return res;
    }


}
