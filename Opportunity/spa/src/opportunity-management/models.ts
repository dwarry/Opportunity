export interface IInitiativeListItem {
    id: number;
    name: string;
    description: string;
    startDate: Date;
    endDate: Date;
    organizationalUnitId: number;
    version: string;
}

export interface IInitiativeDetail {
    id: number;
    name: string;
    description: string;
    link: string;
    logoUrl: string;
    startDate: Date;
    endDate: Date;
    organizationalUnitId: number;
    version: string;
}

export interface INewInitiative {
    name: string;
    description: string;
    link: string;
    logoUrl: string;
    startDate: Date;
    endDate: Date;
    organizationalUnitId: number;
}

