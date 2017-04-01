import { autoinject } from 'aurelia-framework';
import { AppRouter, RouterConfiguration } from 'aurelia-router';

const _routeNames = {
    error: 'error',
    home: 'home',
    manageOpportunitiesMain: 'manage-opportunities-main'
};

Object.freeze(_routeNames);

export const routeNames = _routeNames;



@autoinject
export class ApplicationRoutes {
    public configureRouter(config: RouterConfiguration) {
        config.title = "Opportunity Knocks";

        config.map([
            { route: ['', '/'], name: _routeNames.home, moduleId: 'home', nav: true, title: "Home" },
            { route: 'error', name: _routeNames.error, moduleId: 'error', nav: false, title: "Error" }
            { route: 'manage-opportunities', name: _routeNames.manageOpportunitiesMain, moduleId: 'manage-opportunities/main', nav: true, title: "Manage Opportunities" }
        ]);
    }
}