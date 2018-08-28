import { Supplier, SupplierInfo } from './supplier.model';
import { IEntity, IInformation } from './entity.model';
import { guidZero } from './cart.model';

export class Product implements IEntity<ProductInfo> {
    static EnsureType(product: Product): Product {
        return new this(product.id, product.info, product.jsonCategory ? product.jsonCategory.id : guidZero, product.jsonCategory, product.stringProps, product.intProps, product.decimalProps, product.suppliersAndPrices);
    }

    constructor(
        public id: string,
        public info: ProductInfo,
        public categoryId: string,
        public jsonCategory?: Category,
        public stringProps?: EAV<string>[],
        public intProps?: EAV<number>[],
        public decimalProps?: EAV<number>[],
        public suppliersAndPrices?: SupplierAndPrice[]
    ) { }

    toCreateEdit() {
        return {
            id: this.id || undefined,
            info: {
                name: this.info.name,
                description: this.info.description
            },
            categoryId: this.categoryId,
            intProps: this.intProps,
            stringProps: this.stringProps,
            decimalProps: this.decimalProps,
            suppliersAndPrices: this.suppliersAndPrices
        };
    }
}

export class ProductInfo implements IInformation {
    constructor(
        public id: string,
        public description: string,
        public state: number,
        public name: string,
        public watches: number,
        public rating: number
    ) { }
}

export class SupplierAndPrice {
    constructor(
        public supplierInfo: SupplierInfo,
        public price: number,
        public id?: string
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

export class EAV<T> {
    public id?: string;

    constructor(
        public productId: string,
        public property: Property,
        public value: T
    ) { }
}

export class FilterInfo {
    decimalProps?: DecimalPropertyInfo[];
    intProps?: IntPropertyInfo[];
    stringProps?: StringPropertyInfo[];
    orderBy = 0;
}

export class DecimalPropertyInfo {
    constructor(
        public propId: string,
        public left: number | null,
        public right: number | null
    ) { }
}

export class IntPropertyInfo {
    constructor(
        public propId: string,
        public left: number | null,
        public right: number | null
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
    orderBy = 0;
    sortAscending = true;
}

export class DecimalPropertyInfoView {
    left: number | null = null;
    right: number | null = null;

    constructor(
        public propId: string,
        public name: string
    ) { }
}

export class IntPropertyInfoView {
    left: number | null = null;
    right: number | null = null;

    constructor(
        public propId: string,
        public name: string
    ) { }
}

export class StringPropertyInfoView {
    part = '';

    constructor(
        public propId: string,
        public name: string
    ) { }
}

export class Property {
    id = '';
    name = '';
    type = -1;
}

export class ProductFilterInfo {
    public decimalProps?: DecimalPropertyInfo[];
    public stringProps?: StringPropertyInfo[];
    public intProps?: IntPropertyInfo[];
    public sortingPropertyId?: string;
    public orderBy = 0;
    public sortAscending = false;
    public categoryId?: string;
    public searchTerm?: string;

    constructor(
        public quantity: number,
        public page: number
    ) { }
}

export interface ProductInfosAndCount {
    infos: ProductInfo[];

    count: number;
}

export const propertyType: allowedPropertyTypes[] = [
    'int', // 0
    'string', // 1
    'decimal' // 2
];

export type allowedPropertyTypes = 'int' | 'string' | 'decimal';

export const orderBy: allowedOrderBy[] = [
    'name',
    'rating',
    'watches'
];

export type allowedOrderBy = 'name' | 'rating' | 'watches';


