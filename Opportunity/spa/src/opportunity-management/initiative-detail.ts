import { computedFrom, inject, NewInstance } from 'aurelia-framework';
import { AppRouter } from 'aurelia-router';
import { ValidationController, ValidationRules } from 'aurelia-validation';
import { MaterializeFormValidationRenderer } from 'aurelia-materialize-bridge';

import { } from '../applicationRoutes'
import { OpportunityManagementDataService } from './data-service';
import { CommonData, IOrgUnit } from '../models/reference-data';
import { IInitiativeDetail } from './models';

import { IsValidUrl } from '../custom-validation-rules';

import * as moment from 'moment';

@inject(AppRouter, NewInstance.of(ValidationController), OpportunityManagementDataService, CommonData)
export class InitiativeDetail {

    private static newInitiative: IInitiativeDetail = {
        id: null,
        name: "",
        description: "",
        link: "",
        logoUrl: "",
        startDate: moment().startOf('day').toDate(),
        endDate: moment().endOf('day').add('days', 7).toDate(),
        organizationalUnitId: 1,
        version: null
    };

    initiative: IInitiativeDetail = InitiativeDetail.newInitiative;

    constructor(
        private _router: AppRouter,
        private _validator: ValidationController,
        private _dataService: OpportunityManagementDataService,
        private _common: CommonData
    ) {

        this._validator.addRenderer(new MaterializeFormValidationRenderer());
    }

    rules =
    ValidationRules
        .ensure('name').displayName('Name').required().minLength(5).maxLength(50)
        .ensure("description").displayName('Description').minLength(5).maxLength(200)
        .ensure('link').displayName('Link').satisfiesRule(IsValidUrl).maxLength(100)
        .ensure('logoUrl').displayName('Logo').satisfiesRule(IsValidUrl).maxLength(80)
        .ensure('startDate').displayName('Start Date').required()
        .ensure('endDate').displayName('End Date').required()
        .rules;

    activate(params, routeConfig, navigationInstruction) {
        let id = Number.parseInt(params.id, 10);

        if (!isNaN(id)) {
            return this._dataService.getInitiative(id).then(init => this.initiative = init);
        }
    }

    save() {
        this._validator.validate().then(v => {
            if (v.valid) {
                return this._dataService.saveInitiative(this.initiative)
                    .then(_ => this._router.navigateBack());
            }
        });
    }

}