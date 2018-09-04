import { guidZero } from './cart.model';
import { IEntity, IInformation } from './entity.model';
import {  ProductInfo } from './product.model';
import { Image } from './image.model';

export class Supplier implements IEntity<SupplierInfo> {
    static EnsureType(sup: Supplier): Supplier {
        return new Supplier(sup.info, sup.productsAndPrices, sup.id, sup.images);
    }

    constructor(
        public info: SupplierInfo,
        public productsAndPrices: ProductAndPrice[],
        public id: string = '',
        public images: Image[] = []
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
    public preview?: Image;

    constructor(
        public name: string,
        public location: Location,        
        public id: string = '',
        public watches: number = 0,
        public rating: number = 0,
        public commentsCount: number = 0
    ) { }
}

export class Location {
    constructor(
        public address: string,
        public id: string = ''
    ) { }
}

export class ProductAndPrice {
    constructor(
        public productInfo: ProductInfo,
        public price: number,
        public id?: string
    ) { }
}
