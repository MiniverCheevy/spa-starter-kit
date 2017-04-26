//import * as Models from "../models.generated";
//import * as Api from "../api.generated";
import { MessengerService } from "./messenger-service";
import { AjaxService } from "./ajax-service"
import $ from 'jquery';

export class CurrentUserService {

    url: string = 'api/CurrentUser';
    private user: any//Models.IAppPrincipal | Promise<Models.IAppPrincipal>;
    public ClientInfo: any;//Models.IClientInfo
    private messenger: MessengerService = new MessengerService();

    //Do not add additional local dependencies here or you will likely cause circular references and be sad
    //third party stuff (aurelia,jquery, etc) is fine
    constructor() {


    }

    public get() {

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


    private getUser() {

        console.log('Query User');

        return $.ajax({
            cache: false,
            dataType: 'json',
            data: {},
            type: 'GET',
            url: this.url,
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
            }
        }).done((response) => {
            if (response.isOk) {
                this.user = response.data;
                return this.user;
            }
            else {
                this.messenger.showResponseMessage(response);
            }
            }).fail((err) => {
            debugger;
        })
    }




    //var unaothorizedUser: Models.IAppPrincipal = { isAuthenticated: false };
    //return unaothorizedUser;
}

