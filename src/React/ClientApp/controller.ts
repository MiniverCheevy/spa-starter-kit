import * as Models from "./models.generated";
let process: any;

class AppState
{
    public User: Models.AppPrincipal = { isAuthenticated: false, isAdmin: false };
    public ClientInfo: Models.ClientInfo = { timeZoneOffsetInMinutes : new Date().getTimezoneOffset() };
}

