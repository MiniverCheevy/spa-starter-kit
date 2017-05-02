import * as Models from "./models.generated";
import { Controller } from 'cerebral'
import Devtools from 'cerebral/devtools';
let process: any;

class AppState
{
    public User: Models.IAppPrincipal = { isAuthenticated: false, isAdmin: false };
    public ClientInfo: Models.IClientInfo = { timeZoneOffsetInMinutes : new Date().getTimezoneOffset() };
}

const controller = Controller({
    state: new AppState()
    //,
    //devtools: (Devtools({
    //                    // If running standalone debugger. Some environments
    //                    // might require 127.0.0.1 or computer IP address
    //                    remoteDebugger: 'localhost:8585',

    //                    // By default the devtools tries to reconnect
    //                    // to debugger when it can not be reached, but
    //                    // you can turn it off
    //                    reconnect: true
    //    })
    //)
});

export default controller