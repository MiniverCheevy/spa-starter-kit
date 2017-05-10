var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
define(["require", "exports", "aurelia-framework", "./../services/messenger-service"], function (require, exports, aurelia_framework_1, messenger_service_1) {
    "use strict";
    var Confirm = (function () {
        function Confirm(messengerService) {
            this.messengerService = messengerService;
        }
        Confirm.prototype.activate = function (data) {
            this.prompt = data;
        };
        return Confirm;
    }());
    Confirm = __decorate([
        aurelia_framework_1.customElement('confirm'),
        aurelia_framework_1.autoinject,
        __metadata("design:paramtypes", [messenger_service_1.MessengerService])
    ], Confirm);
    exports.Confirm = Confirm;
});
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiY29uZmlybS5qcyIsInNvdXJjZVJvb3QiOiIiLCJzb3VyY2VzIjpbImNvbmZpcm0udHMiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6Ijs7Ozs7Ozs7Ozs7SUFNQSxJQUFhLE9BQU87UUFHaEIsaUJBQW9CLGdCQUFrQztZQUFsQyxxQkFBZ0IsR0FBaEIsZ0JBQWdCLENBQWtCO1FBRXRELENBQUM7UUFFRCwwQkFBUSxHQUFSLFVBQVMsSUFBSTtZQUNULElBQUksQ0FBQyxNQUFNLEdBQUcsSUFBSSxDQUFDO1FBQ3ZCLENBQUM7UUFDTCxjQUFDO0lBQUQsQ0FBQyxBQVZELElBVUM7SUFWWSxPQUFPO1FBRm5CLGlDQUFhLENBQUMsU0FBUyxDQUFDO1FBQ3hCLDhCQUFVO3lDQUkrQixvQ0FBZ0I7T0FIN0MsT0FBTyxDQVVuQjtJQVZZLDBCQUFPIn0=