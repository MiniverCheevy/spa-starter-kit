var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
define(["require", "exports", "aurelia-framework", "aurelia-router", "jquery"], function (require, exports, aurelia_framework_1, aurelia_router_1, $) {
    "use strict";
    var MessengerService = (function () {
        //Do not add additional local dependencies here or you will likely cause circular references and be sad
        //third party stuff (aurelia,jquery, etc) are fine
        function MessengerService(router) {
            var _this = this;
            this.router = router;
            this.confirm = function (prompt, yesAction, noAction) {
                if (_this.modal == null)
                    _this.modal = $("#modal_dialog").modal({ show: false });
                _this.confirmPrompt = prompt;
                _this.confirmYesAction = yesAction;
                _this.confirmNoAction = noAction;
                _this.modal.css("margin-top", document.body.scrollTop + 100);
                _this.modal.show();
            };
            this.confirmNo = function () {
                _this.modal.hide();
                if (_this.confirmNoAction != null)
                    _this.confirmNoAction();
            };
            this.confirmYes = function () {
                _this.modal.hide();
                if (_this.confirmYesAction != null)
                    _this.confirmYesAction();
            };
            this.clearMessages = function () {
                $("#message2").text('');
                _this.messageCss = '';
                _this.message = ' ';
            };
            this.showResponseMessage = function (response) {
                //file upload?
                if (_this.numberOfPendingHttpRequest < 0)
                    _this.numberOfPendingHttpRequest = 0;
                if (response.isOk) {
                    if (response.message != null && response.message != '')
                        toastr.success(response.message);
                }
                else {
                    _this.showToast(response.message, true);
                }
            };
            this.showToast = function (text, isError) {
                if (!isError) {
                    if (text != null && text != '')
                        toastr.success(text);
                }
                else {
                    toastr.error(text);
                }
            };
            this.numberOfPendingHttpRequest = 0;
        }
        MessengerService.prototype.incrementHttpRequestCounter = function () {
            this.numberOfPendingHttpRequest += 1;
        };
        MessengerService.prototype.decrementHttpRequestCounter = function () {
            this.numberOfPendingHttpRequest -= 1;
        };
        MessengerService.prototype.navigate = function (routeName, args) {
            if (args == null)
                this.router.navigateToRoute(routeName);
            else
                this.router.navigateToRoute(routeName, args);
        };
        return MessengerService;
    }());
    MessengerService = __decorate([
        aurelia_framework_1.autoinject,
        __metadata("design:paramtypes", [aurelia_router_1.Router])
    ], MessengerService);
    exports.MessengerService = MessengerService;
});
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoibWVzc2VuZ2VyLXNlcnZpY2UuanMiLCJzb3VyY2VSb290IjoiIiwic291cmNlcyI6WyJtZXNzZW5nZXItc2VydmljZS50cyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiOzs7Ozs7Ozs7OztJQVVBLElBQWEsZ0JBQWdCO1FBRXpCLHVHQUF1RztRQUN2RyxrREFBa0Q7UUFDbEQsMEJBQW9CLE1BQWM7WUFBbEMsaUJBSUM7WUFKbUIsV0FBTSxHQUFOLE1BQU0sQ0FBUTtZQUszQixZQUFPLEdBQUcsVUFBQyxNQUFjLEVBQUUsU0FBbUIsRUFBRSxRQUFrQjtnQkFDckUsRUFBRSxDQUFDLENBQUMsS0FBSSxDQUFDLEtBQUssSUFBSSxJQUFJLENBQUM7b0JBQ25CLEtBQUksQ0FBQyxLQUFLLEdBQUcsQ0FBQyxDQUFDLGVBQWUsQ0FBQyxDQUFDLEtBQUssQ0FBQyxFQUFFLElBQUksRUFBRSxLQUFLLEVBQUUsQ0FBQyxDQUFDO2dCQUUzRCxLQUFJLENBQUMsYUFBYSxHQUFHLE1BQU0sQ0FBQztnQkFDNUIsS0FBSSxDQUFDLGdCQUFnQixHQUFHLFNBQVMsQ0FBQztnQkFDbEMsS0FBSSxDQUFDLGVBQWUsR0FBRyxRQUFRLENBQUM7Z0JBRWhDLEtBQUksQ0FBQyxLQUFLLENBQUMsR0FBRyxDQUFDLFlBQVksRUFBRSxRQUFRLENBQUMsSUFBSSxDQUFDLFNBQVMsR0FBRyxHQUFHLENBQUMsQ0FBQztnQkFDNUQsS0FBSSxDQUFDLEtBQUssQ0FBQyxJQUFJLEVBQUUsQ0FBQztZQUN0QixDQUFDLENBQUE7WUFDTyxjQUFTLEdBQUc7Z0JBQ2hCLEtBQUksQ0FBQyxLQUFLLENBQUMsSUFBSSxFQUFFLENBQUM7Z0JBQ2xCLEVBQUUsQ0FBQyxDQUFDLEtBQUksQ0FBQyxlQUFlLElBQUksSUFBSSxDQUFDO29CQUM3QixLQUFJLENBQUMsZUFBZSxFQUFFLENBQUM7WUFDL0IsQ0FBQyxDQUFBO1lBQ08sZUFBVSxHQUFHO2dCQUNqQixLQUFJLENBQUMsS0FBSyxDQUFDLElBQUksRUFBRSxDQUFDO2dCQUNsQixFQUFFLENBQUMsQ0FBQyxLQUFJLENBQUMsZ0JBQWdCLElBQUksSUFBSSxDQUFDO29CQUM5QixLQUFJLENBQUMsZ0JBQWdCLEVBQUUsQ0FBQztZQUNoQyxDQUFDLENBQUE7WUFFTSxrQkFBYSxHQUFHO2dCQUNuQixDQUFDLENBQUMsV0FBVyxDQUFDLENBQUMsSUFBSSxDQUFDLEVBQUUsQ0FBQyxDQUFDO2dCQUN4QixLQUFJLENBQUMsVUFBVSxHQUFHLEVBQUUsQ0FBQztnQkFDckIsS0FBSSxDQUFDLE9BQU8sR0FBRyxHQUFHLENBQUM7WUFFdkIsQ0FBQyxDQUFBO1lBQ00sd0JBQW1CLEdBQUcsVUFBQyxRQUEwQjtnQkFDcEQsY0FBYztnQkFDZCxFQUFFLENBQUMsQ0FBQyxLQUFJLENBQUMsMEJBQTBCLEdBQUcsQ0FBQyxDQUFDO29CQUNwQyxLQUFJLENBQUMsMEJBQTBCLEdBQUcsQ0FBQyxDQUFDO2dCQUN4QyxFQUFFLENBQUMsQ0FBQyxRQUFRLENBQUMsSUFBSSxDQUFDLENBQUMsQ0FBQztvQkFDaEIsRUFBRSxDQUFDLENBQUMsUUFBUSxDQUFDLE9BQU8sSUFBSSxJQUFJLElBQUksUUFBUSxDQUFDLE9BQU8sSUFBSSxFQUFFLENBQUM7d0JBQ25ELE1BQU0sQ0FBQyxPQUFPLENBQUMsUUFBUSxDQUFDLE9BQU8sQ0FBQyxDQUFDO2dCQUN6QyxDQUFDO2dCQUNELElBQUksQ0FBQyxDQUFDO29CQUNGLEtBQUksQ0FBQyxTQUFTLENBQUMsUUFBUSxDQUFDLE9BQU8sRUFBRSxJQUFJLENBQUMsQ0FBQztnQkFDM0MsQ0FBQztZQUNMLENBQUMsQ0FBQTtZQUNNLGNBQVMsR0FBRyxVQUFDLElBQVksRUFBRSxPQUFnQjtnQkFDOUMsRUFBRSxDQUFDLENBQUMsQ0FBQyxPQUFPLENBQUMsQ0FBQyxDQUFDO29CQUNYLEVBQUUsQ0FBQyxDQUFDLElBQUksSUFBSSxJQUFJLElBQUksSUFBSSxJQUFJLEVBQUUsQ0FBQzt3QkFDM0IsTUFBTSxDQUFDLE9BQU8sQ0FBQyxJQUFJLENBQUMsQ0FBQztnQkFDN0IsQ0FBQztnQkFDRCxJQUFJLENBQUMsQ0FBQztvQkFDRixNQUFNLENBQUMsS0FBSyxDQUFDLElBQUksQ0FBQyxDQUFDO2dCQUN2QixDQUFDO1lBQ0wsQ0FBQyxDQUFBO1lBbkRHLElBQUksQ0FBQywwQkFBMEIsR0FBRyxDQUFDLENBQUM7UUFFeEMsQ0FBQztRQW1ETSxzREFBMkIsR0FBbEM7WUFDSSxJQUFJLENBQUMsMEJBQTBCLElBQUksQ0FBQyxDQUFDO1FBQ3pDLENBQUM7UUFDTSxzREFBMkIsR0FBbEM7WUFDSSxJQUFJLENBQUMsMEJBQTBCLElBQUksQ0FBQyxDQUFDO1FBQ3pDLENBQUM7UUFDTSxtQ0FBUSxHQUFmLFVBQWdCLFNBQWlCLEVBQUUsSUFBWTtZQUMzQyxFQUFFLENBQUMsQ0FBQyxJQUFJLElBQUksSUFBSSxDQUFDO2dCQUNiLElBQUksQ0FBQyxNQUFNLENBQUMsZUFBZSxDQUFDLFNBQVMsQ0FBQyxDQUFDO1lBQzNDLElBQUk7Z0JBQ0EsSUFBSSxDQUFDLE1BQU0sQ0FBQyxlQUFlLENBQUMsU0FBUyxFQUFFLElBQUksQ0FBQyxDQUFDO1FBQ3JELENBQUM7UUFTTCx1QkFBQztJQUFELENBQUMsQUEvRUQsSUErRUM7SUEvRVksZ0JBQWdCO1FBRDVCLDhCQUFVO3lDQUtxQix1QkFBTTtPQUp6QixnQkFBZ0IsQ0ErRTVCO0lBL0VZLDRDQUFnQiJ9