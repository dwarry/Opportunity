import { autoinject } from 'aurelia-framework';
import { AppRouter } from 'aurelia-router';
import { routeNames } from '../application-routes';

@autoinject
export class Main {

    readonly myOpportunitiesLink: string;
    readonly createOpportunityLink: string;
    readonly initiativesLink: string;
    readonly createInitiativeLink: string;

    constructor(router: AppRouter) {

        this.myOpportunitiesLink = '#';
        this.createOpportunityLink = '#';
        this.initiativesLink = router.generate(routeNames.initiativesList);
        this.createInitiativeLink = router.generate(routeNames.initiativeDetail, { 'id': 'new' });
    }

    activate(params: any, routeConfig, navigationInstruction) {

    }
}