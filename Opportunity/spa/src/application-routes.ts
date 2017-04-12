import { autoinject } from 'aurelia-framework';
import { AppRouter, RouterConfiguration } from 'aurelia-router';

const _routeNames = {
    error: 'error',
    home: 'home',
    initiativeDetail: 'initiative-detail',
    initiativesList: 'initiatives-list',
    manageOpportunities: 'manage-opportunities-main',
    myOpportunities: 'my-opportunities',
    myOpportunityDetail: 'my-opportunity',
    myOpportunityApplications: "my-opportunity-applications",
    myOpportunityAppDetail: 'my-opportunity-app-detail'
};

Object.freeze(_routeNames);

export const routeNames = _routeNames;

@autoinject
export class ApplicationRoutes {
    public configureRouter(config: RouterConfiguration) {
        config.title = "Opportunity Knocks";

        config.map([
            { route: ['', '/'], name: _routeNames.home, moduleId: 'home', nav: true, title: "Home" },
            { route: 'error', name: _routeNames.error, moduleId: 'error', nav: false, title: "Error" },
            { route: 'manage-opportunities', name: _routeNames.manageOpportunities, moduleId: 'opportunity-management/main', nav: true, title: "Manage Opportunities" },
            { route: 'initiatives', name: routeNames.initiativesList, moduleId: "opportunity-management/initiatives-list", nav: true, title: 'Initiatives' },
            { route: 'initiatives/:id', name: routeNames.initiativeDetail, moduleId: "opportunity-management/initiative-detail", nav: false, title: 'Initiative Details' },
            { route: 'my-opportunities', name: routeNames.myOpportunities, moduleId: "opportunity-management/my-opportunity-list", nav: true, title: "My Opportunities" },
            { route: 'my-opportunity/:id', name: routeNames.myOpportunityDetail, moduleId: "opportunity-management/my-opportunity-detail", nav: false, title: "Opportunity Detail" },
            { route: 'my-opportunity/:id/applications', name: routeNames.myOpportunityApplications, moduleId: "opportunity-management/my-opportunity-application-list", nav: false, title: "Applications" },
            { route: 'my-opportunity-application/:id', name: routeNames.myOpportunityAppDetail, moduleId: "opportunity-management/my-opportunity-application-detail", nav: false, title: "Application Detail" }
        ]);
    }

}