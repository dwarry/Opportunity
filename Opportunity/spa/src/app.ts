import { autoinject } from 'aurelia-framework';
import { Router, RouterConfiguration } from 'aurelia-router';
import { ApplicationRoutes } from './application-routes';

@autoinject
export class App {

  router: Router;

  constructor(private _routes: ApplicationRoutes) {
  }

  configureRouter(config: RouterConfiguration, router: Router) {
    this._routes.configureRouter(config);
    this.router = router;
  }
}
