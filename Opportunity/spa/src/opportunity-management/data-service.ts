import 'fetch';

import { HttpClient, json } from 'aurelia-fetch-client';

import { getLogger } from 'aurelia-logging';

import { DataServiceBase } from '../dataService';

import { IIdVersion } from '../models/idVersion';

import { IInitiativeDetail, IInitiativeListItem, INewInitiative } from './models';

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

    createInitiative(initiative: INewInitiative): Promise<IInitiativeDetail> {
        let result = this.post("initiatives", initiative)
            .then<IInitiativeDetail>(response => <IInitiativeDetail>response.json())

        return result;
    }

    deleteInitiative(idVersion: IIdVersion): Promise<boolean> {
        let result = this.delete(`initiatives/${idVersion.id}?version=${idVersion.version}`, null)
            .then<boolean>(response => true);

        return result;
    }
}