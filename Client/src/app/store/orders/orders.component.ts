import { Component, OnInit } from '@angular/core';
import { RepositoryService } from '../../services/repository.service';
import { Order, orderStatus, OrderPartProducts } from '../../models/order.model';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
    selector: 'godsend-orders',
    templateUrl: './orders.component.html',
    styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {
    orders?: Order[];
    status = orderStatus;
    page = 1;
    rpp = 10;
    constructor(private repo: RepositoryService, private auth: AuthenticationService) { }

    get pagesCount(): number {
        return Math.ceil(this.repo.ordersCount / this.rpp);
    }

    get canChangeStatus(): boolean {
        return Boolean(this.auth.roles.find(x => x == 'Administrator' || x == 'Moderator'));
    }

    onPageChanged(page: number) {
        this.page = page;
        this.getOrders();
    }

    ngOnInit() {
        this.getOrders();
    }

    getOrders() {
        this.repo.getEntities<Order>('order', this.page, this.rpp, o => this.orders = o);

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
            res.push(p.product.name);
        }
        return res;
    }


}
