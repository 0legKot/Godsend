import { Supplier } from './supplier.model';
import { IEntity, IInformation } from './entity.model';

export class Product implements IEntity<ProductInfo> {
    static EnsureType(product: Product): Product {
        return new this(product.id, product.info);
    }

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

export class Category {
    constructor(
        public id: string,
        public name: string,
        public baseCategory?: Category
    ) { }
}

export class CatsWithSubs {
    constructor(
        public cat: Category,
        public subs: CatsWithSubs[]
    ) { }
}

export class FilterInfo {
    decimalProps?: DecimalPropertyInfo[];
    intProps?: IntPropertyInfo[];
    stringProps?: StringPropertyInfo[];
}

export class DecimalPropertyInfo {
    constructor(
        public propId: string,
        public left: number,
        public right: number
    ) { }
}

export class IntPropertyInfo {
    constructor(
        public propId: string,
        public left: number,
        public right: number
    ) { }
}

export class StringPropertyInfo {
    constructor(
        public propId: string,
        public part: string
    ) { }
}

export class FilterInfoView {
    decimalProps?: DecimalPropertyInfoView[];
    intProps?: IntPropertyInfoView[];
    stringProps?: StringPropertyInfoView[];
}

export class DecimalPropertyInfoView {
    left: number = 0;
    right: number = 0;

    constructor(
        public propId: string,
        public name: string
    ) { }
}

export class IntPropertyInfoView {
    left: number = 0;
    right: number = 0;

    constructor(
        public propId: string,
        public name: string
    ) { }
}

export class StringPropertyInfoView {
    part: string = '';

    constructor(
        public propId: string,
        public name: string
    ) { }
}

export class Property {
    id: string = '';
    name: string = '';
    type: number = -1;
}

export const propertyType: allowedPropertyTypes[] = [
    'int', //0
    'string', //1
    'decimal' //2
]

export type allowedPropertyTypes = 'int' | 'string' | 'decimal';
