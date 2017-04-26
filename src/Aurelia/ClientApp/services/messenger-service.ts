import * as Models from "../models.generated";
import * as Api from "../api.generated";
import { autoinject } from "aurelia-framework";
//import { Confirm } from "components/confirm";
import { Router } from "aurelia-router";
import * as $ from 'jquery';
declare var toastr;


@autoinject
export class MessengerService {

    //Do not add additional local dependencies here or you will likely cause circular references and be sad
    //third party stuff (aurelia,jquery, etc) are fine
    constructor(private router: Router) {

        this.numberOfPendingHttpRequest = 0;

    }
    public confirm = (prompt: string, yesAction: Function, noAction: Function) => {
        if (this.modal == null)
            this.modal = $("#modal_dialog").modal({ show: false });

        this.confirmPrompt = prompt;
        this.confirmYesAction = yesAction;
        this.confirmNoAction = noAction;

        this.modal.css("margin-top", document.body.scrollTop + 100);
        this.modal.show();
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
        $("#message2").text('');
        this.messageCss = '';
        this.message = ' ';

    }
    public showResponseMessage = (response: Models.IResponse) => {
        //file upload?
        if (this.numberOfPendingHttpRequest < 0)
            this.numberOfPendingHttpRequest = 0;
        if (response.isOk) {
            if (response.message != null && response.message != '')
                toastr.success(response.message);
        }
        else {
            this.showToast(response.message, true);
        }
    }
    public showToast = (text: string, isError: boolean) => {
        if (!isError) {
            if (text != null && text != '')
                toastr.success(text);
        }
        else {
            toastr.error(text);
        }
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

