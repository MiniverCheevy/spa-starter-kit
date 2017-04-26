import * as Models from "./models.generated";
import * as Api from "./api.generated";
import { autoinject } from "aurelia-framework";
import { CurrentUserService } from "./services/current-user-service";
import * as Moment from 'moment';

@autoinject()
export class App {
    message = 'Hello World!?!?!';

    constructor(private currentUserService: CurrentUserService,
    private userListApi: Api.UserList) {
        var user =  currentUserService.get().then(this.handleAutheticationState);

    }
    handleAutheticationState = async(user: Models.IAppPrincipal) =>{
        
        console.log(user.isAuthenticated);
        Moment()
        var date = Moment(user.refreshTime).format('YYYY-MM-DD HH:mm:ss')
        console.log("x=" + date);
        console.log("foo");
        var test = await this.userListApi.get({});
    }
}
