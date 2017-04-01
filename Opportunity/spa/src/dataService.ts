import 'fetch';

import { HttpClient, json } from 'aurelia-fetch-client';

import { getLogger } from 'aurelia-logging';

import { IUser } from './models/user';

export class DataService {
    private _httpClient = new HttpClient();

    private _log = getLogger("DataService");

    constructor() {

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

    getUser(): Promise<IUser> {

        let result = this._httpClient.fetch('users/current')
            .then<any>(response => response.json())
            .then<IUser>(data => <IUser>data);

        return result;
    }



}