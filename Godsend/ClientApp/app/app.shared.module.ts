import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './store/app/app.component';
import { NavMenuComponent } from './store/navmenu/navmenu.component';
import { HomeComponent } from './store/home/home.component';
import { ProductsComponent } from './store/products/products.component';
import { OrdersComponent } from './admin/orders/orders.component';
import { ArticlesComponent } from './store/articles/articles.component';
import { CartComponent } from './store/cart/cart.component';
import { ConsultComponent } from './store/consult/consult.component';
import { LoginComponent } from './store/login/login.component';
import { SearchComponent } from './store/search/search.component';
import { StatisticsComponent } from './store/statistics/statistics.component';
import { SuppliersComponent } from './store/suppliers/suppliers.component';
import { ProductDetailComponent } from './store/products/productDetail.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        ProductsComponent,
        OrdersComponent,
        HomeComponent,
        ArticlesComponent,
        CartComponent,
        ConsultComponent,
        LoginComponent,
        SearchComponent,
        StatisticsComponent,
        SuppliersComponent,
        ProductDetailComponent
    ],
    imports: [
        CommonModule,
        HttpClientModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'products', component: ProductsComponent },
            { path: 'products/:id', component: ProductDetailComponent },
            { path: 'orders', component: OrdersComponent },
            { path: 'articles', component: ArticlesComponent },
            { path: 'cart', component: CartComponent },
            { path: 'consult', component: ConsultComponent },
            { path: 'login', component: LoginComponent },
            { path: 'search', component: SearchComponent },
            { path: 'statistics', component: StatisticsComponent },
            { path: 'suppliers', component: SuppliersComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class AppModuleShared {
}
