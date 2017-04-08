import { computedFrom } from 'aurelia-framework';

export class OpportunityDetail {

    opportunityId: number | null;

    @computedFrom('opportunityId')
    get operation() {
        return typeof (this.opportunityId) === "number" ? "Edit" : "Create";
    }

    constructor() {

    }

    activate(params) {
        this.opportunityId = typeof (params.id) === "number" ? params.id : null;
    }

    refresh() {
        if (this.opportunityId) {

        }
    }


}