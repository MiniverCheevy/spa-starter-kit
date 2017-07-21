import { EncoderService } from "./encoder-service";
import { CurrentUserService, CurrentUserServiceStatic } from "./current-user-service";
import { MessengerService, MessengerServiceStatic } from "./messenger-service";
import { Http, Headers } from "@angular/http";
import { Injectable } from '@angular/core';
import 'rxjs/add/operator/toPromise';
import { AjaxServiceStatic } from './../app/app.module';


@Injectable()
export class AjaxService {
    constructor(private messengerService: MessengerService,
        private currentUserService: CurrentUserService,
        private fetch: Http) { }

    public showReport(url: string, request: any) {
        var params = EncoderService.serializeParams(request);
        var urlWithParams = url + '?' + params;
        window.open(urlWithParams);
    }
    private getAjaxRequest = (url: string, verb: string, token: string, request: any) => {
        try {

            var headers = new Headers();

            headers.append('Accept', 'application/json');
            headers.append('Content-Type', 'application/json; charset=utf-8');
            headers.append('Token', token);

            var body = JSON.stringify(request);
            if (verb.toLowerCase() == 'get' || verb.toLowerCase() == 'delete') {
                return this.fetch.request(url, {
                    withCredentials: true,
                    method: verb,
                    headers: headers
                }).toPromise()
                    .then((r) => {
                        return r.json();
                    })
                    .catch((e) => {
                        this.logError(e, url, new Error().stack);
                        return {
                            isOk: false,
                            message: e.statusText || e.message
                        };
                    });
            } else {

                return this.fetch.request(url, {
                    withCredentials: true,
                    method: verb,
                    headers: headers,
                    body: body
                }).toPromise()
                    .then((r) => { return r.json(); })
                    .catch((e) => {
                        
                        this.logError(e, url, new Error().stack);
                        return {
                            isOk: false,
                            message: e.statusText || e.message
                        };
                    });                
            }
        }
        catch (e) {
            //never hit, why?            
            console.log('The try caught' + e.toString());
            this.logError(e, url, new Error().stack);
        }

    }

    public buildGetRequest = async (request, url): Promise<any> => {
        var user = await this.currentUserService.get();
        var params = EncoderService.serializeParams(request);
        var urlWithParams = url + '?' + params;
        return this.getAjaxRequest(urlWithParams, 'GET', user.token, params);

    }
    public buildPutRequest = async (request, url): Promise<any> => {
        var user = await this.currentUserService.get();
        var req = await this.getAjaxRequest(url, 'PUT', user.token, request);
        return req;
    }
    public buildPostRequest = async (request, url): Promise<any> => {

        var user = await this.currentUserService.get();
        return this.getAjaxRequest(url, 'POST', user.token, request);
    }
    public buildDeleteRequest = async (request, url): Promise<any> => {

        var user = await this.currentUserService.get();
        var params = EncoderService.serializeParams(request);
        var urlWithParams = url + '?' + params;
        return this.getAjaxRequest(urlWithParams, 'DELETE', user.token, params);

    }
    public logError(err, url, stack) {
        //TODO:
        //var message = err.message;
        //if (message == null) {
        //    message = err.statusText;
        //    if (message != null && err.statusCode != null)
        //        message = message + ' (' + err.statusCode + ')';
        //}
        //console.error(err);
        //var error = <any>{};
        //error.errorMsg = message;
        //error.ErrorObject = stack;
        //error.url = url;
        //(<any>$).ajax({
        //    type: "POST",
        //    url: "api/clienterror",
        //    data: error
        //});
    }

}

window.onerror = function (message, file, line, column, errorObject) {
    console.log("window.onerror fired");

    column = column || (<any>(window.event));
    var stack = errorObject ? errorObject.stack : null;

    //trying to get stack from IE
    if (!stack) {
        var builtStack = [];
        var f = <any>arguments.callee.caller;
        while (f) {
            builtStack.push(f.name);
            f = f.caller;
        }
        errorObject['stack'] = <any>builtStack;
    }

    var data = {
        message: message,
        file: file,
        line: line,
        column: column,
        errorStack: stack,
    };
    console.log(data);
    AjaxServiceStatic.logError(errorObject, file, stack);
    MessengerServiceStatic.showToast(message, true);
    return false;
}