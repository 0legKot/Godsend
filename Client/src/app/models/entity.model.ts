export interface IEntity<T extends IInformation> {
    id: string;
    info: T;

    toCreateEdit(): any;
}

export interface IInformation {
    id: string,
    name: string,
    watches: number,
    rating: number
}