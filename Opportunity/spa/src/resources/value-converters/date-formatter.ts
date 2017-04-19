import * as  moment from 'moment';

export class DateFormatterValueConverter {
  toView(value) {
    return value ? moment(value).format('DD/MM/YYYY') : "";
  }

  fromView(value) {
    if (value) {
      value = value.trim();
      return moment(value, "DD/MM/YYYY", true);
    }
    return null;
  }
}

