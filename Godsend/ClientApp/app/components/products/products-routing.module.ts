import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ProductsComponent } from './products.component';
import { ProductDetailComponent } from './productDetail.component';

const productsRoutes: Routes = [
    { path: 'products', component: ProductsComponent },
    { path: 'products/:id', component: ProductDetailComponent },
];

@NgModule({
    imports: [
        RouterModule.forChild(productsRoutes)
    ],
    exports: [
        RouterModule
    ]
})
export class ProductRoutingModule { }