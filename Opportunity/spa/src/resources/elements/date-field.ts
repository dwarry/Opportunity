import { bindable } from 'aurelia-framework';

import { DmyParser } from '../../date-parsers';

export class DateField {
  @bindable name: string = "Date";

  @bindable value: Date | null = null;

  @bindable wrapperClass: string = "";

  @bindable placeholder: string = "";

  parsers = [new DmyParser()];

  advancedOptions = {
    closeOnSelect: true,
    closeOnClear: true,
    selectYears: 5,
    editable: true,
    showIcon: true
  };

  valueChanged(newValue, oldValue) {

  }
}

