import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AngularFontAwesomeModule } from 'angular-font-awesome';

import { AppComponent } from './store/app/app.component';
import { NavMenuComponent } from './store/navmenu/navmenu.component';
import { HomeComponent } from './store/home/home.component';
import { OrdersComponent } from './admin/orders/orders.component';
import { ArticlesComponent } from './store/articles/articles.component';
import { CartComponent } from './store/cart/cart.component';
import { ConsultComponent } from './store/consult/consult.component';
import { LoginComponent } from './store/login/login.component';
import { SearchComponent } from './store/search/search.component';
import { StatisticsComponent } from './store/statistics/statistics.component';
import { Repository } from './models/repository';
import { AuthenticationGuard } from './authentication/authentication.guard';
import { AuthenticationService } from './authentication/authentication.service';
import { DataService } from './models/data.service';
import { SearchService } from './store/search/search.service';
import { SearchInlineComponent } from './store/search/search-inline.component';
import { ProductsModule } from './store/products/products.module';
import { SuppliersModule } from './store/suppliers/suppliers.module';
import { ProductsComponent } from './store/products/products.component';
import { ProductDetailComponent } from './store/products/product-detail.component';
import { SuppliersComponent } from './store/suppliers/suppliers.component';
import { SupplierDetailComponent } from './store/suppliers/supplier-detail.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        OrdersComponent,
        HomeComponent,
        ArticlesComponent,
        CartComponent,
        ConsultComponent,
        LoginComponent,
        SearchComponent,
        StatisticsComponent,
        SearchInlineComponent,
        ProductsComponent,
        ProductDetailComponent,
        SuppliersComponent,
        SupplierDetailComponent
    ],
    imports: [
        CommonModule,
        HttpClientModule,
        FormsModule,
        ReactiveFormsModule,
        BrowserModule,
        AngularFontAwesomeModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'products', component: ProductsComponent },
            { path: 'products/:id', component: ProductDetailComponent },
            { path: 'suppliers', component: SuppliersComponent },
            { path: 'suppliers/:id', component: SupplierDetailComponent },
            { path: 'orders', component: OrdersComponent, canActivate: [AuthenticationGuard] },
            { path: 'articles', component: ArticlesComponent },
            { path: 'cart', component: CartComponent },
            { path: 'consult', component: ConsultComponent },
            { path: 'login', component: LoginComponent },
            { path: 'search', component: SearchComponent },
            { path: 'statistics', component: StatisticsComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [
        Repository,
        AuthenticationGuard,
        AuthenticationService,
        DataService,
        SearchService,
        { provide: 'BASE_URL', useValue: 'http://localhost:56440/' }
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
