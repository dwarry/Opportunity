import { Aurelia } from 'aurelia-framework';
import environment from './environment';
import { addCustomValidationRules } from './custom-validation-rules';


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
  aurelia.use.plugin('aurelia-materialize-bridge', b => b.useAll())
    .plugin('aurelia-validation')
    .globalResources("./resources/elements/icons/icon.html");

  aurelia.start().then(() => {
    addCustomValidationRules();
    aurelia.setRoot();
  }
  );
}
