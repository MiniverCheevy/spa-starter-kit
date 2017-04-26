define(["require", "exports", "isomorphic-fetch", "bootstrap/dist/css/bootstrap.css", "bootstrap"], function (require, exports) {
    "use strict";
    var Promise = require("bluebird");
    //Configure Bluebird Promises.
    function configure(aurelia) {
        aurelia.use.standardConfiguration()
            .feature('resources');
        if (IS_DEV_BUILD) {
            aurelia.use.developmentLogging();
        }
        aurelia.start().then(function () { return aurelia.setRoot('app'); });
    }
    exports.configure = configure;
    Promise.config({
        warnings: {
            wForgottenReturn: false
        }
    });
});
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiYm9vdC5qcyIsInNvdXJjZVJvb3QiOiIiLCJzb3VyY2VzIjpbImJvb3QudHMiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6Ijs7SUFJQSxJQUFJLE9BQU8sR0FBRyxPQUFPLENBQUMsVUFBVSxDQUFDLENBQUM7SUFFbEMsOEJBQThCO0lBRTlCLG1CQUEwQixPQUFnQjtRQUN0QyxPQUFPLENBQUMsR0FBRyxDQUFDLHFCQUFxQixFQUFFO2FBQzlCLE9BQU8sQ0FBQyxXQUFXLENBQUMsQ0FBQztRQUUxQixFQUFFLENBQUMsQ0FBQyxZQUFZLENBQUMsQ0FBQyxDQUFDO1lBQ2YsT0FBTyxDQUFDLEdBQUcsQ0FBQyxrQkFBa0IsRUFBRSxDQUFDO1FBQ3JDLENBQUM7UUFFRCxPQUFPLENBQUMsS0FBSyxFQUFFLENBQUMsSUFBSSxDQUFDLGNBQU0sT0FBQSxPQUFPLENBQUMsT0FBTyxDQUFDLEtBQUssQ0FBQyxFQUF0QixDQUFzQixDQUFDLENBQUM7SUFDdkQsQ0FBQztJQVRELDhCQVNDO0lBQ0ssT0FBUSxDQUFDLE1BQU0sQ0FBQztRQUNsQixRQUFRLEVBQUU7WUFDTixnQkFBZ0IsRUFBRSxLQUFLO1NBQzFCO0tBQ0osQ0FBQyxDQUFDIn0=