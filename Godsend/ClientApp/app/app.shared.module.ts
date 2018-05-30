import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { ProductsComponent } from './components/products/products.component';
import { OrdersComponent } from './components/orders/orders.component';
import { ArticlesComponent } from './components/articles/articles.component';
import { CartComponent } from './components/cart/cart.component';
import { ConsultComponent } from './components/consult/consult.component';
import { LoginComponent } from './components/login/login.component';
import { SearchComponent } from './components/search/search.component';
import { StatisticsComponent } from './components/statistics/statistics.component';
import { SuppliersComponent } from './components/suppliers/suppliers.component';
import { ProductDetailComponent } from './components/products/productDetail.component';

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
