import { HttpClient, HttpResponseMessage, RequestBuilder } from 'aurelia-http-client';
import { autoinject, inject } from "aurelia-framework";
import { EncoderService } from "./encoder-service";
import { CurrentUserService } from "./current-user-service";
import { MessengerService } from "./messenger-service";
import * as Models from "../models.generated";
import * as $ from 'jquery';

@autoinject()
export class AjaxService {

    //Do not add additional local dependencies here or you will likely cause circular references and be sad
    //third party stuff (aurelia,jquery, etc) is fine
    constructor(private http: HttpClient,
        private messenger: MessengerService,
        private encoder: EncoderService,
        private currentUserService: CurrentUserService) {
    }

    private addHeaders = (requestBuilder: RequestBuilder, user: Models.IAppPrincipal): RequestBuilder => {
        return requestBuilder
            .withHeader('Content-Type', 'application/json; charset=utf-8')
            .withHeader('Token', user.token);
    }

    public buildGetRequest = async (request, url): Promise<RequestBuilder> => {

        var user = await this.currentUserService.get();
        var params = this.encoder.serializeParams(request);
        var requestBuilder = this.http.createRequest(url + '?' + params).asGet();
        return this.addHeaders(requestBuilder, user);

    }
    public buildPutRequest = async (request, url): Promise<RequestBuilder> => {

        var user = await this.currentUserService.get();
        var requestBuilder = this.http.createRequest(url)
            .asPut().withContent(request);
        return this.addHeaders(requestBuilder, user);
    }
    public buildPostRequest = async (request, url): Promise<RequestBuilder> => {

        var user = await this.currentUserService.get();
        var requestBuilder = this.http.createRequest(url)
            .asPost().withContent(request);
        return this.addHeaders(requestBuilder, user);
    }
    public buildDeleteRequest = async (request, url): Promise<RequestBuilder> => {

        var user = await this.currentUserService.get();
        var params = this.encoder.serializeParams(request);
        var requestBuilder = this.http.createRequest(url + '?' + params).asDelete();
        return this.addHeaders(requestBuilder, user);
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
