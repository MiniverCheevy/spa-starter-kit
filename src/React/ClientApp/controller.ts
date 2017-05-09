import * as Models from "./models.generated";
let process: any;

class AppState
{
    public User: Models.IAppPrincipal = { isAuthenticated: false, isAdmin: false };
    public ClientInfo: Models.IClientInfo = { timeZoneOffsetInMinutes : new Date().getTimezoneOffset() };
}

