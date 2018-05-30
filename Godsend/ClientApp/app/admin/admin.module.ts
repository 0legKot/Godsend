import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser"
import { RouterModule } from "@angular/router";
import { FormsModule } from "@angular/forms";
import { AdminComponent } from "./admin.component";
import { OrdersComponent } from "./orders/orders.component";

@NgModule({
    imports: [BrowserModule, RouterModule, FormsModule],
    declarations: [AdminComponent, OrdersComponent]
})
export class AdminModule { }