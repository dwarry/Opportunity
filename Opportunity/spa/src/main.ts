import { Aurelia } from 'aurelia-framework'
import environment from './environment';

//Configure Bluebird Promises.
(<any>Promise).config({
  warnings: {
    wForgottenReturn: false
  }
});

export function configure(aurelia: Aurelia) {
  aurelia.use
    .standardConfiguration()
    .feature('resources');

  if (environment.debug) {
    aurelia.use.developmentLogging();
  }

  if (environment.testing) {
    aurelia.use.plugin('aurelia-testing');
  }
  aurelia.use.plugin('aurelia-materialize-bridge', b => b.useAll());

  aurelia.use.globalResources("./resources/elements/icons/icon.html");

  aurelia.start().then(() => aurelia.setRoot());
}