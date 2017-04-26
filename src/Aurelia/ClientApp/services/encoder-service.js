define(["require", "exports"], function (require, exports) {
    "use strict";
    //lifted with love from angularjs 1.0
    //https://raw.githubusercontent.com/angular/angular.js/ddb4ef13a9793b93280e6b5ab2e0593af1c04743/src/Angular.js
    //better handling of arrays (more model binder friendly) than $.params
    var EncoderService = (function () {
        function EncoderService() {
            var _this = this;
            this.isNumber = function (value) {
                try {
                    var num = parseFloat(value);
                    return !isNaN(num) && isFinite(num);
                }
                catch (e) {
                    return false;
                }
            };
            this.isDate = function (value) {
                return toString.call(value) === '[object Date]';
            };
            this.isObject = function (value) {
                // http://jsperf.com/isobject4
                return value !== null && typeof value === 'object';
            };
            this.serializeValue = function (v) {
                if (_this.isObject(v)) {
                    return _this.isDate(v) ? v.toISOString() : v;
                }
                return v;
            };
            this.encodeUriQuery = function (val, pctEncodeSpaces) {
                return encodeURIComponent(val).
                    replace(/%40/gi, '@').
                    replace(/%3A/gi, ':').
                    replace(/%24/g, '$').
                    replace(/%2C/gi, ',').
                    replace(/%3B/gi, ';').
                    replace(/%20/g, (pctEncodeSpaces ? '%20' : '+'));
            };
            this.serializeParams = function (params, prefix) {
                if (!params)
                    return '';
                var parts = [];
                for (var key in params) {
                    var value = params[key];
                    if (value != null) {
                        var isNumber = _this.isNumber(key);
                        var name = '';
                        if (isNumber)
                            name = prefix;
                        else
                            name = prefix == null ? key : prefix + "." + key;
                        if (_this.isObject(value)) {
                            parts.push(_this.serializeParams(value, name));
                        }
                        else if (Array.isArray(value)) {
                            Array.prototype.forEach.call(value, function (v) {
                                if (_this.isObject(value)) {
                                    parts.push(_this.serializeParams(value, name));
                                }
                                else {
                                    parts.push(_this.encodeUriQuery(name, null) + '=' + _this.encodeUriQuery(_this.serializeValue(v), null));
                                }
                            });
                        }
                        else {
                            parts.push(_this.encodeUriQuery(name, null) + '=' + _this.encodeUriQuery(_this.serializeValue(value), null));
                        }
                    }
                }
                return parts.join('&');
            };
        }
        EncoderService.prototype.test = function () {
            var obj = { obj: { a: 'a', b: 'b', c: [1, 2, 3], d: [{ z: 'z' }, { y: 'y' }] } };
            console.log(this.serializeParams(obj));
        };
        return EncoderService;
    }());
    exports.EncoderService = EncoderService;
});
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiZW5jb2Rlci1zZXJ2aWNlLmpzIiwic291cmNlUm9vdCI6IiIsInNvdXJjZXMiOlsiZW5jb2Rlci1zZXJ2aWNlLnRzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiI7O0lBQ0EscUNBQXFDO0lBQ3JDLDhHQUE4RztJQUM5RyxzRUFBc0U7SUFDdEU7UUFBQTtZQUFBLGlCQXdFQztZQXZFVyxhQUFRLEdBQUcsVUFBQyxLQUFLO2dCQUNyQixJQUFJLENBQUM7b0JBQ0QsSUFBSSxHQUFHLEdBQUcsVUFBVSxDQUFDLEtBQUssQ0FBQyxDQUFDO29CQUM1QixNQUFNLENBQUMsQ0FBQyxLQUFLLENBQUMsR0FBRyxDQUFDLElBQUksUUFBUSxDQUFDLEdBQUcsQ0FBQyxDQUFDO2dCQUN4QyxDQUFDO2dCQUNELEtBQUssQ0FBQyxDQUFDLENBQUMsQ0FBQyxDQUFDLENBQUM7b0JBQ1AsTUFBTSxDQUFDLEtBQUssQ0FBQztnQkFDakIsQ0FBQztZQUNMLENBQUMsQ0FBQTtZQUNPLFdBQU0sR0FBRyxVQUFDLEtBQUs7Z0JBQ25CLE1BQU0sQ0FBQyxRQUFRLENBQUMsSUFBSSxDQUFDLEtBQUssQ0FBQyxLQUFLLGVBQWUsQ0FBQztZQUNwRCxDQUFDLENBQUE7WUFDTyxhQUFRLEdBQUcsVUFBQyxLQUFLO2dCQUNyQiw4QkFBOEI7Z0JBQzlCLE1BQU0sQ0FBQyxLQUFLLEtBQUssSUFBSSxJQUFJLE9BQU8sS0FBSyxLQUFLLFFBQVEsQ0FBQztZQUN2RCxDQUFDLENBQUE7WUFFTyxtQkFBYyxHQUFHLFVBQUMsQ0FBQztnQkFDdkIsRUFBRSxDQUFDLENBQUMsS0FBSSxDQUFDLFFBQVEsQ0FBQyxDQUFDLENBQUMsQ0FBQyxDQUFDLENBQUM7b0JBQ25CLE1BQU0sQ0FBQyxLQUFJLENBQUMsTUFBTSxDQUFDLENBQUMsQ0FBQyxHQUFHLENBQUMsQ0FBQyxXQUFXLEVBQUUsR0FBRyxDQUFDLENBQUM7Z0JBQ2hELENBQUM7Z0JBQ0QsTUFBTSxDQUFDLENBQUMsQ0FBQztZQUNiLENBQUMsQ0FBQTtZQUVP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