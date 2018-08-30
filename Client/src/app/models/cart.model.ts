import { ProductInfo } from './product.model';
import { SupplierInfo } from './supplier.model';

export const guidZero = '00000000-0000-0000-0000-000000000000';

export class Cart {
    constructor(
        public discreteItems: OrderPartDiscreteSend[],
    ) { }
}

export class CartView {
    constructor(
        public discreteItems: OrderPartDiscreteView[],
    ) { }
}

export class OrderPartSend {
    constructor(
        public productId: string,
        public supplierId: string
    ) { }
}

export class OrderPartDiscreteSend extends OrderPartSend {
    constructor(
        public quantity: number,
        public productId: string,
        public supplierId: string
    ) {
        super(productId, supplierId);
    }
}

// export class OrderPartWeightedSend extends OrderPartSend {
//    constructor(
//        public weight: number,
//        public productId: string,
//        public supplierId: string
//    ) {
//        super(productId, supplierId);
//    }
// }

// export function isDiscrete(part: OrderPartDiscreteView | OrderPartWeightedView): part is OrderPartDiscreteView {
//    return ((<OrderPartDiscreteView>part).quantity !== undefined);
// }

export class OrderPartView {
    constructor(
        public productInfo: ProductInfo,
        public supplierInfo: SupplierInfo,
        public price: number
    ) { }
}

export class OrderPartDiscreteView extends OrderPartView {
    constructor(
        public productInfo: ProductInfo,
        public supplierInfo: SupplierInfo,
        public price: number,
        public quantity: number
    ) { super(productInfo, supplierInfo, price); }
}

// export class OrderPartWeightedView extends OrderPartView {
//    constructor(
//        public product: Product,
//        public supplier: Supplier,
//        public price: number,
//        public weight: number
//    ) { super(product, supplier, price); }
// }
