import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { ProductsComponent } from './components/products/products.component';
import { OrdersComponent } from './components/orders/orders.component';
import { ProductDetailComponent } from './components/products/productDetail.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        ProductsComponent,
        OrdersComponent,
        ProductDetailComponent,
        HomeComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'products', component: ProductsComponent },
            { path: 'products/:id*', component: ProductDetailComponent },
            { path: 'orders', component: OrdersComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class AppModuleShared {
}
