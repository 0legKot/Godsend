import { guidZero } from "./cart.model";
import { IEntity, IInformation } from "./entity.model";

export class Supplier implements IEntity<SupplierInfo> {

    constructor(
        public info: SupplierInfo,
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
        }
    }

    static EnsureType(sup: Supplier): Supplier {
        return new Supplier(sup.info, sup.id);
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


