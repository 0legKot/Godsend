import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { ProductsComponent } from './products.component';
import { ProductDetailComponent } from './product-detail.component';
import { PagesComponent } from '../pages/pages.component';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
    ],
    declarations: [
        ProductsComponent,
        ProductDetailComponent,
        PagesComponent
    ]
})
export class ProductsModule { }
