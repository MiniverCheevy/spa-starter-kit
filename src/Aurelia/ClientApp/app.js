var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (this && this.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t;
    return { next: verb(0), "throw": verb(1), "return": verb(2) };
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (_) try {
            if (f = 1, y && (t = y[op[0] & 2 ? "return" : op[0] ? "throw" : "next"]) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [0, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};
define(["require", "exports", "./api.generated", "aurelia-framework", "./services/current-user-service", "moment"], function (require, exports, Api, aurelia_framework_1, current_user_service_1, Moment) {
    "use strict";
    var App = (function () {
        function App(currentUserService, userListApi) {
            var _this = this;
            this.currentUserService = currentUserService;
            this.userListApi = userListApi;
            this.message = 'Hello World!!!';
            this.handleAutheticationState = function (user) { return __awaiter(_this, void 0, void 0, function () {
                var date, test;
                return __generator(this, function (_a) {
                    switch (_a.label) {
                        case 0:
                            console.log(user.isAuthenticated);
                            Moment();
                            date = Moment(user.refreshTime).format('YYYY-MM-DD HH:mm:ss');
                            console.log("x=" + date);
                            return [4 /*yield*/, this.userListApi.get({})];
                        case 1:
                            test = _a.sent();
                            return [2 /*return*/];
                    }
                });
            }); };
            var user = currentUserService.get().then(this.handleAutheticationState);
        }
        return App;
    }());
    App = __decorate([
        aurelia_framework_1.autoinject(),
        __metadata("design:paramtypes", [current_user_service_1.CurrentUserService, Api.UserList])
    ], App);
    exports.App = App;
});
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiYXBwLmpzIiwic291cmNlUm9vdCI6IiIsInNvdXJjZXMiOlsiYXBwLnRzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiI7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7SUFPQSxJQUFhLEdBQUc7UUFHWixhQUFvQixrQkFBc0MsRUFDbEQsV0FBeUI7WUFEakMsaUJBSUM7WUFKbUIsdUJBQWtCLEdBQWxCLGtCQUFrQixDQUFvQjtZQUNsRCxnQkFBVyxHQUFYLFdBQVcsQ0FBYztZQUhqQyxZQUFPLEdBQUcsZ0JBQWdCLENBQUM7WUFPM0IsNkJBQXdCLEdBQUcsVUFBTSxJQUEwQjtvQkFJbkQsSUFBSTs7Ozs0QkFGUixPQUFPLENBQUMsR0FBRyxDQUFDLElBQUksQ0FBQyxlQUFlLENBQUMsQ0FBQzs0QkFDbEMsTUFBTSxFQUFFLENBQUE7bUNBQ0csTUFBTSxDQUFDLElBQUksQ0FBQyxXQUFXLENBQUMsQ0FBQyxNQUFNLENBQUMscUJBQXFCLENBQUM7NEJBQ2pFLE9BQU8sQ0FBQyxHQUFHLENBQUMsSUFBSSxHQUFDLElBQUksQ0FBQyxDQUFDOzRCQUNaLHFCQUFNLElBQUksQ0FBQyxXQUFXLENBQUMsR0FBRyxDQUFDLEVBQUUsQ0FBQyxFQUFBOzttQ0FBOUIsU0FBOEI7Ozs7aUJBQzVDLENBQUE7WUFWRyxJQUFJLElBQUksR0FBSSxrQkFBa0IsQ0FBQyxHQUFHLEVBQUUsQ0FBQyxJQUFJLENBQUMsSUFBSSxDQUFDLHdCQUF3QixDQUFDLENBQUM7UUFFN0UsQ0FBQztRQVNMLFVBQUM7SUFBRCxDQUFDLEFBaEJELElBZ0JDO0lBaEJZLEdBQUc7UUFEZiw4QkFBVSxFQUFFO3lDQUkrQix5Q0FBa0IsRUFDckMsR0FBRyxDQUFDLFFBQVE7T0FKeEIsR0FBRyxDQWdCZjtJQWhCWSxrQkFBRyJ9