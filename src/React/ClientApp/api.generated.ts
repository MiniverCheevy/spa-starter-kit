//***************************************************************
//This code just called you a tool
//What I meant to say is that this code was generated by a tool
//so don't mess with it unless you're debugging
//subject to change without notice, might regenerate while you're reading, etc
//***************************************************************
import { CurrentUserService } from './services/current-user-service';
import { MessengerService } from './services/messenger-service';
import { AjaxService } from './services/ajax-service';
import * as Models from './models.generated';
export class ApplicationSettingPrototype    {
url: string = 'api/ApplicationSetting';
public async get (request: Models.IIdRequest):
Promise<Models.IResponseOfApplicationSettingMessage>
{
    try {
    MessengerService.incrementHttpRequestCounter();
    var httpResponse = await AjaxService.buildGetRequest(request, this.url);
    var response = await httpResponse.json();
    
    var out = <Models.IResponse>response;
    MessengerService.showResponseMessage(out);
    MessengerService.decrementHttpRequestCounter();
    return out;
}
catch (err)
{
    AjaxService.logError(err, this.url, (<any>new Error()).stack);
    
    var result = {
    isOk: false,
    message: err.statusText
};

MessengerService.decrementHttpRequestCounter();
MessengerService.showResponseMessage(result);
return result;
}
}
public async delete (request: Models.IIdRequest):
Promise<Models.IResponse>
{
    try {
    MessengerService.incrementHttpRequestCounter();
    var httpResponse = await AjaxService.buildDeleteRequest(request, this.url);
    var response = await httpResponse.json();
    
    var out = <Models.IResponse>response;
    MessengerService.showResponseMessage(out);
    MessengerService.decrementHttpRequestCounter();
    return out;
}
catch (err)
{
    AjaxService.logError(err, this.url, (<any>new Error()).stack);
    
    var result = {
    isOk: false,
    message: err.statusText
};

MessengerService.decrementHttpRequestCounter();
MessengerService.showResponseMessage(result);
return result;
}
}}
export const ApplicationSetting = new ApplicationSettingPrototype();

export class ApplicationSettingDetailPrototype    {
url: string = 'api/ApplicationSettingDetail';
public async put (request: Models.IApplicationSettingMessage):
Promise<Models.INewItemResponse>
{
    try {
    MessengerService.incrementHttpRequestCounter();
    var httpResponse = await AjaxService.buildPutRequest(request, this.url);
    var response = await httpResponse.json();
    
    var out = <Models.IResponse>response;
    MessengerService.showResponseMessage(out);
    MessengerService.decrementHttpRequestCounter();
    return out;
}
catch (err)
{
    AjaxService.logError(err, this.url, (<any>new Error()).stack);
    
    var result = {
    isOk: false,
    message: err.statusText
};

MessengerService.decrementHttpRequestCounter();
MessengerService.showResponseMessage(result);
return result;
}
}}
export const ApplicationSettingDetail = new ApplicationSettingDetailPrototype();

export class ApplicationSettingListPrototype    {
url: string = 'api/ApplicationSettingList';
public async get (request: Models.IApplicationSettingQueryRequest):
Promise<Models.IApplicationSettingQueryResponse>
{
    try {
    MessengerService.incrementHttpRequestCounter();
    var httpResponse = await AjaxService.buildGetRequest(request, this.url);
    var response = await httpResponse.json();
    
    var out = <Models.IResponse>response;
    MessengerService.showResponseMessage(out);
    MessengerService.decrementHttpRequestCounter();
    return out;
}
catch (err)
{
    AjaxService.logError(err, this.url, (<any>new Error()).stack);
    
    var result = {
    isOk: false,
    message: err.statusText
};

MessengerService.decrementHttpRequestCounter();
MessengerService.showResponseMessage(result);
return result;
}
}}
export const ApplicationSettingList = new ApplicationSettingListPrototype();

export class CurrentUserPrototype    {
url: string = 'api/CurrentUser';
public async get (request: Models.IEmptyRequest):
Promise<Models.IResponseOfAppPrincipal>
{
    try {
    MessengerService.incrementHttpRequestCounter();
    var httpResponse = await AjaxService.buildGetRequest(request, this.url);
    var response = await httpResponse.json();
    
    var out = <Models.IResponse>response;
    MessengerService.showResponseMessage(out);
    MessengerService.decrementHttpRequestCounter();
    return out;
}
catch (err)
{
    AjaxService.logError(err, this.url, (<any>new Error()).stack);
    
    var result = {
    isOk: false,
    message: err.statusText
};

MessengerService.decrementHttpRequestCounter();
MessengerService.showResponseMessage(result);
return result;
}
}}
export const CurrentUser = new CurrentUserPrototype();

export class ErrorDetailPrototype    {
url: string = 'api/ErrorDetail';
public async get (request: Models.IIdRequest):
Promise<Models.IResponseOfErrorDetail>
{
    try {
    MessengerService.incrementHttpRequestCounter();
    var httpResponse = await AjaxService.buildGetRequest(request, this.url);
    var response = await httpResponse.json();
    
    var out = <Models.IResponse>response;
    MessengerService.showResponseMessage(out);
    MessengerService.decrementHttpRequestCounter();
    return out;
}
catch (err)
{
    AjaxService.logError(err, this.url, (<any>new Error()).stack);
    
    var result = {
    isOk: false,
    message: err.statusText
};

MessengerService.decrementHttpRequestCounter();
MessengerService.showResponseMessage(result);
return result;
}
}}
export const ErrorDetail = new ErrorDetailPrototype();

export class ErrorListPrototype    {
url: string = 'api/ErrorList';
public async get (request: Models.IErrorQueryRequest):
Promise<Models.IErrorQueryResponse>
{
    try {
    MessengerService.incrementHttpRequestCounter();
    var httpResponse = await AjaxService.buildGetRequest(request, this.url);
    var response = await httpResponse.json();
    
    var out = <Models.IResponse>response;
    MessengerService.showResponseMessage(out);
    MessengerService.decrementHttpRequestCounter();
    return out;
}
catch (err)
{
    AjaxService.logError(err, this.url, (<any>new Error()).stack);
    
    var result = {
    isOk: false,
    message: err.statusText
};

MessengerService.decrementHttpRequestCounter();
MessengerService.showResponseMessage(result);
return result;
}
}}
export const ErrorList = new ErrorListPrototype();

export class ListsPrototype    {
url: string = 'api/Lists';
public async get (request: Models.IListsRequest):
Promise<Models.IListsResponse>
{
    try {
    MessengerService.incrementHttpRequestCounter();
    var httpResponse = await AjaxService.buildGetRequest(request, this.url);
    var response = await httpResponse.json();
    
    var out = <Models.IResponse>response;
    MessengerService.showResponseMessage(out);
    MessengerService.decrementHttpRequestCounter();
    return out;
}
catch (err)
{
    AjaxService.logError(err, this.url, (<any>new Error()).stack);
    
    var result = {
    isOk: false,
    message: err.statusText
};

MessengerService.decrementHttpRequestCounter();
MessengerService.showResponseMessage(result);
return result;
}
}}
export const Lists = new ListsPrototype();

export class MobileErrorPrototype    {
url: string = 'api/MobileError';
public async post (request: Models.IMobileErrorRequest):
Promise<Models.IResponse>
{
    try {
    MessengerService.incrementHttpRequestCounter();
    var httpResponse = await AjaxService.buildPostRequest(request, this.url);
    var response = await httpResponse.json();
    
    var out = <Models.IResponse>response;
    MessengerService.showResponseMessage(out);
    MessengerService.decrementHttpRequestCounter();
    return out;
}
catch (err)
{
    AjaxService.logError(err, this.url, (<any>new Error()).stack);
    
    var result = {
    isOk: false,
    message: err.statusText
};

MessengerService.decrementHttpRequestCounter();
MessengerService.showResponseMessage(result);
return result;
}
}}
export const MobileError = new MobileErrorPrototype();

export class UserDetailPrototype    {
url: string = 'api/UserDetail';
public async delete (request: Models.IIdRequest):
Promise<Models.IResponse>
{
    try {
    MessengerService.incrementHttpRequestCounter();
    var httpResponse = await AjaxService.buildDeleteRequest(request, this.url);
    var response = await httpResponse.json();
    
    var out = <Models.IResponse>response;
    MessengerService.showResponseMessage(out);
    MessengerService.decrementHttpRequestCounter();
    return out;
}
catch (err)
{
    AjaxService.logError(err, this.url, (<any>new Error()).stack);
    
    var result = {
    isOk: false,
    message: err.statusText
};

MessengerService.decrementHttpRequestCounter();
MessengerService.showResponseMessage(result);
return result;
}
}
public async get (request: Models.IIdRequest):
Promise<Models.IResponseOfUserDetail>
{
    try {
    MessengerService.incrementHttpRequestCounter();
    var httpResponse = await AjaxService.buildGetRequest(request, this.url);
    var response = await httpResponse.json();
    
    var out = <Models.IResponse>response;
    MessengerService.showResponseMessage(out);
    MessengerService.decrementHttpRequestCounter();
    return out;
}
catch (err)
{
    AjaxService.logError(err, this.url, (<any>new Error()).stack);
    
    var result = {
    isOk: false,
    message: err.statusText
};

MessengerService.decrementHttpRequestCounter();
MessengerService.showResponseMessage(result);
return result;
}
}
public async put (request: Models.IUserDetail):
Promise<Models.INewItemResponse>
{
    try {
    MessengerService.incrementHttpRequestCounter();
    var httpResponse = await AjaxService.buildPutRequest(request, this.url);
    var response = await httpResponse.json();
    
    var out = <Models.IResponse>response;
    MessengerService.showResponseMessage(out);
    MessengerService.decrementHttpRequestCounter();
    return out;
}
catch (err)
{
    AjaxService.logError(err, this.url, (<any>new Error()).stack);
    
    var result = {
    isOk: false,
    message: err.statusText
};

MessengerService.decrementHttpRequestCounter();
MessengerService.showResponseMessage(result);
return result;
}
}}
export const UserDetail = new UserDetailPrototype();

export class UserListPrototype    {
url: string = 'api/UserList';
public async get (request: Models.IUserQueryRequest):
Promise<Models.IUserQueryResponse>
{
    try {
    MessengerService.incrementHttpRequestCounter();
    var httpResponse = await AjaxService.buildGetRequest(request, this.url);
    var response = await httpResponse.json();
    
    var out = <Models.IResponse>response;
    MessengerService.showResponseMessage(out);
    MessengerService.decrementHttpRequestCounter();
    return out;
}
catch (err)
{
    AjaxService.logError(err, this.url, (<any>new Error()).stack);
    
    var result = {
    isOk: false,
    message: err.statusText
};

MessengerService.decrementHttpRequestCounter();
MessengerService.showResponseMessage(result);
return result;
}
}}
export const UserList = new UserListPrototype();

