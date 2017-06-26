﻿import { Injectable } from '@angular/core';
@Injectable()
export class MessengerService {
    private router: any//Router:
    //Do not add additional local dependencies here or you will likely cause circular references and be sad
    //third party stuff (aurelia,jquery, etc) are fine
    constructor() {

        this.numberOfPendingHttpRequest = 0;

    }
    public confirm = (prompt: string, yesAction: Function, noAction: Function) => {
        //if (this.modal == null)
        //    this.modal = $("#modal_dialog").modal({ show: false });

        //this.confirmPrompt = prompt;
        //this.confirmYesAction = yesAction;
        //this.confirmNoAction = noAction;

        //this.modal.css("margin-top", document.body.scrollTop + 100);
        //this.modal.show();
    }
    private confirmNo = () => {
        this.modal.hide();
        if (this.confirmNoAction != null)
            this.confirmNoAction();
    }
    private confirmYes = () => {
        this.modal.hide();
        if (this.confirmYesAction != null)
            this.confirmYesAction();
    }
    public numberOfPendingHttpRequest: number;
    public clearMessages = () => {
        //$("#message2").text('');
        //this.messageCss = '';
        //this.message = ' ';

    }
    public showResponseMessage = (response: any//Models.IResponse
    ) => {
        if (response == null)
            return;
        //file upload?
        if (this.numberOfPendingHttpRequest < 0)
            this.numberOfPendingHttpRequest = 0;
        if (response.isOk) {
            //if (response.message != null && response.message != '')
            //    toastr.success(response.message);
        }
        else {
            this.showToast(response.message, true);
        }
    }
    public showToast = (text: string, isError: boolean) => {
        //if (!isError) {
        //    if (text != null && text != '')
        //        toastr.success(text);
        //}
        //else {
        //    toastr.error(text);
        //}
    }

    public incrementHttpRequestCounter() {
        this.numberOfPendingHttpRequest += 1;
    }
    public decrementHttpRequestCounter() {
        this.numberOfPendingHttpRequest -= 1;
    }
    public navigate(routeName: string, args: Object) {
        if (args == null)
            this.router.navigateToRoute(routeName);
        else
            this.router.navigateToRoute(routeName, args);
    }


    private modal: any;
    private confirmPrompt: string;
    private confirmYesAction: Function;
    private confirmNoAction: Function;
    public messageCss: string;
    public message: string;
}
export const MessengerServiceStatic = new MessengerService();
