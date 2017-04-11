import { ValidationRules } from 'aurelia-validation';

import { HttpClient } from 'aurelia-fetch-client';


class UrlTester {

    private _httpClient = new HttpClient();

    testUrlExists(url: string, allowUnauthorised: boolean): boolean | Promise<boolean> {
        if (url === null || url === undefined) { return true; }
        return this._httpClient.fetch(url)
            .then<boolean>(response => (response.status == 200 || (response.status == 401 && allowUnauthorised)),
            reason => false);
    }
}

let ruleNames = {
    IsValidUrl: "isValidUrl",
    IsAfter: "isAfter",
    IsBefore: "isBefore",
    IsOnOrAfter: "isOnOrAfter",
    IsOnOrBefore: "isOnOrBefore"
};

Object.freeze(ruleNames);

export const RuleNames = ruleNames;

export function addCustomValidationRules() {

    ValidationRules.customRule(ruleNames.IsAfter, (value, obj, otherName) => {
        if (value === null || value === undefined) { return true; }
        const other = obj[otherName];
        if (other === null || other === undefined) { return true; }
        return value > other;
    }, `\${$displayName} must be after \${$config.otherName}`);

    ValidationRules.customRule(ruleNames.IsBefore, (value, obj, otherName) => {
        if (value === null || value === undefined) { return true; }
        const other = obj[otherName];
        if (other === null || other === undefined) { return true; }
        return value < other;
    }, `\${$displayName} must be before \${$config.otherName}`);

    ValidationRules.customRule(ruleNames.IsOnOrAfter, (value, obj, otherName) => {
        if (value === null || value === undefined) { return true; }
        const other = obj[otherName];
        if (other === null || other === undefined) { return true; }
        return value >= other;
    }, `\${$displayName} must be after \${$config.otherName}`);

    ValidationRules.customRule(ruleNames.IsOnOrBefore, (value, obj, otherName) => {
        if (value === null || value === undefined) { return true; }
        const other = obj[otherName];
        if (other === null || other === undefined) { return true; }
        return value <= other;
    }, `\${$displayName} must be before \${$config.otherName}`);

    ValidationRules.customRule(ruleNames.IsValidUrl,
        (value: any, obj?: any) => new UrlTester().testUrlExists(value.toString(), false),
        "Please enter a URL that exists.");

}

