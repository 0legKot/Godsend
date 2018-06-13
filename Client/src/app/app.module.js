var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
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
import { AuthenticationGuard } from './authentication/authentication.guard';
import { SearchService } from './store/search/search.service';
import { SearchInlineComponent } from './store/search/search-inline.component';
import { ProductsComponent } from './store/products/products.component';
import { ProductDetailComponent } from './store/products/product-detail.component';
import { SuppliersComponent } from './store/suppliers/suppliers.component';
import { SupplierDetailComponent } from './store/suppliers/supplier-detail.component';
import { StarsComponent } from './store/stars/stars.component';
import { ArticleDetailComponent } from './store/articles/article-detail.component';
import { ProductCardComponent } from './store/products/product-card.component';
import { SupplierCardComponent } from './store/suppliers/supplier-card.component';
import { InputOutputComponent } from './store/input-output/input-output.component';
var AppModule = /** @class */ (function () {
    function AppModule() {
    }
    AppModule = __decorate([
        NgModule({
            declarations: [
                AppComponent,
                ArticlesComponent,
                ArticleDetailComponent,
                CartComponent,
                ConsultComponent,
                HomeComponent,
                LoginComponent,
                NavMenuComponent,
                OrdersComponent,
                ProductDetailComponent,
                ProductCardComponent,
                ProductsComponent,
                SearchComponent,
                SearchInlineComponent,
                StarsComponent,
                StatisticsComponent,
                SupplierDetailComponent,
                SupplierCardComponent,
                SuppliersComponent,
                InputOutputComponent
            ],
            imports: [
                AngularFontAwesomeModule,
                BrowserModule,
                FormsModule,
                HttpClientModule,
                ReactiveFormsModule,
                RouterModule.forRoot([
                    { path: '', redirectTo: 'home', pathMatch: 'full' },
                    { path: 'home', component: HomeComponent },
                    { path: 'products', component: ProductsComponent },
                    { path: 'products/:id', component: ProductDetailComponent },
                    { path: 'suppliers', component: SuppliersComponent },
                    { path: 'suppliers/:id', component: SupplierDetailComponent },
                    { path: 'orders', component: OrdersComponent, canActivate: [AuthenticationGuard] },
                    { path: 'articles', component: ArticlesComponent },
                    { path: 'article/:id', component: ArticleDetailComponent },
                    { path: 'cart', component: CartComponent },
                    { path: 'consult', component: ConsultComponent },
                    { path: 'login', component: LoginComponent },
                    { path: 'search', component: SearchComponent },
                    { path: 'statistics', component: StatisticsComponent },
                    { path: '**', redirectTo: 'home' }
                ])
            ],
            providers: [
                SearchService,
                { provide: 'BASE_URL', useValue: 'http://localhost:56440/' }
            ],
            bootstrap: [AppComponent]
        })
    ], AppModule);
    return AppModule;
}());
export { AppModule };
//# sourceMappingURL=app.module.js.map