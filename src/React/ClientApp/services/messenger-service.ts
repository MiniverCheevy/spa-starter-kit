let mdc: any = (<any>window).mdc;

class MessengerServicePrototype {
    private router: any//Router:
    //Do not add additional local dependencies here or you will likely cause circular references and be sad
    //third party stuff (aurelia,jquery, etc) are fine

    public showDialog = false;
    public confirmPrompt;
    private dialogResult: boolean;
    private dialog: any;
    private confirmOkCallback;
    private confirmCancelCallback;
    constructor() {

        this.numberOfPendingHttpRequest = 0;

    }
   

    public confirm = async (prompt: string, okCallback?, cancelCallback?) => {
        this.dialogResult = null;
        this.confirmOkCallback = null;
        this.confirmCancelCallback = null;
        this.confirmPrompt = prompt;
        this.confirmOkCallback = okCallback;
        this.confirmCancelCallback = cancelCallback;

        this.dialog = new mdc.dialog.MDCDialog(document.querySelector('#my-mdc-dialog'));

        this.dialog.listen('MDCDialog:accept', () => {
            this.dialogResult = true;
            if (this.confirmOkCallback)
                this.confirmOkCallback();
            //console.log('accepted');
        })

        this.dialog.listen('MDCDialog:cancel', () => {
            this.dialogResult = false;
            if (this.confirmCancelCallback)
                this.confirmCancelCallback();
            //console.log('canceled');
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
        //if (response.isOk) {
        //    if (response.message != null && response.message != '')
        //        toastr.success(response.message);
        //}
        //else {
        //    this.showToast(response.message, true);
        //}
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



}
export const MessengerService = new MessengerServicePrototype();