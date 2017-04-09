import { autoinject } from 'aurelia-framework';
import { AppRouter } from 'aurelia-router';

import { routeNames } from './application-routes';
import { DataService } from './dataService';
import { IUser } from './models/user'


@autoinject
export class Home {
    user: IUser = null;

    readonly viewOpportunitiesLink: string;
    readonly viewApplicationsLink: string;
    readonly manageProfileLink: string;
    readonly manageOpportunitiesLink: string;

    constructor(private _dataService: DataService, router: AppRouter) {
        this.viewOpportunitiesLink = "#";
        this.viewApplicationsLink = "#";
        this.manageProfileLink = "#";
        this.manageOpportunitiesLink = router.generate(routeNames.manageOpportunities);
    }

    activate() {
        return this._dataService.getUser().then(u => this.user = u);
    }


}
