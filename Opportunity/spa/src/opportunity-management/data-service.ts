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
            .then<IInitiativeListItem[]>(data => {
                log.info("Retrieved Initiatives");
                return <IInitiativeListItem[]>data;
            })
            .catch<IInitiativeListItem[]>(reason => {
                log.error("Could not retrieve initiatives.");
                log.debug(reason);
                return [];
            });

        return result;
    }

    getInitiativeCount(activeOnly: boolean): Promise<number> {
        const allOrCurrent = activeOnly ? "current" : "all";
        let result = this._httpClient.fetch(`initiatives/${allOrCurrent}/count`)
            .then<number>(response => +response.text())
            .catch(reason => {
                log.debug(reason);
                log.error("Could not retrieve count of Initiatives.");
                return 0;
            });

        return result;
    }

    getInitiative(id: number): Promise<IInitiativeDetail> {
        let result = this._httpClient.fetch(`initiatives/${id}`)
            .then<any>(response => response.json())
            .then<IInitiativeDetail>(data => {
                log.info("Retrieved Initiative");
                return <IInitiativeDetail>data;
            }).catch(reason => {
                log.debug(reason);
                log.error("Could not retrieve Initiative.");
                return null;
            });

        return result;
    }

    saveInitiative(initiative: IInitiativeDetail): Promise<IInitiativeDetail> {
        let result = (initiative.id)
            ? this.put(`initiatives/${initiative.id}`, initiative)
            : this.post("initiatives", initiative);

        return result
            .then<any>(response => response.json())
            .then<IInitiativeDetail>(data => <IInitiativeDetail>data)
            .catch(reason => {
                log.debug(reason);
                return null;
            });
    }

    deleteInitiative(idVersion: IIdVersion): Promise<boolean> {
        let result = this.delete(`initiatives/${idVersion.id}?version=${idVersion.version}`, null)
            .then<boolean>(response => {
                log.info("Deleted Initiative");
                return true;
            })
            .catch(reason => {
                log.debug(reason);
                log.error("Could not delete the Initiative.");
                return false;
            });

        return result;
    }

    getMyOpportunities(pageIndex: number, pageSize: number): Promise<IMyOpportunity[]> {
        let result = this._httpClient.fetch(`opportunities/my?pageSize=${pageSize}&pageIndex=${pageIndex}`)
            .then<any>(response => response.json())
            .then<IMyOpportunity[]>(data => {
                log.info("Retrieved Opportunities");
                return <IMyOpportunity[]>data;
            })
            .catch(reason => {
                log.debug(reason);
                log.error("Could not retrieve the Opportunities.");
                return [];
            });
        return result;
    }

    getOpportunity(id: number): Promise<IOpportunityDetail> {
        let result = this._httpClient.fetch(`/opportunities/${id}`)
            .then<any>(response => response.json())
            .then<IOpportunityDetail>(data => {
                log.info("Retrieved Opportunity")
                return <IOpportunityDetail>data;
            })
            .catch(reason => {
                log.debug(reason);
                log.error("Could not retrieve the Opportunity.");
                return <IOpportunityDetail>null;
            });
        return result;
    }

    createOpportunity(opp: INewOpportunity): Promise<number> {
        let result = this.post("opportunities", opp)
            .then(response => response.json())
            .then<number>(data => {
                log.info("Created new Opportunity")
                return <number>data;
            })
            .catch(reason => {
                log.debug(reason);
                log.error("Could not create the Opportunity.");
                return null;
            });

        return result;
    }

    updateOpportunity(opp: IOpportunityDetail): Promise<boolean> {
        let result = this.put("opportunities", opp)
            .then(response => {
                log.info("Updated Opportunity");
                return true;
            })
            .catch(reason => {
                log.debug(reason);
                log.error("Could not update the Opportunity");
                return false;
            });

        return result;
    }

    deleteOpportunity(idVersion: IIdVersion): Promise<boolean> {
        let result = this.delete(`opportunities/${idVersion.id}?version=${idVersion.version}`, null)
            .then(response => {
                log.info("Deleted Opportunity");
                return true;
            })
            .catch(reason => {
                log.debug(reason);
                log.error("Could not delete Opportunity");
                return false;
            });

        return result;
    }
}