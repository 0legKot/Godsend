import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { ProductsComponent } from './products.component';
import { ProductDetailComponent } from './productDetail.component';

import { ProductRoutingModule } from './products-routing.module';
import { Repository } from '../../models/repository';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ProductRoutingModule
    ],
    declarations: [
        ProductsComponent,
        ProductDetailComponent
    ],
    providers: [Repository]
})
export class ProductsModule { }
