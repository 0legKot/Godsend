import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { SuppliersComponent } from './suppliers.component';
import { SupplierDetailComponent } from './supplier-detail.component';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
    ],
    declarations: [
        SuppliersComponent,
        SupplierDetailComponent
    ]
})
export class SuppliersModule { }
