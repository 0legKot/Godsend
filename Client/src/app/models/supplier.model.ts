import { guidZero } from "./cart.model";

export class Supplier {
    constructor(
        public info: SupplierInfo,
        public id: string = ''
    ) { }
}

export class SupplierInfo {
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

export class LocationCreate {
    constructor(
        public address: string,
    ) { }
}

export class SupplierCreate {
    public info: SupplierInfoCreate;

    constructor(
        name: string,
        address: string
    ) {
        this.info = new SupplierInfoCreate(name, address)
    }

    static FromSupplier(supplier: Supplier): SupplierCreate {
        return new this(supplier.info.name, supplier.info.location.address);
    }
}

export class SupplierInfoCreate {
    public location: LocationCreate;

    constructor(
        public name: string,
        address: string   
    ) {
        this.location = new LocationCreate(address);
    }
}


