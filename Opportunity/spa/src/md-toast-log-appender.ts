import { autoinject } from 'aurelia-framework';
import { Appender, Logger } from 'aurelia-logging';

import { MdToastService } from 'aurelia-materialize-bridge';

@autoinject
export class MdToastServiceLogAppender implements Appender {

    constructor(private _toaster: MdToastService) { }

    debug(logger: Logger, ...rest: any[]): void {
        // debug messages don't display toasts
    }
    info(logger: Logger, ...rest: any[]): void {
        this._toaster.show(rest[0], 5000, 'light-green')
    }
    warn(logger: Logger, ...rest: any[]): void {
        this._toaster.show(rest[0], 5000, 'amber')
    }
    error(logger: Logger, ...rest: any[]): void {
        this._toaster.show(rest[0], 5000, 'red amber-text text-lighten-2')
    }

}