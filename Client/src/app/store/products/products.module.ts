import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { ProductsComponent } from './products.component';
import { ProductDetailComponent } from './product-detail.component';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
    ],
    declarations: [
        ProductsComponent,
        ProductDetailComponent
    ]
})
export class ProductsModule { }
