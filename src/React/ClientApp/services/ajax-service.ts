//import { HttpClient, HttpResponseMessage, RequestBuilder } from 'aurelia-http-client';
//import { autoinject, inject } from "aurelia-framework";
import { EncoderService }  from "./encoder-service";
import { CurrentUserService }  from "./current-user-service";
import { MessengerService } from "./messenger-service";

//import * as Models from "../models.generated";
import * as $ from 'jquery';


export class AjaxService {
    private messenger: MessengerService = new MessengerService();
    private encoder: EncoderService = new EncoderService();
    private currentUserService: CurrentUserService = new CurrentUserService();
      
    private getAjaxRequest(url: string, verb: string, token:string, request: any)
    {
        return $.ajax({
                cache: false,
                dataType: 'json',
                data: request,
                type: verb,
                url: url,
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'Token': token
                }
            });
    }
    public buildGetRequest = async (request, url): Promise<any> => {
        var user = await this.currentUserService.get();
        var params = this.encoder.serializeParams(request);
        return this.getAjaxRequest(url, 'GET', user.token, params);        

    }
    public buildPutRequest = async (request, url): Promise<any> => {

        var user = await this.currentUserService.get();
        return this.getAjaxRequest(url, 'PUT', user.token, request);             
    }
    public buildPostRequest = async (request, url): Promise<any> => {

        var user = await this.currentUserService.get();
        return this.getAjaxRequest(url, 'POST', user.token, request);    
    }
    public buildDeleteRequest = async (request, url): Promise<any> => {

        var user = await this.currentUserService.get();
        var params = this.encoder.serializeParams(request);
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
