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
define(["require", "exports", "aurelia-http-client", "aurelia-framework", "./messenger-service"], function (require, exports, aurelia_http_client_1, aurelia_framework_1, messenger_service_1) {
    "use strict";
    var CurrentUserService = (function () {
        //Do not add additional local dependencies here or you will likely cause circular references and be sad
        //third party stuff (aurelia,jquery, etc) is fine
        function CurrentUserService(http, messenger) {
            this.http = http;
            this.messenger = messenger;
            this.url = 'api/CurrentUser';
        }
        CurrentUserService.prototype.get = function () {
            return __awaiter(this, void 0, void 0, function () {
                return __generator(this, function (_a) {
                    if (this.user != null && this.user.refreshTime != null) {
                        if (this.user.refreshTime > new Date()) {
                            console.log('RefreshUser');
                            this.user = null;
                        }
                    }
                    if (this.user == null) {
                        this.user = this.getUser();
                        return [2 /*return*/, this.user];
                    }
                    return [2 /*return*/, this.user];
                });
            });
        };
        CurrentUserService.prototype.getUser = function () {
            return __awaiter(this, void 0, void 0, function () {
                var requestBuilder, httpResponse, response, error_1;
                return __generator(this, function (_a) {
                    switch (_a.label) {
                        case 0:
                            _a.trys.push([0, 2, , 3]);
                            console.log('Query User');
                            requestBuilder = this.http.createRequest(this.url).asGet()
                                .withHeader('Content-Type', 'application/json; charset=utf-8');
                            return [4 /*yield*/, requestBuilder.send()];
                        case 1:
                            httpResponse = _a.sent();
                            response = JSON.parse(httpResponse.response);
                            if (response.isOk) {
                                this.user = response.data;
                                return [2 /*return*/, this.user];
                            }
                            else {
                                this.messenger.showResponseMessage(response);
                            }
                            return [3 /*break*/, 3];
                        case 2:
                            error_1 = _a.sent();
                            this.messenger.showResponseMessage({ isOk: false, message: error_1.msg });
                            return [2 /*return*/, null];
                        case 3: return [2 /*return*/];
                    }
                });
            });
        };
        return CurrentUserService;
    }());
    CurrentUserService = __decorate([
        aurelia_framework_1.autoinject(),
        __metadata("design:paramtypes", [aurelia_http_client_1.HttpClient,
            messenger_service_1.MessengerService])
    ], CurrentUserService);
    exports.CurrentUserService = CurrentUserService;
});
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiY3VycmVudC11c2VyLXNlcnZpY2UuanMiLCJzb3VyY2VSb290IjoiIiwic291cmNlcyI6WyJjdXJyZW50LXVzZXItc2VydmljZS50cyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiOzs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7O0lBVUEsSUFBYSxrQkFBa0I7UUFNM0IsdUdBQXVHO1FBQ3ZHLGlEQUFpRDtRQUNqRCw0QkFBb0IsSUFBZ0IsRUFDeEIsU0FBMkI7WUFEbkIsU0FBSSxHQUFKLElBQUksQ0FBWTtZQUN4QixjQUFTLEdBQVQsU0FBUyxDQUFrQjtZQVB2QyxRQUFHLEdBQVcsaUJBQWlCLENBQUM7UUFVaEMsQ0FBQztRQUVZLGdDQUFHLEdBQWhCOzs7b0JBRUksRUFBRSxDQUFDLENBQUMsSUFBSSxDQUFDLElBQUksSUFBSSxJQUFJLElBQVUsSUFBSSxDQUFDLElBQUssQ0FBQyxXQUFXLElBQUksSUFBSSxDQUFDLENBQUMsQ0FBQzt3QkFDNUQsRUFBRSxDQUFDLENBQU8sSUFBSSxDQUFDLElBQUssQ0FBQyxXQUFXLEdBQUcsSUFBSSxJQUFJLEVBQUUsQ0FBQyxDQUFDLENBQUM7NEJBQzVDLE9BQU8sQ0FBQyxHQUFHLENBQUMsYUFBYSxDQUFDLENBQUM7NEJBQzNCLElBQUksQ0FBQyxJQUFJLEdBQUcsSUFBSSxDQUFDO3dCQUNyQixDQUFDO29CQUNMLENBQUM7b0JBQ0QsRUFBRSxDQUFDLENBQUMsSUFBSSxDQUFDLElBQUksSUFBSSxJQUFJLENBQUMsQ0FBQyxDQUFDO3dCQUNwQixJQUFJLENBQUMsSUFBSSxHQUFHLElBQUksQ0FBQyxPQUFPLEVBQUUsQ0FBQzt3QkFDM0IsTUFBTSxnQkFBQyxJQUFJLENBQUMsSUFBSSxFQUFDO29CQUNyQixDQUFDO29CQUdELHNCQUFPLElBQUksQ0FBQyxJQUFJLEVBQUM7OztTQUNwQjtRQUdhLG9DQUFPLEdBQXJCOztvQkFJWSxjQUFjLGdCQUlkLFFBQVE7Ozs7OzRCQUxaLE9BQU8sQ0FBQyxHQUFHLENBQUMsWUFBWSxDQUFDLENBQUM7NkNBQ0wsSUFBSSxDQUFDLElBQUksQ0FBQyxhQUFhLENBQUMsSUFBSSxDQUFDLEdBQUcsQ0FBQyxDQUFDLEtBQUssRUFBRTtpQ0FDekQsVUFBVSxDQUFDLGNBQWMsRUFBRSxpQ0FBaUMsQ0FBQzs0QkFFL0MscUJBQU0sY0FBYyxDQUFDLElBQUksRUFBRSxFQUFBOzsyQ0FBM0IsU0FBMkI7dUNBQ0MsSUFBSSxDQUFDLEtBQUssQ0FBQyxZQUFZLENBQUMsUUFBUSxDQUFDOzRCQUNoRixFQUFFLENBQUMsQ0FBQyxRQUFRLENBQUMsSUFBSSxDQUFDLENBQUMsQ0FBQztnQ0FDaEIsSUFBSSxDQUFDLElBQUksR0FBRyxRQUFRLENBQUMsSUFBSSxDQUFDO2dDQUMxQixNQUFNLGdCQUFDLElBQUksQ0FBQyxJQUFJLEVBQUM7NEJBQ3JCLENBQUM7NEJBQ0QsSUFBSSxDQUFDLENBQUM7Z0NBQ0YsSUFBSSxDQUFDLFNBQVMsQ0FBQyxtQkFBbUIsQ0FBQyxRQUFRLENBQUMsQ0FBQzs0QkFDakQsQ0FBQzs7Ozs0QkFHRCxJQUFJLENBQUMsU0FBUyxDQUFDLG1CQUFtQixDQUFDLEVBQUUsSUFBSSxFQUFFLEtBQUssRUFBRSxPQUFPLEVBQUUsT0FBSyxDQUFDLEdBQUcsRUFBRSxDQUFDLENBQUM7NEJBQ3hFLHNCQUFPLElBQUksRUFBQzs7Ozs7U0FFbkI7UUFPTCx5QkFBQztJQUFELENBQUMsQUE1REQsSUE0REM7SUE1RFksa0JBQWtCO1FBRDlCLDhCQUFVLEVBQUU7eUNBU2lCLGdDQUFVO1lBQ2Isb0NBQWdCO09BVDlCLGtCQUFrQixDQTREOUI7SUE1RFksZ0RBQWtCIn0=