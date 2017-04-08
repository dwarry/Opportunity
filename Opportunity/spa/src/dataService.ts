import 'whatwg-fetch';
import { HttpClient, json, RequestInit } from 'aurelia-fetch-client';

import { getLogger } from 'aurelia-logging';

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

export class DataServiceBase {
    protected _httpClient = new HttpClient();

    private _log = getLogger("DataService");

    constructor() {
        this.initializeHttpClient();
    }

    protected initializeHttpClient() {
        this._httpClient.configure(config => {
            config
                .withBaseUrl('api/')
                .withDefaults({
                    credentials: 'same-origin',
                    headers: {
                        'Accept': 'application/json',
                    }
                })
                .withInterceptor({
                    request(request) {
                        this._log.debug(`Requesting ${request.method} ${request.url}`);
                        return request; // you can return a modified Request, or you can short-circuit the request by returning a Response
                    },
                    response(response) {
                        this._log.debug(`Received ${response.status} ${response.url}`);
                        return response; // you can return a modified Response
                    }
                });

            config.rejectErrorResponses();
        });
    }

    protected sendJson(method: string, url: string, content: any) {

        let req = {
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

        let result = this._httpClient.fetch('users/current')
            .then<any>(response => response.json())
            .then<IUser>(data => <IUser>data);

        return result;
    }

    getReferenceData(): Promise<IReferenceData> {

        let result = this._httpClient.fetch('referenceData')
            .then<any>(response => response.json())
            .then<IReferenceData>(data => <IReferenceData>data);
        return result;
    }
}