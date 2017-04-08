import { IUser } from './user';

export interface ICategory {
    id: number;
    name: string;
    icon: string;
}

export interface IOrgUnit {
    id: number;
    name: string;
    icon: string;
    colour: string;
    subOrgUnits: IOrgUnit[];
}

export interface IReferenceData {
    categories: ICategory[];
    orgUnits: IOrgUnit[];
}

export class CommonData {
    readonly user: IUser;
    readonly categories: ICategory[];
    readonly orgUnits: IOrgUnit[];

    constructor(user: IUser, refData: IReferenceData) {
        Object.freeze(user);
        Object.freeze(refData.categories);
        Object.freeze(refData.orgUnits);
        this.user = user;
        this.categories = refData.categories;
        this.orgUnits = refData.orgUnits;
    }
}