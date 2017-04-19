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

export interface IInitiativeSummary {
    id: number;
    name: string;
    link: string | null;
    logoUrl: string | null;
}

export interface IMyOpportunity {
    id: number;
    title: string;
    description: string;
    startDate: Date;
    endDate: Date;
    categoryId: number;
    tags: string[];
    applicationCount: number;
    successfulCount: number;
}

export interface IOpportunityDetail {
    id: number;
    orgUnitId: number;
    initiative: IInitiativeSummary;
    title: string;
    description: string;
    estimatedWorkload: string;
    outcomes: string;
    startDate: Date;
    endDate: Date;
    vacancies: string;
    categoryId: number;
    tags: string[];
    updatedAt: Date;
    updatedBy: string;
    version: string;
}

export interface INewOpportunity {

    orgUnitId: number;
    initiativeId: number | null;
    title: string;
    description: string;
    estimatedWorkload: string;
    outcomes: string;
    startDate: Date;
    endDate: Date;
    vacancies: string;
    categoryId: number;
    tags: string[];
}

export interface IOpenOpportunity {
    id: number;
    title: string;
    description: string;
    estimatedWorkload: string;
    outcomes: string;
    startDate: Date;
    endDate: Date;
    vacancies: string;
    categoryId: number;
    ownerId: number;
    tags: string[];
    isApplicationSubmitted: boolean
}
