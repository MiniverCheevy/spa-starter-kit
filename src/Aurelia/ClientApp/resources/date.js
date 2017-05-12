define(["require", "exports", "moment"], function (require, exports, moment) {
    "use strict";
    var DateValueConverter = (function () {
        function DateValueConverter() {
        }
        DateValueConverter.prototype.toView = function (value) {
            if (value == null)
                return '';
            return moment(value).format('M/D/YYYY');
        };
        return DateValueConverter;
    }());
    exports.DateValueConverter = DateValueConverter;
});
//# sourceMappingURL=date.js.map