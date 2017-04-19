import { autoinject, Container } from 'aurelia-framework';
import { Router, RouterConfiguration } from 'aurelia-router';
import { ApplicationRoutes } from './application-routes';
import { DataService } from './dataService';
import { IUser } from './models/user';
import { CommonData, ICategory, IOrgUnit } from './models/reference-data';
@autoinject
export class App {

  router: Router;

  constructor(private _routes: ApplicationRoutes, private _container: Container, private _dataService: DataService) {

  }

  activate() {
    let userPromise = this._dataService.getUser();
    let refDataPromise = this._dataService.getReferenceData();

    return Promise.all([userPromise, refDataPromise]).then(values => {
      let [user, refData] = values;
      let common = new CommonData(user, refData);
      this._container.registerInstance(CommonData, common);
    });
  }

  configureRouter(config: RouterConfiguration, router: Router) {
    this._routes.configureRouter(config);
    this.router = router;
  }
}
