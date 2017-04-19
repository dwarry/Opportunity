import { Aurelia, LogManager } from 'aurelia-framework';
import { addAppender, getLogger, setLevel } from 'aurelia-logging';

import environment from './environment';
import { addCustomValidationRules } from './custom-validation-rules';
import { MdToastServiceLogAppender } from './md-toast-log-appender';
import 'jquery';

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

  setLevel(environment.loglevel);

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

    addAppender(aurelia.container.get(MdToastServiceLogAppender));

    getLogger('main').debug('Aurelia initialized');

    aurelia.setRoot();
  }
  );
}
