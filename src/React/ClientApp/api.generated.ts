//***************************************************************
//This code just called you a tool
//What I meant to say is that this code was generated by a tool
//so don't mess with it unless you're debugging
//subject to change without notice, might regenerate while you're reading, etc
//***************************************************************
import { CurrentUserService } from './services/current-user-service';
import { MessengerService } from './services/messenger-service';
import { AjaxService } from './services/ajax-service';
import { EncoderService } from './services/encoder-service';
import * as Models from './models.generated';

export class ApplicationSettingPrototype    {
url: string = 'api/ApplicationSetting';
public async delete (request: Models.IdRequest):
Promise<Models.Response>
{
    var result;
    try {
    MessengerService.incrementHttpRequestCounter();
    var response = await AjaxService.buildDeleteRequest(request, this.url)
    if (response.isOk != undefined) {
    var out = <Models.IResponse>response;
    result = out;
}
else {
AjaxService.logError(response, this.url, (< any > new Error()).stack);

var errorResposne = {
isOk: false,
message: response.statusText || response.message
};
result = out;
}
}
catch (e)
{
    AjaxService.logError(e, this.url, (< any > new Error()).stack);
    
    result = {
    isOk: false,
    message: e.statusText || e.message
};
}
MessengerService.decrementHttpRequestCounter();
MessengerService.showResponseMessage(result);
return result;
}
public async get (request: Models.IdRequest):
Promise<Models.ResponseOfApplicationSettingDetail>
{
    var result;
    try {
    MessengerService.incrementHttpRequestCounter();
    var response = await AjaxService.buildGetRequest(request, this.url)
    if (response.isOk != undefined) {
    var out = <Models.IResponse>response;
    result = out;
}
else {
AjaxService.logError(response, this.url, (< any > new Error()).stack);

var errorResposne = {
isOk: false,
message: response.statusText || response.message
};
result = out;
}
}
catch (e)
{
    AjaxService.logError(e, this.url, (< any > new Error()).stack);
    
    result = {
    isOk: false,
    message: e.statusText || e.message
};
}
MessengerService.decrementHttpRequestCounter();
MessengerService.showResponseMessage(result);
return result;
}
public async put (request: Models.ApplicationSettingDetail):
Promise<Models.NewItemResponse>
{
    var result;
    try {
    MessengerService.incrementHttpRequestCounter();
    var response = await AjaxService.buildPutRequest(request, this.url)
    if (response.isOk != undefined) {
    var out = <Models.IResponse>response;
    result = out;
}
else {
AjaxService.logError(response, this.url, (< any > new Error()).stack);

var errorResposne = {
isOk: false,
message: response.statusText || response.message
};
result = out;
}
}
catch (e)
{
    AjaxService.logError(e, this.url, (< any > new Error()).stack);
    
    result = {
    isOk: false,
    message: e.statusText || e.message
};
}
MessengerService.decrementHttpRequestCounter();
MessengerService.showResponseMessage(result);
return result;
}}
export const ApplicationSetting = new ApplicationSettingPrototype();

export class ApplicationSettingListPrototype    {
url: string = 'api/ApplicationSettingList';
public async get (request: Models.ApplicationSettingListRequest):
Promise<Models.ApplicationSettingListResponse>
{
    var result;
    try {
    MessengerService.incrementHttpRequestCounter();
    var response = await AjaxService.buildGetRequest(request, this.url)
    if (response.isOk != undefined) {
    var out = <Models.IResponse>response;
    result = out;
}
else {
AjaxService.logError(response, this.url, (< any > new Error()).stack);

var errorResposne = {
isOk: false,
message: response.statusText || response.message
};
result = out;
}
}
catch (e)
{
    AjaxService.logError(e, this.url, (< any > new Error()).stack);
    
    result = {
    isOk: false,
    message: e.statusText || e.message
};
}
MessengerService.decrementHttpRequestCounter();
MessengerService.showResponseMessage(result);
return result;
}}
export const ApplicationSettingList = new ApplicationSettingListPrototype();

export class CurrentUserPrototype    {
url: string = 'api/CurrentUser';
public async get (request: Models.EmptyRequest):
Promise<Models.ResponseOfAppPrincipal>
{
    var result;
    try {
    MessengerService.incrementHttpRequestCounter();
    var response = await AjaxService.buildGetRequest(request, this.url)
    if (response.isOk != undefined) {
    var out = <Models.IResponse>response;
    result = out;
}
else {
AjaxService.logError(response, this.url, (< any > new Error()).stack);

var errorResposne = {
isOk: false,
message: response.statusText || response.message
};
result = out;
}
}
catch (e)
{
    AjaxService.logError(e, this.url, (< any > new Error()).stack);
    
    result = {
    isOk: false,
    message: e.statusText || e.message
};
}
MessengerService.decrementHttpRequestCounter();
MessengerService.showResponseMessage(result);
return result;
}}
export const CurrentUser = new CurrentUserPrototype();

export class ErrorLogPrototype    {
url: string = 'api/ErrorLog';
public async get (request: Models.IdRequest):
Promise<Models.ResponseOfErrorDetail>
{
    var result;
    try {
    MessengerService.incrementHttpRequestCounter();
    var response = await AjaxService.buildGetRequest(request, this.url)
    if (response.isOk != undefined) {
    var out = <Models.IResponse>response;
    result = out;
}
else {
AjaxService.logError(response, this.url, (< any > new Error()).stack);

var errorResposne = {
isOk: false,
message: response.statusText || response.message
};
result = out;
}
}
catch (e)
{
    AjaxService.logError(e, this.url, (< any > new Error()).stack);
    
    result = {
    isOk: false,
    message: e.statusText || e.message
};
}
MessengerService.decrementHttpRequestCounter();
MessengerService.showResponseMessage(result);
return result;
}}
export const ErrorLog = new ErrorLogPrototype();

export class ErrorLogListPrototype    {
url: string = 'api/ErrorLogList';
public async get (request: Models.ErrorListRequest):
Promise<Models.ErrorListResponse>
{
    var result;
    try {
    MessengerService.incrementHttpRequestCounter();
    var response = await AjaxService.buildGetRequest(request, this.url)
    if (response.isOk != undefined) {
    var out = <Models.IResponse>response;
    result = out;
}
else {
AjaxService.logError(response, this.url, (< any > new Error()).stack);

var errorResposne = {
isOk: false,
message: response.statusText || response.message
};
result = out;
}
}
catch (e)
{
    AjaxService.logError(e, this.url, (< any > new Error()).stack);
    
    result = {
    isOk: false,
    message: e.statusText || e.message
};
}
MessengerService.decrementHttpRequestCounter();
MessengerService.showResponseMessage(result);
return result;
}}
export const ErrorLogList = new ErrorLogListPrototype();

export class ListsPrototype    {
url: string = 'api/Lists';
public async get (request: Models.ListsRequest):
Promise<Models.ListsResponse>
{
    var result;
    try {
    MessengerService.incrementHttpRequestCounter();
    var response = await AjaxService.buildGetRequest(request, this.url)
    if (response.isOk != undefined) {
    var out = <Models.IResponse>response;
    result = out;
}
else {
AjaxService.logError(response, this.url, (< any > new Error()).stack);

var errorResposne = {
isOk: false,
message: response.statusText || response.message
};
result = out;
}
}
catch (e)
{
    AjaxService.logError(e, this.url, (< any > new Error()).stack);
    
    result = {
    isOk: false,
    message: e.statusText || e.message
};
}
MessengerService.decrementHttpRequestCounter();
MessengerService.showResponseMessage(result);
return result;
}}
export const Lists = new ListsPrototype();

export class MemberPrototype    {
url: string = 'api/Member';
public async delete (request: Models.IdRequest):
Promise<Models.Response>
{
    var result;
    try {
    MessengerService.incrementHttpRequestCounter();
    var response = await AjaxService.buildDeleteRequest(request, this.url)
    if (response.isOk != undefined) {
    var out = <Models.IResponse>response;
    result = out;
}
else {
AjaxService.logError(response, this.url, (< any > new Error()).stack);

var errorResposne = {
isOk: false,
message: response.statusText || response.message
};
result = out;
}
}
catch (e)
{
    AjaxService.logError(e, this.url, (< any > new Error()).stack);
    
    result = {
    isOk: false,
    message: e.statusText || e.message
};
}
MessengerService.decrementHttpRequestCounter();
MessengerService.showResponseMessage(result);
return result;
}
public async get (request: Models.IdRequest):
Promise<Models.ResponseOfMemberDetail>
{
    var result;
    try {
    MessengerService.incrementHttpRequestCounter();
    var response = await AjaxService.buildGetRequest(request, this.url)
    if (response.isOk != undefined) {
    var out = <Models.IResponse>response;
    result = out;
}
else {
AjaxService.logError(response, this.url, (< any > new Error()).stack);

var errorResposne = {
isOk: false,
message: response.statusText || response.message
};
result = out;
}
}
catch (e)
{
    AjaxService.logError(e, this.url, (< any > new Error()).stack);
    
    result = {
    isOk: false,
    message: e.statusText || e.message
};
}
MessengerService.decrementHttpRequestCounter();
MessengerService.showResponseMessage(result);
return result;
}
public async put (request: Models.MemberDetail):
Promise<Models.NewItemResponse>
{
    var result;
    try {
    MessengerService.incrementHttpRequestCounter();
    var response = await AjaxService.buildPutRequest(request, this.url)
    if (response.isOk != undefined) {
    var out = <Models.IResponse>response;
    result = out;
}
else {
AjaxService.logError(response, this.url, (< any > new Error()).stack);

var errorResposne = {
isOk: false,
message: response.statusText || response.message
};
result = out;
}
}
catch (e)
{
    AjaxService.logError(e, this.url, (< any > new Error()).stack);
    
    result = {
    isOk: false,
    message: e.statusText || e.message
};
}
MessengerService.decrementHttpRequestCounter();
MessengerService.showResponseMessage(result);
return result;
}}
export const Member = new MemberPrototype();

export class MemberListPrototype    {
url: string = 'api/MemberList';
public async get (request: Models.MemberListRequest):
Promise<Models.MemberListResponse>
{
    var result;
    try {
    MessengerService.incrementHttpRequestCounter();
    var response = await AjaxService.buildGetRequest(request, this.url)
    if (response.isOk != undefined) {
    var out = <Models.IResponse>response;
    result = out;
}
else {
AjaxService.logError(response, this.url, (< any > new Error()).stack);

var errorResposne = {
isOk: false,
message: response.statusText || response.message
};
result = out;
}
}
catch (e)
{
    AjaxService.logError(e, this.url, (< any > new Error()).stack);
    
    result = {
    isOk: false,
    message: e.statusText || e.message
};
}
MessengerService.decrementHttpRequestCounter();
MessengerService.showResponseMessage(result);
return result;
}}
export const MemberList = new MemberListPrototype();

export class MobileErrorPrototype    {
url: string = 'api/MobileError';
public async post (request: Models.MobileErrorRequest):
Promise<Models.Response>
{
    var result;
    try {
    MessengerService.incrementHttpRequestCounter();
    var response = await AjaxService.buildPostRequest(request, this.url)
    if (response.isOk != undefined) {
    var out = <Models.IResponse>response;
    result = out;
}
else {
AjaxService.logError(response, this.url, (< any > new Error()).stack);

var errorResposne = {
isOk: false,
message: response.statusText || response.message
};
result = out;
}
}
catch (e)
{
    AjaxService.logError(e, this.url, (< any > new Error()).stack);
    
    result = {
    isOk: false,
    message: e.statusText || e.message
};
}
MessengerService.decrementHttpRequestCounter();
MessengerService.showResponseMessage(result);
return result;
}}
export const MobileError = new MobileErrorPrototype();

export class ProjectPrototype    {
url: string = 'api/Project';
public async delete (request: Models.IdRequest):
Promise<Models.Response>
{
    var result;
    try {
    MessengerService.incrementHttpRequestCounter();
    var response = await AjaxService.buildDeleteRequest(request, this.url)
    if (response.isOk != undefined) {
    var out = <Models.IResponse>response;
    result = out;
}
else {
AjaxService.logError(response, this.url, (< any > new Error()).stack);

var errorResposne = {
isOk: false,
message: response.statusText || response.message
};
result = out;
}
}
catch (e)
{
    AjaxService.logError(e, this.url, (< any > new Error()).stack);
    
    result = {
    isOk: false,
    message: e.statusText || e.message
};
}
MessengerService.decrementHttpRequestCounter();
MessengerService.showResponseMessage(result);
return result;
}
public async get (request: Models.IdRequest):
Promise<Models.ResponseOfProjectDetail>
{
    var result;
    try {
    MessengerService.incrementHttpRequestCounter();
    var response = await AjaxService.buildGetRequest(request, this.url)
    if (response.isOk != undefined) {
    var out = <Models.IResponse>response;
    result = out;
}
else {
AjaxService.logError(response, this.url, (< any > new Error()).stack);

var errorResposne = {
isOk: false,
message: response.statusText || response.message
};
result = out;
}
}
catch (e)
{
    AjaxService.logError(e, this.url, (< any > new Error()).stack);
    
    result = {
    isOk: false,
    message: e.statusText || e.message
};
}
MessengerService.decrementHttpRequestCounter();
MessengerService.showResponseMessage(result);
return result;
}
public async put (request: Models.ProjectDetail):
Promise<Models.NewItemResponse>
{
    var result;
    try {
    MessengerService.incrementHttpRequestCounter();
    var response = await AjaxService.buildPutRequest(request, this.url)
    if (response.isOk != undefined) {
    var out = <Models.IResponse>response;
    result = out;
}
else {
AjaxService.logError(response, this.url, (< any > new Error()).stack);

var errorResposne = {
isOk: false,
message: response.statusText || response.message
};
result = out;
}
}
catch (e)
{
    AjaxService.logError(e, this.url, (< any > new Error()).stack);
    
    result = {
    isOk: false,
    message: e.statusText || e.message
};
}
MessengerService.decrementHttpRequestCounter();
MessengerService.showResponseMessage(result);
return result;
}}
export const Project = new ProjectPrototype();

export class ProjectListPrototype    {
url: string = 'api/ProjectList';
public async get (request: Models.ProjectListRequest):
Promise<Models.ProjectListResponse>
{
    var result;
    try {
    MessengerService.incrementHttpRequestCounter();
    var response = await AjaxService.buildGetRequest(request, this.url)
    if (response.isOk != undefined) {
    var out = <Models.IResponse>response;
    result = out;
}
else {
AjaxService.logError(response, this.url, (< any > new Error()).stack);

var errorResposne = {
isOk: false,
message: response.statusText || response.message
};
result = out;
}
}
catch (e)
{
    AjaxService.logError(e, this.url, (< any > new Error()).stack);
    
    result = {
    isOk: false,
    message: e.statusText || e.message
};
}
MessengerService.decrementHttpRequestCounter();
MessengerService.showResponseMessage(result);
return result;
}}
export const ProjectList = new ProjectListPrototype();

export class TeamPrototype    {
url: string = 'api/Team';
public async delete (request: Models.IdRequest):
Promise<Models.Response>
{
    var result;
    try {
    MessengerService.incrementHttpRequestCounter();
    var response = await AjaxService.buildDeleteRequest(request, this.url)
    if (response.isOk != undefined) {
    var out = <Models.IResponse>response;
    result = out;
}
else {
AjaxService.logError(response, this.url, (< any > new Error()).stack);

var errorResposne = {
isOk: false,
message: response.statusText || response.message
};
result = out;
}
}
catch (e)
{
    AjaxService.logError(e, this.url, (< any > new Error()).stack);
    
    result = {
    isOk: false,
    message: e.statusText || e.message
};
}
MessengerService.decrementHttpRequestCounter();
MessengerService.showResponseMessage(result);
return result;
}
public async get (request: Models.IdRequest):
Promise<Models.ResponseOfTeamDetail>
{
    var result;
    try {
    MessengerService.incrementHttpRequestCounter();
    var response = await AjaxService.buildGetRequest(request, this.url)
    if (response.isOk != undefined) {
    var out = <Models.IResponse>response;
    result = out;
}
else {
AjaxService.logError(response, this.url, (< any > new Error()).stack);

var errorResposne = {
isOk: false,
message: response.statusText || response.message
};
result = out;
}
}
catch (e)
{
    AjaxService.logError(e, this.url, (< any > new Error()).stack);
    
    result = {
    isOk: false,
    message: e.statusText || e.message
};
}
MessengerService.decrementHttpRequestCounter();
MessengerService.showResponseMessage(result);
return result;
}
public async put (request: Models.TeamDetail):
Promise<Models.NewItemResponse>
{
    var result;
    try {
    MessengerService.incrementHttpRequestCounter();
    var response = await AjaxService.buildPutRequest(request, this.url)
    if (response.isOk != undefined) {
    var out = <Models.IResponse>response;
    result = out;
}
else {
AjaxService.logError(response, this.url, (< any > new Error()).stack);

var errorResposne = {
isOk: false,
message: response.statusText || response.message
};
result = out;
}
}
catch (e)
{
    AjaxService.logError(e, this.url, (< any > new Error()).stack);
    
    result = {
    isOk: false,
    message: e.statusText || e.message
};
}
MessengerService.decrementHttpRequestCounter();
MessengerService.showResponseMessage(result);
return result;
}}
export const Team = new TeamPrototype();

export class TeamListPrototype    {
url: string = 'api/TeamList';
public async get (request: Models.TeamListRequest):
Promise<Models.TeamListResponse>
{
    var result;
    try {
    MessengerService.incrementHttpRequestCounter();
    var response = await AjaxService.buildGetRequest(request, this.url)
    if (response.isOk != undefined) {
    var out = <Models.IResponse>response;
    result = out;
}
else {
AjaxService.logError(response, this.url, (< any > new Error()).stack);

var errorResposne = {
isOk: false,
message: response.statusText || response.message
};
result = out;
}
}
catch (e)
{
    AjaxService.logError(e, this.url, (< any > new Error()).stack);
    
    result = {
    isOk: false,
    message: e.statusText || e.message
};
}
MessengerService.decrementHttpRequestCounter();
MessengerService.showResponseMessage(result);
return result;
}}
export const TeamList = new TeamListPrototype();

export class TestClassPrototype    {
url: string = 'api/TestClass';
public async delete (request: Models.IdRequest):
Promise<Models.Response>
{
    var result;
    try {
    MessengerService.incrementHttpRequestCounter();
    var response = await AjaxService.buildDeleteRequest(request, this.url)
    if (response.isOk != undefined) {
    var out = <Models.IResponse>response;
    result = out;
}
else {
AjaxService.logError(response, this.url, (< any > new Error()).stack);

var errorResposne = {
isOk: false,
message: response.statusText || response.message
};
result = out;
}
}
catch (e)
{
    AjaxService.logError(e, this.url, (< any > new Error()).stack);
    
    result = {
    isOk: false,
    message: e.statusText || e.message
};
}
MessengerService.decrementHttpRequestCounter();
MessengerService.showResponseMessage(result);
return result;
}
public async get (request: Models.IdRequest):
Promise<Models.ResponseOfTestClassDetail>
{
    var result;
    try {
    MessengerService.incrementHttpRequestCounter();
    var response = await AjaxService.buildGetRequest(request, this.url)
    if (response.isOk != undefined) {
    var out = <Models.IResponse>response;
    result = out;
}
else {
AjaxService.logError(response, this.url, (< any > new Error()).stack);

var errorResposne = {
isOk: false,
message: response.statusText || response.message
};
result = out;
}
}
catch (e)
{
    AjaxService.logError(e, this.url, (< any > new Error()).stack);
    
    result = {
    isOk: false,
    message: e.statusText || e.message
};
}
MessengerService.decrementHttpRequestCounter();
MessengerService.showResponseMessage(result);
return result;
}
public async post (request: Models.TestClassDetail):
Promise<Models.NewItemResponse>
{
    var result;
    try {
    MessengerService.incrementHttpRequestCounter();
    var response = await AjaxService.buildPostRequest(request, this.url)
    if (response.isOk != undefined) {
    var out = <Models.IResponse>response;
    result = out;
}
else {
AjaxService.logError(response, this.url, (< any > new Error()).stack);

var errorResposne = {
isOk: false,
message: response.statusText || response.message
};
result = out;
}
}
catch (e)
{
    AjaxService.logError(e, this.url, (< any > new Error()).stack);
    
    result = {
    isOk: false,
    message: e.statusText || e.message
};
}
MessengerService.decrementHttpRequestCounter();
MessengerService.showResponseMessage(result);
return result;
}}
export const TestClass = new TestClassPrototype();

export class TestClassListPrototype    {
url: string = 'api/TestClassList';
public async get (request: Models.TestClassListRequest):
Promise<Models.TestClassListResponse>
{
    var result;
    try {
    MessengerService.incrementHttpRequestCounter();
    var response = await AjaxService.buildGetRequest(request, this.url)
    if (response.isOk != undefined) {
    var out = <Models.IResponse>response;
    result = out;
}
else {
AjaxService.logError(response, this.url, (< any > new Error()).stack);

var errorResposne = {
isOk: false,
message: response.statusText || response.message
};
result = out;
}
}
catch (e)
{
    AjaxService.logError(e, this.url, (< any > new Error()).stack);
    
    result = {
    isOk: false,
    message: e.statusText || e.message
};
}
MessengerService.decrementHttpRequestCounter();
MessengerService.showResponseMessage(result);
return result;
}}
export const TestClassList = new TestClassListPrototype();

export class UserPrototype    {
url: string = 'api/User';
public async delete (request: Models.IdRequest):
Promise<Models.Response>
{
    var result;
    try {
    MessengerService.incrementHttpRequestCounter();
    var response = await AjaxService.buildDeleteRequest(request, this.url)
    if (response.isOk != undefined) {
    var out = <Models.IResponse>response;
    result = out;
}
else {
AjaxService.logError(response, this.url, (< any > new Error()).stack);

var errorResposne = {
isOk: false,
message: response.statusText || response.message
};
result = out;
}
}
catch (e)
{
    AjaxService.logError(e, this.url, (< any > new Error()).stack);
    
    result = {
    isOk: false,
    message: e.statusText || e.message
};
}
MessengerService.decrementHttpRequestCounter();
MessengerService.showResponseMessage(result);
return result;
}
public async get (request: Models.IdRequest):
Promise<Models.ResponseOfUserDetail>
{
    var result;
    try {
    MessengerService.incrementHttpRequestCounter();
    var response = await AjaxService.buildGetRequest(request, this.url)
    if (response.isOk != undefined) {
    var out = <Models.IResponse>response;
    result = out;
}
else {
AjaxService.logError(response, this.url, (< any > new Error()).stack);

var errorResposne = {
isOk: false,
message: response.statusText || response.message
};
result = out;
}
}
catch (e)
{
    AjaxService.logError(e, this.url, (< any > new Error()).stack);
    
    result = {
    isOk: false,
    message: e.statusText || e.message
};
}
MessengerService.decrementHttpRequestCounter();
MessengerService.showResponseMessage(result);
return result;
}
public async put (request: Models.UserDetail):
Promise<Models.NewItemResponse>
{
    var result;
    try {
    MessengerService.incrementHttpRequestCounter();
    var response = await AjaxService.buildPutRequest(request, this.url)
    if (response.isOk != undefined) {
    var out = <Models.IResponse>response;
    result = out;
}
else {
AjaxService.logError(response, this.url, (< any > new Error()).stack);

var errorResposne = {
isOk: false,
message: response.statusText || response.message
};
result = out;
}
}
catch (e)
{
    AjaxService.logError(e, this.url, (< any > new Error()).stack);
    
    result = {
    isOk: false,
    message: e.statusText || e.message
};
}
MessengerService.decrementHttpRequestCounter();
MessengerService.showResponseMessage(result);
return result;
}}
export const User = new UserPrototype();

export class UserListPrototype    {
url: string = 'api/UserList';
public async get (request: Models.UserListRequest):
Promise<Models.UserListResponse>
{
    var result;
    try {
    MessengerService.incrementHttpRequestCounter();
    var response = await AjaxService.buildGetRequest(request, this.url)
    if (response.isOk != undefined) {
    var out = <Models.IResponse>response;
    result = out;
}
else {
AjaxService.logError(response, this.url, (< any > new Error()).stack);

var errorResposne = {
isOk: false,
message: response.statusText || response.message
};
result = out;
}
}
catch (e)
{
    AjaxService.logError(e, this.url, (< any > new Error()).stack);
    
    result = {
    isOk: false,
    message: e.statusText || e.message
};
}
MessengerService.decrementHttpRequestCounter();
MessengerService.showResponseMessage(result);
return result;
}}
export const UserList = new UserListPrototype();

