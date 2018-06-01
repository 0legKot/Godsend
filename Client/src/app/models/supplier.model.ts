export class Supplier {
    constructor(
        public id: string,
        public info: SupplierInfo,
    ) { }
}

export class SupplierInfo {
    constructor(
        public id: string,
        public location: Location,
        public name: string,
        public watches: number,
        public rating: number
    ) { }
}

export class Location {
    constructor(
        public address: string
    ) { }
}
