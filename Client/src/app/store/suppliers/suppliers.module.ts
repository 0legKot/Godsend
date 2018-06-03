import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';


import { Repository } from '../../models/repository';
import { SuppliersComponent } from './suppliers.component';
import { SupplierDetailComponent } from './suppliersDetail.component';

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
