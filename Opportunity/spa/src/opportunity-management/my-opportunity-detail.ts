import { computedFrom, inject, NewInstance } from 'aurelia-framework';
import { AppRouter } from 'aurelia-router';
import { ValidationController, ValidationRules } from 'aurelia-validation';
import { MaterializeFormValidationRenderer } from 'aurelia-materialize-bridge';

//import { } from '../applicationRoutes'
import { OpportunityManagementDataService } from './data-service';
import { CommonData, IOrgUnit, ICategory } from '../models/reference-data';
import { INewOpportunity, IOpportunityDetail } from './models';

import { RuleNames } from '../custom-validation-rules';

import { DmyParser, DmmmmyyyyParser } from '../date-parsers';

import * as moment from 'moment';


@inject(AppRouter, NewInstance.of(ValidationController), OpportunityManagementDataService, CommonData)
export class MyOpportunityDetail {

    opportunityId: number | null;
    opportunity: INewOpportunity | IOpportunityDetail = {
        orgUnitId: 1,
        initiativeId: null,
        title: "",
        description: "",
        estimatedWorkload: "",
        outcomes: "",
        startDate: moment().startOf("day").toDate(),
        endDate: moment().add(14, "days").toDate(),
        vacancies: "",
        categoryId: 0
    };
    categories: ICategory[];

    parsers = [new DmyParser(), new DmmmmyyyyParser()];

    datePickerOptions = {
        closeOnSelect: true,
        closeOnClear: true,
        selectYears: 5,
        editable: false,
        showIcon: false
    };

    private _rules = ValidationRules
        .ensure('title').displayName("Title").required().maxLength(50).minLength(5)
        .ensure('description').displayName("Description").required().maxLength(1024)
        .ensure('estimatedWorkLoad').displayName("Estimated Work Load").required().maxLength(50)
        .ensure('outcomes').displayName("Outcomes").required().maxLength(50)
        .ensure('vacancies').displayName("Vacancies").required().maxLength(50)
        .ensure('startDate').displayName("Start Date").required()
        .ensure('endDate').displayName("End Date").required().satisfiesRule(RuleNames.IsAfter, "startDate")
        .rules;

    constructor(
        private _appRouter: AppRouter,
        private _validationController: ValidationController,
        private _dataService: OpportunityManagementDataService,
        private _commonData: CommonData
    ) {
        this.categories = this._commonData.categories;
    }

    @computedFrom('opportunityId')
    get operation() {
        return typeof (this.opportunityId) === "number" ? "Edit" : "Create";
    }

    activate(params) {
        this.opportunityId = typeof (params.id) === "number" ? params.id : null;

        if (this.opportunityId) {
            return this._dataService.getOpportunity(this.opportunityId)
                .then(opp => {
                    this.opportunity = opp;
                });
        }
        else {
            this.opportunity = {
                orgUnitId: 1,
                initiativeId: null,
                title: "",
                description: "",
                estimatedWorkload: "",
                outcomes: "",
                startDate: moment().startOf("day").toDate(),
                endDate: moment().add(14, "days").toDate(),
                vacancies: "",
                categoryId: 0
            };
        }
    }

    save() {
        this._dataService.createOpportunity(<INewOpportunity>this.opportunity);
    }



}