import { autoinject } from 'aurelia-framework';
import { AppRouter } from 'aurelia-router';

import { routeNames } from '../application-routes';

import { OpportunityManagementDataService } from './data-service';

import { IMyOpportunity } from './models';

import { CommonData } from '../models/reference-data';

@autoinject
export class MyOpportunityList {
    pageIndex = 0;

    opportunities: IMyOpportunity[] = [];

    constructor(private _dataService: OpportunityManagementDataService,
        private _router: AppRouter,
        private _common: CommonData) { }

    activate(params, routeConfig, navigationInstruction) {
        this._dataService.getMyOpportunities(this.pageIndex, 20).then(x => this.opportunities = x);
    }

    getIconName(opportunity: IMyOpportunity) {
        let category = this._common.categories.find(x => x.id === opportunity.categoryId);
        return category ? category.icon : "";
    }

    getDetailsLink(opportunityId: number) {
        return this._router.generate(routeNames.myOpportunityDetail, { id: opportunityId });
    }

    getApplicationsLink(opportunityId: number) {
        return this._router.generate(routeNames.myOpportunityApplications, { opportunityId: opportunityId });
    }
}