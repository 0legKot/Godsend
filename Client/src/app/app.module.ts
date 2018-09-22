import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AngularFontAwesomeModule } from 'angular-font-awesome';
import {TranslateModule, TranslateLoader} from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

import {MatTreeModule, MatButtonModule, MatIconModule} from '@angular/material';
import { CdkTreeModule } from '@angular/cdk/tree';

import { AppComponent } from './store/app/app.component';
import { NavMenuComponent } from './store/navmenu/navmenu.component';
import { HomeComponent } from './store/home/home.component';
import { OrdersComponent } from './store/orders/orders.component';
import { ArticlesComponent } from './store/articles/articles.component';
import { CartComponent } from './store/cart/cart.component';
import { ConsultComponent } from './store/consult/consult.component';
import { LoginComponent } from './store/login/login.component';
import { SearchComponent } from './store/search/search.component';
import { StatisticsComponent } from './store/statistics/statistics.component';
import { AuthenticationGuard } from './services/authentication.guard';
import { SearchService } from './store/search/search.service';
import { SearchInlineComponent } from './store/search/search-inline.component';
import { ProductsModule } from './store/products/products.module';
import { SuppliersModule } from './store/suppliers/suppliers.module';
import { ProductsComponent } from './store/products/products.component';
import { ProductDetailComponent } from './store/products/product-detail.component';
import { SuppliersComponent } from './store/suppliers/suppliers.component';
import { SupplierDetailComponent } from './store/suppliers/supplier-detail.component';
import { StarsComponent } from './store/stars/stars.component';
import { ArticleDetailComponent } from './store/articles/article-detail.component';
import { ProductCardComponent } from './store/products/product-card.component';
import { RegistrationComponent } from './store/registration/registration.component';
import { SupplierCardComponent } from './store/suppliers/supplier-card.component';
import { InputOutputComponent } from './store/input-output/input-output.component';
import { PagesComponent } from './store/pages/pages.component';
import { UserComponent } from './store/user/user.component';
import { AdminComponent } from './store/admin/admin.component';
import { NotificationComponent } from './store/notification/notification.component';
import { CommentComponent } from './store/comments/comment.component';
import { CommentWrapperComponent } from './store/comments/comment-wrapper.component';
import { EntityRatingsComponent } from './store/rating/entity-ratings.component';
import { RatingsComponent } from './store/rating/ratings.component';
import { CategoryTreeComponent } from './store/products/category-tree.component';
import { ProductsComparisonComponent } from './store/products/products-comparison.component';
import { GalleryComponent } from './store/gallery/gallery.component';
import { RichtextComponent } from './store/richtext/richtext.component';

export function HttpLoaderFactory(httpClient: HttpClient) {
    return new TranslateHttpLoader(httpClient);
}

const APP_ROUTES: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', component: HomeComponent },
    { path: 'products', component: ProductsComponent },
    { path: 'products/:id', component: ProductDetailComponent },
    { path: 'products/comparison/:ids', component: ProductsComparisonComponent },
    { path: 'suppliers', component: SuppliersComponent },
    { path: 'suppliers/:id', component: SupplierDetailComponent },
    { path: 'orders', component: OrdersComponent, canActivate: [AuthenticationGuard] },
    { path: 'articles', component: ArticlesComponent },
    { path: 'articles/:id', component: ArticleDetailComponent },
    { path: 'cart', component: CartComponent },
    { path: 'consult', component: ConsultComponent },
    { path: 'login', component: LoginComponent },
    { path: 'registration', component: RegistrationComponent },
    { path: 'search', component: SearchComponent },
    { path: 'search', component: SearchComponent },
    { path: 'statistics', component: StatisticsComponent },
    { path: 'user/:id', component: UserComponent },
    { path: 'admin', component: AdminComponent },
    { path: '**', redirectTo: 'home' }
];

@NgModule({
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
        PagesComponent,
        ProductDetailComponent,
        ProductCardComponent,
        ProductsComparisonComponent,
        ProductsComponent,
        SearchComponent,
        SearchInlineComponent,
        StarsComponent,
        StatisticsComponent,
        SupplierDetailComponent,
        SupplierCardComponent,
        SuppliersComponent,
        InputOutputComponent,
        UserComponent,
        RegistrationComponent,
        AdminComponent,
        NotificationComponent,
        CommentComponent,
        CommentWrapperComponent,
        EntityRatingsComponent,
        RatingsComponent,
        CategoryTreeComponent,
        GalleryComponent,
        RichtextComponent,
    ],
    imports: [
        AngularFontAwesomeModule,
        BrowserModule,
        FormsModule,
        HttpClientModule,
        ReactiveFormsModule,
        MatTreeModule,
        CdkTreeModule,
        MatButtonModule,
        MatIconModule,
        TranslateModule.forRoot(
            {
                loader: {
                    provide: TranslateLoader,
                    useFactory: HttpLoaderFactory,
                    deps: [HttpClient]
                }
            }
        ),
        RouterModule.forRoot(APP_ROUTES),
        
        
    ],
    providers: [
        SearchService,
        { provide: 'BASE_URL', useValue: 'http://localhost:56440/' }
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
