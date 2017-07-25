import { HttpClient, HttpResponseMessage, RequestBuilder } from 'aurelia-http-client';
import * as Models from "../models.generated";
import * as Api from "../api.generated";
import { autoinject } from "aurelia-framework";
import { MessengerService } from "./messenger-service";
import { AjaxService } from "./ajax-service";



@autoinject()
export class CurrentUserService {

    url: string = 'api/CurrentUser';
    private user: Models.AppPrincipal | Promise<Models.AppPrincipal>;
    public ClientInfo: Models.ClientInfo

    //Do not add additional local dependencies here or you will likely cause circular references and be sad
    //third party stuff (aurelia,jquery, etc) is fine
    constructor(private http: HttpClient,
        private messenger: MessengerService) {


    }

    public async get() {
        
        if (this.user != null && (<any>this.user).refreshTime != null) {
            if ((<any>this.user).refreshTime > new Date()) {
                console.log('RefreshUser');
                this.user = null;
            }
        }
        if (this.user == null) {
            this.user = this.getUser();
            return this.user;
        }


        return this.user;
    }


    private async getUser() {

        try {
            console.log('Query User');
            var requestBuilder = this.http.createRequest(this.url).asGet()
                .withHeader('Content-Type', 'application/json; charset=utf-8');

            var httpResponse = await requestBuilder.send();
            var response = <Models.ResponseOfAppPrincipal>JSON.parse(httpResponse.response);
            if (response.isOk) {
                this.user = response.data;
                return this.user;
            }
            else {
                this.messenger.showResponseMessage(response);
            }
        }
        catch (error) {
            this.messenger.showResponseMessage({ isOk: false, message: error.msg });
            return null;
        }
    }




    //var unaothorizedUser: Models.IAppPrincipal = { isAuthenticated: false };
    //return unaothorizedUser;
}

