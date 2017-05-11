import * as Models from "./models.generated";
import * as Api from "./api.generated";
import { autoinject } from "aurelia-framework";
import { CurrentUserService } from "./services/current-user-service";
import * as Moment from 'moment';
import { Aurelia, PLATFORM } from 'aurelia-framework';
import { Router, RouterConfiguration } from 'aurelia-router';


@autoinject()
export class App {
    message = 'Hello World!!!';
 router: Router;
    constructor(private currentUserService: CurrentUserService,
    private userListApi: Api.UserList) {
        var user =  currentUserService.get().then(this.handleAutheticationState);

    }
    handleAutheticationState = async(user: Models.IAppPrincipal) =>{
        
        console.log("user.isAuthticated=" + user.isAuthenticated);
        console.log("user.userName=" + user.userName);
        Moment()
        var date = Moment(user.refreshTime).format('YYYY-MM-DD HH:mm:ss')
        console.log("x=" + date);
        
        var test = await this.userListApi.get({});
    }
    configureRouter(config: RouterConfiguration, router: Router) {
        config.title = 'Aurelia';
        //config.map([{
        //    route: [ '', 'home' ],
        //    name: 'home',
        //    settings: { icon: 'home' },
        //    moduleId: PLATFORM.moduleName('../home/home'),
        //    nav: true,
        //    title: 'Home'
       // }]);

        this.router = router;
}
}
