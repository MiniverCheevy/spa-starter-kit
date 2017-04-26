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
define(["require", "exports", "aurelia-framework", "./services/messenger-service", "./services/ajax-service"], function (require, exports, aurelia_framework_1, messenger_service_1, ajax_service_1) {
    "use strict";
    var ApplicationSetting = (function () {
        function ApplicationSetting(ajaxService, messenger) {
            this.ajaxService = ajaxService;
            this.messenger = messenger;
            this.url = 'api/ApplicationSetting';
        }
        ApplicationSetting.prototype.get = function (request) {
            return __awaiter(this, void 0, void 0, function () {
                var requestBuilder, response, out, err_1, result;
                return __generator(this, function (_a) {
                    switch (_a.label) {
                        case 0:
                            this.messenger.incrementHttpRequestCounter();
                            return [4 /*yield*/, this.ajaxService.buildGetRequest(request, this.url)];
                        case 1:
                            requestBuilder = _a.sent();
                            _a.label = 2;
                        case 2:
                            _a.trys.push([2, 4, , 5]);
                            return [4 /*yield*/, requestBuilder.send()];
                        case 3:
                            response = _a.sent();
                            this.messenger.decrementHttpRequestCounter();
                            out = JSON.parse(response.response);
                            this.messenger.showResponseMessage(out);
                            return [2 /*return*/, out];
                        case 4:
                            err_1 = _a.sent();
                            this.ajaxService.logError(err_1, this.url, new Error().stack);
                            result = {
                                isOk: false,
                                message: err_1.statusText
                            };
                            this.messenger.decrementHttpRequestCounter();
                            this.messenger.showResponseMessage(result);
                            return [2 /*return*/, result];
                        case 5: return [2 /*return*/];
                    }
                });
            });
        };
        ApplicationSetting.prototype.delete = function (request) {
            return __awaiter(this, void 0, void 0, function () {
                var requestBuilder, response, out, err_2, result;
                return __generator(this, function (_a) {
                    switch (_a.label) {
                        case 0:
                            this.messenger.incrementHttpRequestCounter();
                            return [4 /*yield*/, this.ajaxService.buildDeleteRequest(request, this.url)];
                        case 1:
                            requestBuilder = _a.sent();
                            _a.label = 2;
                        case 2:
                            _a.trys.push([2, 4, , 5]);
                            return [4 /*yield*/, requestBuilder.send()];
                        case 3:
                            response = _a.sent();
                            this.messenger.decrementHttpRequestCounter();
                            out = JSON.parse(response.response);
                            this.messenger.showResponseMessage(out);
                            return [2 /*return*/, out];
                        case 4:
                            err_2 = _a.sent();
                            this.ajaxService.logError(err_2, this.url, new Error().stack);
                            result = {
                                isOk: false,
                                message: err_2.statusText
                            };
                            this.messenger.decrementHttpRequestCounter();
                            this.messenger.showResponseMessage(result);
                            return [2 /*return*/, result];
                        case 5: return [2 /*return*/];
                    }
                });
            });
        };
        return ApplicationSetting;
    }());
    ApplicationSetting = __decorate([
        aurelia_framework_1.autoinject(),
        __metadata("design:paramtypes", [ajax_service_1.AjaxService, messenger_service_1.MessengerService])
    ], ApplicationSetting);
    exports.ApplicationSetting = ApplicationSetting;
    var ApplicationSettingDetail = (function () {
        function ApplicationSettingDetail(ajaxService, messenger) {
            this.ajaxService = ajaxService;
            this.messenger = messenger;
            this.url = 'api/ApplicationSettingDetail';
        }
        ApplicationSettingDetail.prototype.put = function (request) {
            return __awaiter(this, void 0, void 0, function () {
                var requestBuilder, response, out, err_3, result;
                return __generator(this, function (_a) {
                    switch (_a.label) {
                        case 0:
                            this.messenger.incrementHttpRequestCounter();
                            return [4 /*yield*/, this.ajaxService.buildPutRequest(request, this.url)];
                        case 1:
                            requestBuilder = _a.sent();
                            _a.label = 2;
                        case 2:
                            _a.trys.push([2, 4, , 5]);
                            return [4 /*yield*/, requestBuilder.send()];
                        case 3:
                            response = _a.sent();
                            this.messenger.decrementHttpRequestCounter();
                            out = JSON.parse(response.response);
                            this.messenger.showResponseMessage(out);
                            return [2 /*return*/, out];
                        case 4:
                            err_3 = _a.sent();
                            this.ajaxService.logError(err_3, this.url, new Error().stack);
                            result = {
                                isOk: false,
                                message: err_3.statusText
                            };
                            this.messenger.decrementHttpRequestCounter();
                            this.messenger.showResponseMessage(result);
                            return [2 /*return*/, result];
                        case 5: return [2 /*return*/];
                    }
                });
            });
        };
        return ApplicationSettingDetail;
    }());
    ApplicationSettingDetail = __decorate([
        aurelia_framework_1.autoinject(),
        __metadata("design:paramtypes", [ajax_service_1.AjaxService, messenger_service_1.MessengerService])
    ], ApplicationSettingDetail);
    exports.ApplicationSettingDetail = ApplicationSettingDetail;
    var ApplicationSettingList = (function () {
        function ApplicationSettingList(ajaxService, messenger) {
            this.ajaxService = ajaxService;
            this.messenger = messenger;
            this.url = 'api/ApplicationSettingList';
        }
        ApplicationSettingList.prototype.get = function (request) {
            return __awaiter(this, void 0, void 0, function () {
                var requestBuilder, response, out, err_4, result;
                return __generator(this, function (_a) {
                    switch (_a.label) {
                        case 0:
                            this.messenger.incrementHttpRequestCounter();
                            return [4 /*yield*/, this.ajaxService.buildGetRequest(request, this.url)];
                        case 1:
                            requestBuilder = _a.sent();
                            _a.label = 2;
                        case 2:
                            _a.trys.push([2, 4, , 5]);
                            return [4 /*yield*/, requestBuilder.send()];
                        case 3:
                            response = _a.sent();
                            this.messenger.decrementHttpRequestCounter();
                            out = JSON.parse(response.response);
                            this.messenger.showResponseMessage(out);
                            return [2 /*return*/, out];
                        case 4:
                            err_4 = _a.sent();
                            this.ajaxService.logError(err_4, this.url, new Error().stack);
                            result = {
                                isOk: false,
                                message: err_4.statusText
                            };
                            this.messenger.decrementHttpRequestCounter();
                            this.messenger.showResponseMessage(result);
                            return [2 /*return*/, result];
                        case 5: return [2 /*return*/];
                    }
                });
            });
        };
        return ApplicationSettingList;
    }());
    ApplicationSettingList = __decorate([
        aurelia_framework_1.autoinject(),
        __metadata("design:paramtypes", [ajax_service_1.AjaxService, messenger_service_1.MessengerService])
    ], ApplicationSettingList);
    exports.ApplicationSettingList = ApplicationSettingList;
    var CurrentUser = (function () {
        function CurrentUser(ajaxService, messenger) {
            this.ajaxService = ajaxService;
            this.messenger = messenger;
            this.url = 'api/CurrentUser';
        }
        CurrentUser.prototype.get = function (request) {
            return __awaiter(this, void 0, void 0, function () {
                var requestBuilder, response, out, err_5, result;
                return __generator(this, function (_a) {
                    switch (_a.label) {
                        case 0:
                            this.messenger.incrementHttpRequestCounter();
                            return [4 /*yield*/, this.ajaxService.buildGetRequest(request, this.url)];
                        case 1:
                            requestBuilder = _a.sent();
                            _a.label = 2;
                        case 2:
                            _a.trys.push([2, 4, , 5]);
                            return [4 /*yield*/, requestBuilder.send()];
                        case 3:
                            response = _a.sent();
                            this.messenger.decrementHttpRequestCounter();
                            out = JSON.parse(response.response);
                            this.messenger.showResponseMessage(out);
                            return [2 /*return*/, out];
                        case 4:
                            err_5 = _a.sent();
                            this.ajaxService.logError(err_5, this.url, new Error().stack);
                            result = {
                                isOk: false,
                                message: err_5.statusText
                            };
                            this.messenger.decrementHttpRequestCounter();
                            this.messenger.showResponseMessage(result);
                            return [2 /*return*/, result];
                        case 5: return [2 /*return*/];
                    }
                });
            });
        };
        return CurrentUser;
    }());
    CurrentUser = __decorate([
        aurelia_framework_1.autoinject(),
        __metadata("design:paramtypes", [ajax_service_1.AjaxService, messenger_service_1.MessengerService])
    ], CurrentUser);
    exports.CurrentUser = CurrentUser;
    var ErrorDetail = (function () {
        function ErrorDetail(ajaxService, messenger) {
            this.ajaxService = ajaxService;
            this.messenger = messenger;
            this.url = 'api/ErrorDetail';
        }
        ErrorDetail.prototype.get = function (request) {
            return __awaiter(this, void 0, void 0, function () {
                var requestBuilder, response, out, err_6, result;
                return __generator(this, function (_a) {
                    switch (_a.label) {
                        case 0:
                            this.messenger.incrementHttpRequestCounter();
                            return [4 /*yield*/, this.ajaxService.buildGetRequest(request, this.url)];
                        case 1:
                            requestBuilder = _a.sent();
                            _a.label = 2;
                        case 2:
                            _a.trys.push([2, 4, , 5]);
                            return [4 /*yield*/, requestBuilder.send()];
                        case 3:
                            response = _a.sent();
                            this.messenger.decrementHttpRequestCounter();
                            out = JSON.parse(response.response);
                            this.messenger.showResponseMessage(out);
                            return [2 /*return*/, out];
                        case 4:
                            err_6 = _a.sent();
                            this.ajaxService.logError(err_6, this.url, new Error().stack);
                            result = {
                                isOk: false,
                                message: err_6.statusText
                            };
                            this.messenger.decrementHttpRequestCounter();
                            this.messenger.showResponseMessage(result);
                            return [2 /*return*/, result];
                        case 5: return [2 /*return*/];
                    }
                });
            });
        };
        return ErrorDetail;
    }());
    ErrorDetail = __decorate([
        aurelia_framework_1.autoinject(),
        __metadata("design:paramtypes", [ajax_service_1.AjaxService, messenger_service_1.MessengerService])
    ], ErrorDetail);
    exports.ErrorDetail = ErrorDetail;
    var ErrorList = (function () {
        function ErrorList(ajaxService, messenger) {
            this.ajaxService = ajaxService;
            this.messenger = messenger;
            this.url = 'api/ErrorList';
        }
        ErrorList.prototype.get = function (request) {
            return __awaiter(this, void 0, void 0, function () {
                var requestBuilder, response, out, err_7, result;
                return __generator(this, function (_a) {
                    switch (_a.label) {
                        case 0:
                            this.messenger.incrementHttpRequestCounter();
                            return [4 /*yield*/, this.ajaxService.buildGetRequest(request, this.url)];
                        case 1:
                            requestBuilder = _a.sent();
                            _a.label = 2;
                        case 2:
                            _a.trys.push([2, 4, , 5]);
                            return [4 /*yield*/, requestBuilder.send()];
                        case 3:
                            response = _a.sent();
                            this.messenger.decrementHttpRequestCounter();
                            out = JSON.parse(response.response);
                            this.messenger.showResponseMessage(out);
                            return [2 /*return*/, out];
                        case 4:
                            err_7 = _a.sent();
                            this.ajaxService.logError(err_7, this.url, new Error().stack);
                            result = {
                                isOk: false,
                                message: err_7.statusText
                            };
                            this.messenger.decrementHttpRequestCounter();
                            this.messenger.showResponseMessage(result);
                            return [2 /*return*/, result];
                        case 5: return [2 /*return*/];
                    }
                });
            });
        };
        return ErrorList;
    }());
    ErrorList = __decorate([
        aurelia_framework_1.autoinject(),
        __metadata("design:paramtypes", [ajax_service_1.AjaxService, messenger_service_1.MessengerService])
    ], ErrorList);
    exports.ErrorList = ErrorList;
    var Lists = (function () {
        function Lists(ajaxService, messenger) {
            this.ajaxService = ajaxService;
            this.messenger = messenger;
            this.url = 'api/Lists';
        }
        Lists.prototype.get = function (request) {
            return __awaiter(this, void 0, void 0, function () {
                var requestBuilder, response, out, err_8, result;
                return __generator(this, function (_a) {
                    switch (_a.label) {
                        case 0:
                            this.messenger.incrementHttpRequestCounter();
                            return [4 /*yield*/, this.ajaxService.buildGetRequest(request, this.url)];
                        case 1:
                            requestBuilder = _a.sent();
                            _a.label = 2;
                        case 2:
                            _a.trys.push([2, 4, , 5]);
                            return [4 /*yield*/, requestBuilder.send()];
                        case 3:
                            response = _a.sent();
                            this.messenger.decrementHttpRequestCounter();
                            out = JSON.parse(response.response);
                            this.messenger.showResponseMessage(out);
                            return [2 /*return*/, out];
                        case 4:
                            err_8 = _a.sent();
                            this.ajaxService.logError(err_8, this.url, new Error().stack);
                            result = {
                                isOk: false,
                                message: err_8.statusText
                            };
                            this.messenger.decrementHttpRequestCounter();
                            this.messenger.showResponseMessage(result);
                            return [2 /*return*/, result];
                        case 5: return [2 /*return*/];
                    }
                });
            });
        };
        return Lists;
    }());
    Lists = __decorate([
        aurelia_framework_1.autoinject(),
        __metadata("design:paramtypes", [ajax_service_1.AjaxService, messenger_service_1.MessengerService])
    ], Lists);
    exports.Lists = Lists;
    var MobileError = (function () {
        function MobileError(ajaxService, messenger) {
            this.ajaxService = ajaxService;
            this.messenger = messenger;
            this.url = 'api/MobileError';
        }
        MobileError.prototype.post = function (request) {
            return __awaiter(this, void 0, void 0, function () {
                var requestBuilder, response, out, err_9, result;
                return __generator(this, function (_a) {
                    switch (_a.label) {
                        case 0:
                            this.messenger.incrementHttpRequestCounter();
                            return [4 /*yield*/, this.ajaxService.buildPostRequest(request, this.url)];
                        case 1:
                            requestBuilder = _a.sent();
                            _a.label = 2;
                        case 2:
                            _a.trys.push([2, 4, , 5]);
                            return [4 /*yield*/, requestBuilder.send()];
                        case 3:
                            response = _a.sent();
                            this.messenger.decrementHttpRequestCounter();
                            out = JSON.parse(response.response);
                            this.messenger.showResponseMessage(out);
                            return [2 /*return*/, out];
                        case 4:
                            err_9 = _a.sent();
                            this.ajaxService.logError(err_9, this.url, new Error().stack);
                            result = {
                                isOk: false,
                                message: err_9.statusText
                            };
                            this.messenger.decrementHttpRequestCounter();
                            this.messenger.showResponseMessage(result);
                            return [2 /*return*/, result];
                        case 5: return [2 /*return*/];
                    }
                });
            });
        };
        return MobileError;
    }());
    MobileError = __decorate([
        aurelia_framework_1.autoinject(),
        __metadata("design:paramtypes", [ajax_service_1.AjaxService, messenger_service_1.MessengerService])
    ], MobileError);
    exports.MobileError = MobileError;
    var UserDetail = (function () {
        function UserDetail(ajaxService, messenger) {
            this.ajaxService = ajaxService;
            this.messenger = messenger;
            this.url = 'api/UserDetail';
        }
        UserDetail.prototype.delete = function (request) {
            return __awaiter(this, void 0, void 0, function () {
                var requestBuilder, response, out, err_10, result;
                return __generator(this, function (_a) {
                    switch (_a.label) {
                        case 0:
                            this.messenger.incrementHttpRequestCounter();
                            return [4 /*yield*/, this.ajaxService.buildDeleteRequest(request, this.url)];
                        case 1:
                            requestBuilder = _a.sent();
                            _a.label = 2;
                        case 2:
                            _a.trys.push([2, 4, , 5]);
                            return [4 /*yield*/, requestBuilder.send()];
                        case 3:
                            response = _a.sent();
                            this.messenger.decrementHttpRequestCounter();
                            out = JSON.parse(response.response);
                            this.messenger.showResponseMessage(out);
                            return [2 /*return*/, out];
                        case 4:
                            err_10 = _a.sent();
                            this.ajaxService.logError(err_10, this.url, new Error().stack);
                            result = {
                                isOk: false,
                                message: err_10.statusText
                            };
                            this.messenger.decrementHttpRequestCounter();
                            this.messenger.showResponseMessage(result);
                            return [2 /*return*/, result];
                        case 5: return [2 /*return*/];
                    }
                });
            });
        };
        UserDetail.prototype.get = function (request) {
            return __awaiter(this, void 0, void 0, function () {
                var requestBuilder, response, out, err_11, result;
                return __generator(this, function (_a) {
                    switch (_a.label) {
                        case 0:
                            this.messenger.incrementHttpRequestCounter();
                            return [4 /*yield*/, this.ajaxService.buildGetRequest(request, this.url)];
                        case 1:
                            requestBuilder = _a.sent();
                            _a.label = 2;
                        case 2:
                            _a.trys.push([2, 4, , 5]);
                            return [4 /*yield*/, requestBuilder.send()];
                        case 3:
                            response = _a.sent();
                            this.messenger.decrementHttpRequestCounter();
                            out = JSON.parse(response.response);
                            this.messenger.showResponseMessage(out);
                            return [2 /*return*/, out];
                        case 4:
                            err_11 = _a.sent();
                            this.ajaxService.logError(err_11, this.url, new Error().stack);
                            result = {
                                isOk: false,
                                message: err_11.statusText
                            };
                            this.messenger.decrementHttpRequestCounter();
                            this.messenger.showResponseMessage(result);
                            return [2 /*return*/, result];
                        case 5: return [2 /*return*/];
                    }
                });
            });
        };
        UserDetail.prototype.put = function (request) {
            return __awaiter(this, void 0, void 0, function () {
                var requestBuilder, response, out, err_12, result;
                return __generator(this, function (_a) {
                    switch (_a.label) {
                        case 0:
                            this.messenger.incrementHttpRequestCounter();
                            return [4 /*yield*/, this.ajaxService.buildPutRequest(request, this.url)];
                        case 1:
                            requestBuilder = _a.sent();
                            _a.label = 2;
                        case 2:
                            _a.trys.push([2, 4, , 5]);
                            return [4 /*yield*/, requestBuilder.send()];
                        case 3:
                            response = _a.sent();
                            this.messenger.decrementHttpRequestCounter();
                            out = JSON.parse(response.response);
                            this.messenger.showResponseMessage(out);
                            return [2 /*return*/, out];
                        case 4:
                            err_12 = _a.sent();
                            this.ajaxService.logError(err_12, this.url, new Error().stack);
                            result = {
                                isOk: false,
                                message: err_12.statusText
                            };
                            this.messenger.decrementHttpRequestCounter();
                            this.messenger.showResponseMessage(result);
                            return [2 /*return*/, result];
                        case 5: return [2 /*return*/];
                    }
                });
            });
        };
        return UserDetail;
    }());
    UserDetail = __decorate([
        aurelia_framework_1.autoinject(),
        __metadata("design:paramtypes", [ajax_service_1.AjaxService, messenger_service_1.MessengerService])
    ], UserDetail);
    exports.UserDetail = UserDetail;
    var UserList = (function () {
        function UserList(ajaxService, messenger) {
            this.ajaxService = ajaxService;
            this.messenger = messenger;
            this.url = 'api/UserList';
        }
        UserList.prototype.get = function (request) {
            return __awaiter(this, void 0, void 0, function () {
                var requestBuilder, response, out, err_13, result;
                return __generator(this, function (_a) {
                    switch (_a.label) {
                        case 0:
                            this.messenger.incrementHttpRequestCounter();
                            return [4 /*yield*/, this.ajaxService.buildGetRequest(request, this.url)];
                        case 1:
                            requestBuilder = _a.sent();
                            _a.label = 2;
                        case 2:
                            _a.trys.push([2, 4, , 5]);
                            return [4 /*yield*/, requestBuilder.send()];
                        case 3:
                            response = _a.sent();
                            this.messenger.decrementHttpRequestCounter();
                            out = JSON.parse(response.response);
                            this.messenger.showResponseMessage(out);
                            return [2 /*return*/, out];
                        case 4:
                            err_13 = _a.sent();
                            this.ajaxService.logError(err_13, this.url, new Error().stack);
                            result = {
                                isOk: false,
                                message: err_13.statusText
                            };
                            this.messenger.decrementHttpRequestCounter();
                            this.messenger.showResponseMessage(result);
                            return [2 /*return*/, result];
                        case 5: return [2 /*return*/];
                    }
                });
            });
        };
        return UserList;
    }());
    UserList = __decorate([
        aurelia_framework_1.autoinject(),
        __metadata("design:paramtypes", [ajax_service_1.AjaxService, messenger_service_1.MessengerService])
    ], UserList);
    exports.UserList = UserList;
});
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiYXBpLmdlbmVyYXRlZC5qcyIsInNvdXJjZVJvb3QiOiIiLCJzb3VyY2VzIjpbImFwaS5nZW5lcmF0ZWQudHMiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6Ijs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7OztJQWNBLElBQWEsa0JBQWtCO1FBSTNCLDRCQUFvQixXQUF3QixFQUFVLFNBQTBCO1lBQTVELGdCQUFXLEdBQVgsV0FBVyxDQUFhO1lBQVUsY0FBUyxHQUFULFNBQVMsQ0FBaUI7WUFGaEYsUUFBRyxHQUFXLHdCQUF3QixDQUFDO1FBSXZDLENBQUM7UUFFWSxnQ0FBRyxHQUFoQixVQUFrQixPQUEwQjs7OENBUXBDLEdBQUcsU0FRSCxNQUFNOzs7OzRCQWJWLElBQUksQ0FBQyxTQUFTLENBQUMsMkJBQTJCLEVBQUUsQ0FBQzs0QkFDeEIscUJBQU0sSUFBSSxDQUFDLFdBQVcsQ0FBQyxlQUFlLENBQUMsT0FBTyxFQUFFLElBQUksQ0FBQyxHQUFHLENBQUMsRUFBQTs7NkNBQXpELFNBQXlEOzs7OzRCQUUvRCxxQkFBTSxjQUFjLENBQUMsSUFBSSxFQUFFLEVBQUE7O3VDQUEzQixTQUEyQjs0QkFDMUMsSUFBSSxDQUFDLFNBQVMsQ0FBQywyQkFBMkIsRUFBRSxDQUFDO2tDQUNqQixJQUFJLENBQUMsS0FBSyxDQUFDLFFBQVEsQ0FBQyxRQUFRLENBQUM7NEJBQ3pELElBQUksQ0FBQyxTQUFTLENBQUMsbUJBQW1CLENBQUMsR0FBRyxDQUFDLENBQUM7NEJBQ3hDLHNCQUFPLEdBQUcsRUFBQzs7OzRCQUlYLElBQUksQ0FBQyxXQUFXLENBQUMsUUFBUSxDQUFDLEtBQUcsRUFBRSxJQUFJLENBQUMsR0FBRyxFQUFRLElBQUksS0FBSyxFQUFHLENBQUMsS0FBSyxDQUFDLENBQUM7cUNBRXREO2dDQUNiLElBQUksRUFBRSxLQUFLO2dDQUNYLE9BQU8sRUFBRSxLQUFHLENBQUMsVUFBVTs2QkFDMUI7NEJBRUQsSUFBSSxDQUFDLFNBQVMsQ0FB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