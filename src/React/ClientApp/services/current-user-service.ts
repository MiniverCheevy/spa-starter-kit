//import * as Models from "../models.generated";
//import * as Api from "../api.generated";
import { MessengerService } from "./messenger-service";
import { AjaxService } from "./ajax-service"
import $ from 'jquery';
let fetch: any;

export class CurrentUserService {

    url: string = 'api/CurrentUser';
    private user: any//Models.IAppPrincipal | Promise<Models.IAppPrincipal>;
    public ClientInfo: any;//Models.IClientInfo
    private messenger: MessengerService = new MessengerService();

    constructor() {


    }

    public get = async () => {

        if (this.user == null) {
            var storedUser = localStorage.getItem("user");
            if (storedUser != null)
                this.user = JSON.parse(storedUser);
        }
        if (this.user != null && (<any>this.user).refreshTime != null) {
            if ((<any>this.user).refreshTime > new Date()) {
                console.log('RefreshUser');
                this.user = null;
            }
        }
        if (this.user == null) {
            this.user = await this.getUser();
            localStorage.setItem("user", JSON.stringify(this.user));
            return this.user;
        }


        return this.user;
    }


    private getUser = async () => {

        console.log('Query User');
        
        var httpResponse = await (<any>window).fetch(this.url,
            {
                method: 'GET',
                credentials: 'same-origin',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json; charset=utf-8'
                }
            });
        
        var response = await httpResponse.json();
        if (response.isOk) {
            this.user = response.data;
            return this.user;
        }
        else {
            this.messenger.showResponseMessage(response);
        }
        return this.user;
    }


    //var unaothorizedUser: Models.IAppPrincipal = { isAuthenticated: false };
    //return unaothorizedUser;
}

