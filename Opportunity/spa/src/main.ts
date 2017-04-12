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
    .globalResources([
      "./resources/attributes/md-datepicker-label",
      "./resources/elements/date-field",
      "./resources/elements/icons/icon.html",
      "./resources/value-converters/date-formatter"]);

  aurelia.start().then(() => {
    addCustomValidationRules();
    aurelia.setRoot();
  }
  );
}
