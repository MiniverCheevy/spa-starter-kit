import * as mdc from 'material-components-web';
import { observable, IObservableArray } from './../mx';

export class ToastMessage {
    className: string;
    message: string;
}
class MessengerServicePrototype {

    @observable numberOfPendingHttpRequest: number = 0;
    @observable snackbarclass:string;
    @observable confirmPrompt: string;

    private dialog: any;
    private snackbar: any;
    private confirmOkCallback;
    private confirmCancelCallback;

    public confirm = async (prompt: string, okCallback?, cancelCallback?) => {

        if (!this.dialog) {
            this.dialog = new mdc.dialog.MDCDialog(document.querySelector('#my-mdc-dialog'));
            this.dialog.listen('MDCDialog:accept', () => {
                if (this.confirmOkCallback)
                    this.confirmOkCallback();
                this.dialog.style = 'display:none';
            })

            this.dialog.listen('MDCDialog:cancel', () => {
                if (this.confirmCancelCallback)
                    this.confirmCancelCallback();
                this.dialog.style = 'display:none';
            })
        }

        this.confirmOkCallback = null;
        this.confirmCancelCallback = null;
        this.confirmPrompt = prompt;
        this.confirmOkCallback = okCallback;
        this.confirmCancelCallback = cancelCallback;

        this.dialog.style = 'display:block';

        this.dialog.show();
    }
    

    public showResponseMessage = (response: any) => {
        if (response == null)
            return;
        //file upload?
        if (this.numberOfPendingHttpRequest < 0)
            this.numberOfPendingHttpRequest = 0;
        if (response.isOk) {
            if (response.message != null && response.message != '')
                this.showSnackbar(response.message, false);
        }
        else {
            this.showSnackbar(response.message, true);
        }
    }
    private showSnackbar = (message: string, isError: boolean) => {
        if (!this.snackbar) {
            this.snackbar = mdc.snackbar.MDCSnackbar.attachTo(document.querySelector('#my-mdc-snackbar'));
        }

        var multiline = false;
        if (message.length > 50)
            multiline = true;

        const props = {
            message: message,
            multiline: multiline
        };

        if (isError) {
            this.snackbarclass = "snackbar-error";
        }
        else {
            this.snackbarclass = "snackbar-success";
        }
        debugger;
        this.snackbar.show(props);
    }

    public showToast = (text: string, isError: boolean) => {
        if (!isError) {
            if (text != null && text != '')
                this.showSnackbar(text, false);
        }
        else {
            this.showSnackbar(text, true);
        }
    }

    public incrementHttpRequestCounter() {
        this.numberOfPendingHttpRequest += 1;
    }
    public decrementHttpRequestCounter() {
        this.numberOfPendingHttpRequest -= 1;
    }

}
export const MessengerService = new MessengerServicePrototype();