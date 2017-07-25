import { MessengerService, MessengerServiceStatic } from "./messenger-service";
import { AjaxService } from "./ajax-service"
let fetch: any;
import { Injectable } from '@angular/core';
@Injectable()
export class CurrentUserService {

    url: string = 'api/CurrentUser';
    private user: any//Models.IAppPrincipal | Promise<Models.IAppPrincipal>;
    public ClientInfo: any;//Models.IClientInfo
   

    constructor(private messengerService: MessengerService) {


    }

    public get = async () => {
        if (this.user != null && (<any>this.user).refreshTime != null) {
            if ((<any>this.user).refreshTime > new Date()) {
                console.log('RefreshUser');
                this.user = null;
            }
        }
        if (this.user == null) {
            this.user = await this.getUser();
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
            this.messengerService.showResponseMessage(response);
        }
        return this.user;
    }
    
}

export const CurrentUserServiceStatic = new CurrentUserService(MessengerServiceStatic);



