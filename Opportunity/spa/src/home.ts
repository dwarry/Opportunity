import { autoinject } from 'aurelia-framework';
import { DataService } from './dataService';
import { IUser } from './models/user'


@autoinject
export class Home {
    user: IUser = null;

    constructor(private _dataService: DataService) {

    }

    activate() {
        return this._dataService.getUser().then(u => this.user = u);
    }
}
