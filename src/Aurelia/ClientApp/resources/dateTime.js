define(["require", "exports", "moment"], function (require, exports, moment) {
    "use strict";
    var DateTimeValueConverter = (function () {
        function DateTimeValueConverter() {
        }
        DateTimeValueConverter.prototype.toView = function (value) {
            if (value == null)
                return '';
            return moment(value).format('M/D/YYYY hh:mm');
        };
        return DateTimeValueConverter;
    }());
    exports.DateTimeValueConverter = DateTimeValueConverter;
});
//# sourceMappingURL=dateTime.js.map