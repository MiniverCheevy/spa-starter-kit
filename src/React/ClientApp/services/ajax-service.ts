import { EncoderService }  from "./encoder-service";
import { CurrentUserService }  from "./current-user-service";
import { MessengerService } from "./messenger-service";

//import * as Models from "../models.generated";
import * as $ from 'jquery';
let fetch = (<any>window).fetch;

export const AjaxService = new AjaxServicePrototype();

export class AjaxServicePrototype {
   
    private getAjaxRequest(url: string, verb: string, token:string, request: any)
    {
        var body = JSON.stringify(request);
        return fetch(url,
            {
                method: verb,
                credentials: 'same-origon',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json; charset=utf-8',
                    'Token': token
                },
                body: body                
            }
        );
      
    }
    public buildGetRequest = async (request, url): Promise<any> => {
        var user = await CurrentUserService.get();
        var params = EncoderService.serializeParams(request);
        return this.getAjaxRequest(url, 'GET', user.token, params);        

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
        return this.getAjaxRequest(url, 'DELETE', user.token, params);        

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
