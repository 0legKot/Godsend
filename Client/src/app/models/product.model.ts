import { Supplier } from './supplier.model';
import { IEntity, IInformation} from './entity.model'

export class Product implements IEntity<ProductInfo> {
    constructor(
        public id: string,
        public info: ProductInfo,
    ) { }

    toCreateEdit() {
        return {
            id: this.id || undefined,
            info: {
                name: this.info.name,
                description: this.info.description
            }
        };
    }

    static EnsureType(product: Product): Product {
        return new this(product.id, product.info);
    }
}

export class ProductInfo implements IInformation {
    constructor(
        public id: string,
        public description: string,
        public name: string,
        public watches: number,
        public rating: number
    ) { }
}

export class ProductWithSuppliers {
    constructor(
        public product: Product,
        public suppliers: SupplierAndPrice[]
    ) { }
}

export class SupplierAndPrice {
    constructor(
        public supplier: Supplier,
        public price: number
    ) { }
}
