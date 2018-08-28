import { guidZero } from './cart.model';
import { IEntity, IInformation } from './entity.model';
import {  ProductInfo } from './product.model';

export class Supplier implements IEntity<SupplierInfo> {
    static EnsureType(sup: Supplier): Supplier {
        return new Supplier(sup.info, sup.products, sup.id);
    }

    constructor(
        public info: SupplierInfo,
        public products: ProductInfo[],
        public id: string = ''
    ) { }

    toCreateEdit() {
        return {
            id: this.id || undefined,
            info: {
                name: this.info.name,
                location: {
                    address: this.info.location.address
                }
            }
        };
    }
}

export class SupplierInfo implements IInformation {
    constructor(
        public name: string,
        public location: Location,
        
        public id: string = '',
        public watches: number = 0,
        public rating: number = 0
    ) { }
}

export class Location {
    constructor(
        public address: string,
        public id: string = ''
    ) { }
}




