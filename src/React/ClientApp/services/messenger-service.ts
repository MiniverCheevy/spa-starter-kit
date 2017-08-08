import * as mdc from 'material-components-web';
import { observable, IObservableArray } from './../mx';

//let mdc: any = (<any>window).mdc;
export class ToastMessage {
    className: string;
    message: string;
}
class MessengerServicePrototype {
    //private router: any//Router:
    //Do not add additional local dependencies here or you will likely cause circular references and be sad
    //third party stuff (aurelia,jquery, etc) are fine

    public showDialog = false;
    @observable public confirmPrompt;
    @observable public toastMessages: IObservableArray<ToastMessage> = observable([]);
    private dialogResult: boolean;
    private dialog: any;
    private snackbar: any;
    private confirmOkCallback;
    private confirmCancelCallback;
    constructor() {

        this.numberOfPendingHttpRequest = 0;

    }
    snackbarVisibilityChanged(event) {
        debugger;
    }

    public confirm = async (prompt: string, okCallback?, cancelCallback?) => {

        if (!this.dialog) {
            this.dialog = new mdc.dialog.MDCDialog(document.querySelector('#my-mdc-dialog'));
        }

        this.dialogResult = null;
        this.confirmOkCallback = null;
        this.confirmCancelCallback = null;
        this.confirmPrompt = prompt;
        this.confirmOkCallback = okCallback;
        this.confirmCancelCallback = cancelCallback;

        this.dialog.style = 'display:block';

        this.dialog.listen('MDCDialog:accept', () => {
            this.dialogResult = true;
            if (this.confirmOkCallback)
                this.confirmOkCallback();
            this.dialog.style = 'display:none';
        })

        this.dialog.listen('MDCDialog:cancel', () => {
            this.dialogResult = false;
            if (this.confirmCancelCallback)
                this.confirmCancelCallback();
            this.dialog.style = 'display:none';
        })
        this.dialog.show();
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
            if (response.message != null && response.message != '')
                this.success(response.message);
        }
        else {
            this.error(response.message);
        }
    }
    private showSnackbar() {
        debugger;
        if (!this.snackbar) {
            this.snackbar = new mdc.snackbar.MDCSnackbar(document.querySelector('#my-mdc-snackbar'));
            this.snackbar.registerVisibilityChangeHandler(this.snackbarVisibilityChanged);
        }
        this.snackbar.show();
    }
    private success(message: string) {
        this.toastMessages.push({ message: message, className: 'sucess' });
        this.showSnackbar();
    }
    private error(message: string) {
        this.toastMessages.push({ message: message, className: 'error' });
        this.showSnackbar();
    }
    public showToast = (text: string, isError: boolean) => {
        if (!isError) {
            if (text != null && text != '')
                this.success(text);
        }
        else {
            this.error(text);
        }
    }

    public incrementHttpRequestCounter() {
        this.numberOfPendingHttpRequest += 1;
    }
    public decrementHttpRequestCounter() {
        this.numberOfPendingHttpRequest -= 1;
    }
    //public navigate(routeName: string, args: Object) {
    //    if (args == null)
    //        this.router.navigateToRoute(routeName);
    //    else
    //        this.router.navigateToRoute(routeName, args);
    //}



}
export const MessengerService = new MessengerServicePrototype();