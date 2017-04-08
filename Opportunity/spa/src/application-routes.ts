import { autoinject } from 'aurelia-framework';
import { AppRouter, RouterConfiguration } from 'aurelia-router';

const _routeNames = {
    error: 'error',
    home: 'home',
    initiativeDetail: 'initiative-detail',
    initiativesList: 'initiatives-list',
    manageOpportunities: 'manage-opportunities-main',

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
            { route: 'initiatives', name: routeNames.initiativesList, moduleId: "opportunity-management/initiativesList", nav: true, title: 'Initiatives' },
            { route: 'initiatives/:id', name: routeNames.initiativeDetail, moduleId: "opportunity-management/initiativeDetail", nav: false, title: 'Initiative Details' }
        ]);
    }

}