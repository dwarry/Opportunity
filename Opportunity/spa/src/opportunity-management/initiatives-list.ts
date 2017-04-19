import { autoinject, computedFrom } from 'aurelia-framework';

import { OpportunityManagementDataService } from './data-service';

import { CommonData } from '../models/reference-data';
import { IInitiativeListItem } from './models';

import { MdPagination } from 'aurelia-materialize-bridge';
import { ApplicationRoutes } from '../application-routes';
import { AppRouter } from 'aurelia-router';
import { routeNames } from '../application-routes';

const pageSize = 20;

@autoinject
export class InitiativesList {
    initiatives: Object[]
    initiativeCount: number = 0;
    pageIndex: number = 0;

    constructor(private _router: AppRouter, private _dataService: OpportunityManagementDataService, private _commonData: CommonData) {

    }

    activate() {
        return this.retrieveInitiatives(0);
    }

    retrieveInitiatives(index) {
        const getInitiatives = this._dataService.getInitiatives(pageSize, 0, true);
        const getCount = this._dataService.getInitiativeCount(true);
        // Type inferencing for Promise.all is buggy - explicitly declare types
        // to the heterogeneous array to get it to work. 
        const all: [Promise<IInitiativeListItem[]>, Promise<number>] = [getInitiatives, getCount];

        Promise.all(all).then(results => {
            this.initiatives = results[0];
            this.initiativeCount = results[1];
            this.pageIndex = index;
        });
    }

    @computedFrom("initiativeCount")
    get pageCount(): Number {
        return Math.floor(this.initiativeCount / pageSize) + 1;
    }

    onPageChanged(e) {
        this.retrieveInitiatives(e.detail);
    }

    createInitiative() {
        this._router.navigateToRoute(routeNames.initiativeDetail, { 'id': 'new' })
    }

    editInitiative(id: number) {
        this._router.navigateToRoute(routeNames.initiativeDetail, { 'id': id })
    }

    getOrgUnitName(orgUnitId: number) {
        if (orgUnitId) {
            const result = this._commonData.orgUnits.find((val, index, arr) => val.id === orgUnitId);
            return result ? result.name : "";
        }
        return "";
    }
}