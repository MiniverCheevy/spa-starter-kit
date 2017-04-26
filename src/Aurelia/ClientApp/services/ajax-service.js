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
define(["require", "exports", "aurelia-http-client", "aurelia-framework", "./encoder-service", "./current-user-service", "./messenger-service", "jquery"], function (require, exports, aurelia_http_client_1, aurelia_framework_1, encoder_service_1, current_user_service_1, messenger_service_1, $) {
    "use strict";
    var AjaxService = (function () {
        //Do not add additional local dependencies here or you will likely cause circular references and be sad
        //third party stuff (aurelia,jquery, etc) is fine
        function AjaxService(http, messenger, encoder, currentUserService) {
            var _this = this;
            this.http = http;
            this.messenger = messenger;
            this.encoder = encoder;
            this.currentUserService = currentUserService;
            this.addHeaders = function (requestBuilder, user) {
                return requestBuilder
                    .withHeader('Content-Type', 'application/json; charset=utf-8')
                    .withHeader('Token', user.token);
            };
            this.buildGetRequest = function (request, url) { return __awaiter(_this, void 0, void 0, function () {
                var user, params, requestBuilder;
                return __generator(this, function (_a) {
                    switch (_a.label) {
                        case 0: return [4 /*yield*/, this.currentUserService.get()];
                        case 1:
                            user = _a.sent();
                            params = this.encoder.serializeParams(request);
                            requestBuilder = this.http.createRequest(url + '?' + params).asGet();
                            return [2 /*return*/, this.addHeaders(requestBuilder, user)];
                    }
                });
            }); };
            this.buildPutRequest = function (request, url) { return __awaiter(_this, void 0, void 0, function () {
                var user, requestBuilder;
                return __generator(this, function (_a) {
                    switch (_a.label) {
                        case 0: return [4 /*yield*/, this.currentUserService.get()];
                        case 1:
                            user = _a.sent();
                            requestBuilder = this.http.createRequest(url)
                                .asPut().withContent(request);
                            return [2 /*return*/, this.addHeaders(requestBuilder, user)];
                    }
                });
            }); };
            this.buildPostRequest = function (request, url) { return __awaiter(_this, void 0, void 0, function () {
                var user, requestBuilder;
                return __generator(this, function (_a) {
                    switch (_a.label) {
                        case 0: return [4 /*yield*/, this.currentUserService.get()];
                        case 1:
                            user = _a.sent();
                            requestBuilder = this.http.createRequest(url)
                                .asPost().withContent(request);
                            return [2 /*return*/, this.addHeaders(requestBuilder, user)];
                    }
                });
            }); };
            this.buildDeleteRequest = function (request, url) { return __awaiter(_this, void 0, void 0, function () {
                var user, params, requestBuilder;
                return __generator(this, function (_a) {
                    switch (_a.label) {
                        case 0: return [4 /*yield*/, this.currentUserService.get()];
                        case 1:
                            user = _a.sent();
                            params = this.encoder.serializeParams(request);
                            requestBuilder = this.http.createRequest(url + '?' + params).asDelete();
                            return [2 /*return*/, this.addHeaders(requestBuilder, user)];
                    }
                });
            }); };
        }
        AjaxService.prototype.logError = function (err, url, stack) {
            var message = err.statusText + ' (' + err.statusCode + ')';
            console.error(err);
            var error = {};
            error.errorMsg = message;
            error.ErrorObject = stack;
            error.url = url;
            $.ajax({
                type: "POST",
                url: "api/clienterror",
                data: error
            });
        };
        return AjaxService;
    }());
    AjaxService = __decorate([
        aurelia_framework_1.autoinject(),
        __metadata("design:paramtypes", [aurelia_http_client_1.HttpClient,
            messenger_service_1.MessengerService,
            encoder_service_1.EncoderService,
            current_user_service_1.CurrentUserService])
    ], AjaxService);
    exports.AjaxService = AjaxService;
});
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiYWpheC1zZXJ2aWNlLmpzIiwic291cmNlUm9vdCI6IiIsInNvdXJjZXMiOlsiYWpheC1zZXJ2aWNlLnRzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiI7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7SUFTQSxJQUFhLFdBQVc7UUFFcEIsdUdBQXVHO1FBQ3ZHLGlEQUFpRDtRQUNqRCxxQkFBb0IsSUFBZ0IsRUFDeEIsU0FBMkIsRUFDM0IsT0FBdUIsRUFDdkIsa0JBQXNDO1lBSGxELGlCQUlDO1lBSm1CLFNBQUksR0FBSixJQUFJLENBQVk7WUFDeEIsY0FBUyxHQUFULFNBQVMsQ0FBa0I7WUFDM0IsWUFBTyxHQUFQLE9BQU8sQ0FBZ0I7WUFDdkIsdUJBQWtCLEdBQWxCLGtCQUFrQixDQUFvQjtZQUcxQyxlQUFVLEdBQUcsVUFBQyxjQUE4QixFQUFFLElBQTBCO2dCQUM1RSxNQUFNLENBQUMsY0FBYztxQkFDaEIsVUFBVSxDQUFDLGNBQWMsRUFBRSxpQ0FBaUMsQ0FBQztxQkFDN0QsVUFBVSxDQUFDLE9BQU8sRUFBRSxJQUFJLENBQUMsS0FBSyxDQUFDLENBQUM7WUFDekMsQ0FBQyxDQUFBO1lBRU0sb0JBQWUsR0FBRyxVQUFPLE9BQU8sRUFBRSxHQUFHOzBCQUdwQyxNQUFNLEVBQ04sY0FBYzs7O2dDQUZQLHFCQUFNLElBQUksQ0FBQyxrQkFBa0IsQ0FBQyxHQUFHLEVBQUUsRUFBQTs7bUNBQW5DLFNBQW1DO3FDQUNqQyxJQUFJLENBQUMsT0FBTyxDQUFDLGVBQWUsQ0FBQyxPQUFPLENBQUM7NkNBQzdCLElBQUksQ0FBQyxJQUFJLENBQUMsYUFBYSxDQUFDLEdBQUcsR0FBRyxHQUFHLEdBQUcsTUFBTSxDQUFDLENBQUMsS0FBSyxFQUFFOzRCQUN4RSxzQkFBTyxJQUFJLENBQUMsVUFBVSxDQUFDLGNBQWMsRUFBRSxJQUFJLENBQUMsRUFBQzs7O2lCQUVoRCxDQUFBO1lBQ00sb0JBQWUsR0FBRyxVQUFPLE9BQU8sRUFBRSxHQUFHOzBCQUdwQyxjQUFjOzs7Z0NBRFAscUJBQU0sSUFBSSxDQUFDLGtCQUFrQixDQUFDLEdBQUcsRUFBRSxFQUFBOzttQ0FBbkMsU0FBbUM7NkNBQ3pCLElBQUksQ0FBQyxJQUFJLENBQUMsYUFBYSxDQUFDLEdBQUcsQ0FBQztpQ0FDNUMsS0FBSyxFQUFFLENBQUMsV0FBVyxDQUFDLE9BQU8sQ0FBQzs0QkFDakMsc0JBQU8sSUFBSSxDQUFDLFVBQVUsQ0FBQyxjQUFjLEVBQUUsSUFBSSxDQUFDLEVBQUM7OztpQkFDaEQsQ0FBQTtZQUNNLHFCQUFnQixHQUFHLFVBQU8sT0FBTyxFQUFFLEdBQUc7MEJBR3JDLGNBQWM7OztnQ0FEUCxxQkFBTSxJQUFJLENBQUMsa0JBQWtCLENBQUMsR0FBRyxFQUFFLEVBQUE7O21DQUFuQyxTQUFtQzs2Q0FDekIsSUFBSSxDQUFDLElBQUksQ0FBQyxhQUFhLENBQUMsR0FBRyxDQUFDO2lDQUM1QyxNQUFNLEVBQUUsQ0FBQyxXQUFXLENBQUMsT0FBTyxDQUFDOzRCQUNsQyxzQkFBTyxJQUFJLENBQUMsVUFBVSxDQUFDLGNBQWMsRUFBRSxJQUFJLENBQUMsRUFBQzs7O2lCQUNoRCxDQUFBO1lBQ00sdUJBQWtCLEdBQUcsVUFBTyxPQUFPLEVBQUUsR0FBRzswQkFHdkMsTUFBTSxFQUNOLGNBQWM7OztnQ0FGUCxxQkFBTSxJQUFJLENBQUMsa0JBQWtCLENBQUMsR0FBRyxFQUFFLEVBQUE7O21DQUFuQyxTQUFtQztxQ0FDakMsSUFBSSxDQUFDLE9BQU8sQ0FBQyxlQUFlLENBQUMsT0FBTyxDQUFDOzZDQUM3QixJQUFJLENBQUMsSUFBSSxDQUFDLGFBQWEsQ0FBQyxHQUFHLEdBQUcsR0FBRyxHQUFHLE1BQU0sQ0FBQyxDQUFDLFFBQVEsRUFBRTs0QkFDM0Usc0JBQU8sSUFBSSxDQUFDLFVBQVUsQ0FBQyxjQUFjLEVBQUUsSUFBSSxDQUFDLEVBQUM7OztpQkFDaEQsQ0FBQTtRQXBDRCxDQUFDO1FBcUNNLDhCQUFRLEdBQWYsVUFBZ0IsR0FBRyxFQUFFLEdBQUcsRUFBRSxLQUFLO1lBQzNCLElBQUksT0FBTyxHQUFHLEdBQUcsQ0FBQyxVQUFVLEdBQUcsSUFBSSxHQUFHLEdBQUcsQ0FBQyxVQUFVLEdBQUcsR0FBRyxDQUFDO1lBQzNELE9BQU8sQ0FBQyxLQUFLLENBQUMsR0FBRyxDQUFDLENBQUM7WUFDbkIsSUFBSSxLQUFLLEdBQVEsRUFBRSxDQUFDO1lBQ3BCLEtBQUssQ0FBQyxRQUFRLEdBQUcsT0FBTyxDQUFDO1lBQ3pCLEtBQUssQ0FBQyxXQUFXLEdBQUcsS0FBSyxDQUFDO1lBQzFCLEtBQUssQ0FBQyxHQUFHLEdBQUcsR0FBRyxDQUFDO1lBQ1YsQ0FBRSxDQUFDLElBQUksQ0FBQztnQkFDVixJQUFJLEVBQUUsTUFBTTtnQkFDWixHQUFHLEVBQUUsaUJBQWlCO2dCQUN0QixJQUFJLEVBQUUsS0FBSzthQUNkLENBQUMsQ0FBQztRQUNQLENBQUM7UUFDTCxrQkFBQztJQUFELENBQUMsQUExREQsSUEwREM7SUExRFksV0FBVztRQUR2Qiw4QkFBVSxFQUFFO3lDQUtpQixnQ0FBVTtZQUNiLG9DQUFnQjtZQUNsQixnQ0FBYztZQUNILHlDQUFrQjtPQVB6QyxXQUFXLENBMER2QjtJQTFEWSxrQ0FBVyJ9