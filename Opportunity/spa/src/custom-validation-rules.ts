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

export const IsValidUrl = "isValidUrl";


export function addCustomValidationRules() {
    ValidationRules.customRule(IsValidUrl,
        (value: any, obj?: any) => new UrlTester().testUrlExists(value.toString(), false),
        "Please enter a URL that exists.");
}

