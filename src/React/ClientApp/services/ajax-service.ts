import { EncoderService }  from "./encoder-service";
import { CurrentUserService }  from "./current-user-service";
import { MessengerService } from "./messenger-service";

//import * as Models from "../models.generated";
import * as $ from 'jquery';
import * as  axios  from 'axios';

export class AjaxServicePrototype {
   
    private getAjaxRequest(url: string, verb: string, token:string, request: any)
    {
        var body = JSON.stringify(request);
        if (verb.toLowerCase() == 'get' || verb.toLowerCase() == 'delete')
        {

          

            return (<any>axios)({                    
                    url: url,
                    method: verb,
                    credentials: 'same-origin',
                    headers: [{ 'Accept': 'application/json' },
                    { 'Content-Type': 'application/json; charset=utf-8' },
                    { 'Token': token }]                    
                });    
        }
        else
            {
            (<any>axios)({ 
                    url: url,
                    method: verb,
                    credentials: 'same-origin',
                    headers: [{ 'Accept': 'application/json' },
                    { 'Content-Type': 'application/json; charset=utf-8' },
                    { 'Token': token }],
                    body: body
                });               
            }      
    }
    public buildGetRequest = async (request, url): Promise<any> => {
        var user = await CurrentUserService.get();        
        var params = EncoderService.serializeParams(request);
        var urlWithParams = url + '?' + params;
        return this.getAjaxRequest(urlWithParams, 'GET', user.token, params);        

    }
    public buildPutRequest = async (request, url): Promise<any> => {

        var user = await CurrentUserService.get();
        return this.getAjaxRequest(url, 'PUT', user.token, request);             
    }
    public buildPostRequest = async (request, url): Promise<any> => {

        var user = await CurrentUserService.get();
        return this.getAjaxRequest(url, 'POST', user.token, request);    
    }
    public buildDeleteRequest = async (request, url): Promise<any> => {

        var user = await CurrentUserService.get();
        var params = EncoderService.serializeParams(request);
        var urlWithParams = url + '?' + params;
        return this.getAjaxRequest(urlWithParams, 'DELETE', user.token, params);        

    }
    public logError(err, url, stack) {
        var message = err.statusText + ' (' + err.statusCode + ')';
        console.error(err);
        var error = <any>{};
        error.errorMsg = message;
        error.ErrorObject = stack;
        error.url = url;
        (<any>$).ajax({
            type: "POST",
            url: "api/clienterror",
            data: error
        });
    }
    
}

export const AjaxService = new AjaxServicePrototype();

window.onerror = (message, file, line, column, errorObject) => {
    console.log("window.onerror fired");
    
    column = column || (<any>(window.event));
    var stack = errorObject ? errorObject.stack : null;
    
    //trying to get stack from IE
    if (!stack) {
        var builtStack = [];
        var f = arguments.callee.caller;
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
    AjaxService.logError(errorObject, file, stack);
    MessengerService.showToast(message, true);  
    return false;
}