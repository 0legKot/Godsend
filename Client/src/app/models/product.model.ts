export class Product {
    constructor(
        public id: string,
        public info: ProductInfo,        
    ) { }
}

export class ProductInfo {
    constructor(
        public id: string,
        public description: string,
        public name: string,
        public watches: number,
        public rating: number
    ) { }
}