import 'whatwg-fetch';

import { HttpClient, json } from 'aurelia-fetch-client';

import { getLogger } from 'aurelia-logging';

import { DataServiceBase } from '../dataService';

import { IIdVersion } from '../models/idVersion';

import {
    IInitiativeDetail,
    IInitiativeListItem,
    IMyOpportunity,
    INewInitiative,
    INewOpportunity,
    IOpportunityDetail
} from './models';

const log = getLogger('OpportunityManagement-DataService')

export class OpportunityManagementDataService extends DataServiceBase {

    constructor() {
        super();
    }

    getInitiatives(pageSize: number, pageIndex: number, activeOnly: boolean)
        : Promise<IInitiativeListItem[]> {

        let result = this._httpClient.fetch('initiatives/current')
            .then<any>(response => response.json())
            .then<IInitiativeListItem[]>(data => <IInitiativeListItem[]>data);

        return result;
    }

    getInitiativeCount(activeOnly: boolean): Promise<number> {
        const allOrCurrent = activeOnly ? "current" : "all";
        let result = this._httpClient.fetch(`initiatives/${allOrCurrent}/count`)
            .then<number>(response => +response.text())

        return result;
    }

    getInitiative(id: number): Promise<IInitiativeDetail> {
        let result = this._httpClient.fetch(`initiatives/${id}`)
            .then<any>(response => response.json())
            .then<IInitiativeDetail>(data => <IInitiativeDetail>data);

        return result;
    }

    saveInitiative(initiative: IInitiativeDetail): Promise<IInitiativeDetail> {
        let result = (initiative.id)
            ? this.put(`initiatives/${initiative.id}`, initiative)
            : this.post("initiatives", initiative);

        return result
            .then<any>(response => response.json())
            .then<IInitiativeDetail>(data => <IInitiativeDetail>data);
    }

    deleteInitiative(idVersion: IIdVersion): Promise<boolean> {
        let result = this.delete(`initiatives/${idVersion.id}?version=${idVersion.version}`, null)
            .then<boolean>(response => true);

        return result;
    }

    getMyOpportunities(pageIndex: number, pageSize: number): Promise<IMyOpportunity[]> {
        let result = this._httpClient.fetch(`opportunities/my?pageSize=${pageSize}&pageIndex=${pageIndex}`)
            .then<any>(response => response.json())
            .then<IMyOpportunity[]>(data => <IMyOpportunity[]>data);

        return result;
    }

    getOpportunity(id: number): Promise<IOpportunityDetail> {
        let result = this._httpClient.fetch(`/opportunities/${id}`)
            .then<any>(response => response.json())
            .then<IOpportunityDetail>(data => <IOpportunityDetail>data);

        return result;
    }

    createOpportunity(opp: INewOpportunity): Promise<number> {
        let result = this.post("opportunities", opp)
            .then(response => response.json())
            .then<number>(data => <number>data);

        return result;
    }
}