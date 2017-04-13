import { computedFrom, inject, NewInstance } from 'aurelia-framework';
import { AppRouter } from 'aurelia-router';
import { ValidationController, ValidationRules } from 'aurelia-validation';
import { MaterializeFormValidationRenderer } from 'aurelia-materialize-bridge';

//import { } from '../applicationRoutes'
import { OpportunityManagementDataService } from './data-service';
import { CommonData, IOrgUnit } from '../models/reference-data';
import { IInitiativeDetail } from './models';

import { RuleNames } from '../custom-validation-rules';

import { DmyParser, DmmmmyyyyParser } from '../date-parsers';

import * as moment from 'moment';

@inject(AppRouter, NewInstance.of(ValidationController), OpportunityManagementDataService, CommonData)
export class InitiativeDetail {

    private newInitiative: IInitiativeDetail = {
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

    operation: string = "";

    initiative: IInitiativeDetail = this.newInitiative;

    parsers = [new DmyParser(), new DmmmmyyyyParser()];

    datePickerOptions = {
        closeOnSelect: true,
        closeOnClear: true,
        selectYears: 2,
        editable: false,
        showIcon: false
    };


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
        .ensure('link').displayName('Link').satisfiesRule(RuleNames.IsValidUrl).maxLength(100)
        .ensure('logoUrl').displayName('Logo').satisfiesRule(RuleNames.IsValidUrl).maxLength(80)
        .ensure('startDate').displayName('Start Date').required()
        .ensure('endDate').displayName('End Date').required().satisfiesRule(RuleNames.IsAfter, "startDate")
        .rules;

    activate(params, routeConfig, navigationInstruction) {
        let id = Number.parseInt(params.id, 10);

        if (!isNaN(id)) {
            this.operation = "Edit";
            return this._dataService.getInitiative(id).then(init => this.initiative = init);
        }
        this.operation = "Create";
        this.initiative = this.newInitiative;
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