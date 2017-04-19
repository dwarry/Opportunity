import 'whatwg-fetch';
import { HttpClient, json, RequestInit } from 'aurelia-fetch-client';

import { getLogger } from 'aurelia-logging';

import environment from './environment';
import { IUser } from './models/user';
import { IReferenceData } from './models/reference-data';

const PostDefault: RequestInit = {
    'method': 'POST',
    'credentials': 'same-origin',
    'headers': {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
    }
}

const _log = getLogger('DataService');

export class DataServiceBase {
    protected _httpClient = new HttpClient();

    constructor() {
        this.initializeHttpClient();
    }

    protected initializeHttpClient() {
        _log.debug("Initializing the HttpClient");

        this._httpClient.configure(config => {
            config
                .withBaseUrl(environment.apiBase || 'api/')
                .withDefaults({
                    credentials: environment.credentials || 'same-origin',
                    headers: {
                        'Accept': 'application/json',
                    }
                })
                .withInterceptor({
                    request(request) {
                        _log.debug(`Requesting ${request.method} ${request.url}`);
                        return request; // you can return a modified Request, or you can short-circuit the request by returning a Response
                    },
                    response(response) {
                        _log.debug(`Received ${response.status} ${response.url}`);
                        return response; // you can return a modified Response
                    }
                });

            config.rejectErrorResponses();
        });
    }

    protected sendJson(method: string, url: string, content: any) {

        _log.debug(`${method} to ${url}`);

        let req = {
            'credentials': environment.credentials || 'same-origin',
            'headers': {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            'method': method
        };

        if (content) {
            req['body'] = (typeof (content) === 'string') ? content : JSON.stringify(content);
        }

        return this._httpClient.fetch(url, req);
    }

    protected post(url: string, content: any): Promise<any> {

        return this.sendJson('POST', url, content);
    }

    protected put(url: string, content: any) {

        return this.sendJson('PUT', url, content);
    }

    protected delete(url: string, content: any) {

        return this.sendJson('DELETE', url, content);
    }
}


export class DataService extends DataServiceBase {

    getUser(): Promise<IUser> {
        _log.debug("Retrieving current user");

        let result = this._httpClient.fetch('users/current')
            .then<any>(response => response.json())
            .then<IUser>(data => <IUser>data);

        return result;
    }

    getReferenceData(): Promise<IReferenceData> {
        _log.debug("Retrieving reference data");
        let result = this._httpClient.fetch('referenceData')
            .then<any>(response => response.json())
            .then<IReferenceData>(data => <IReferenceData>data);
        return result;
    }
}