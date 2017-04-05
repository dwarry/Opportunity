import { autoinject, computedFrom } from 'aurelia-framework';

import { OpportunityManagementDataService } from './data-service';

import { IInitiativeListItem } from './models';

const pageSize = 20;

@autoinject
export class InitiativesList {
    initiatives: Object[]
    initiativeCount: number = 0;
    pageIndex: number = 0;

    constructor(private _dataService: OpportunityManagementDataService) {

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
        });
    }

    @computedFrom("initiativeCount")
    get pageCount(): Number {
        return Math.floor(this.initiativeCount / pageSize) + 1;
    }


}