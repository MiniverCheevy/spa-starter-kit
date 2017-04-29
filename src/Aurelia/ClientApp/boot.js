define(["require", "exports", "aurelia-framework", "isomorphic-fetch"], function (require, exports, aurelia_framework_1) {
    "use strict";
    var Promise = require("bluebird");
    //Configure Bluebird Promises.
    function configure(aurelia) {
        aurelia.use.standardConfiguration()
            .feature('resources');
        if (IS_DEV_BUILD) {
            aurelia.use.developmentLogging();
        }
        aurelia.start()
            .then(function () { return aurelia.setRoot(aurelia_framework_1.PLATFORM.moduleName("app")); });
    }
    exports.configure = configure;
    Promise.config({
        warnings: {
            wForgottenReturn: false
        }
    });
});
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiYm9vdC5qcyIsInNvdXJjZVJvb3QiOiIiLCJzb3VyY2VzIjpbImJvb3QudHMiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6Ijs7SUFFQSxJQUFJLE9BQU8sR0FBRyxPQUFPLENBQUMsVUFBVSxDQUFDLENBQUM7SUFFbEMsOEJBQThCO0lBRTlCLG1CQUEwQixPQUFnQjtRQUN0QyxPQUFPLENBQUMsR0FBRyxDQUFDLHFCQUFxQixFQUFFO2FBQzlCLE9BQU8sQ0FBQyxXQUFXLENBQUMsQ0FBQztRQUUxQixFQUFFLENBQUMsQ0FBQyxZQUFZLENBQUMsQ0FBQyxDQUFDO1lBQ2YsT0FBTyxDQUFDLEdBQUcsQ0FBQyxrQkFBa0IsRUFBRSxDQUFDO1FBQ3JDLENBQUM7UUFFRCxPQUFPLENBQUMsS0FBSyxFQUFFO2FBQ1YsSUFBSSxDQUFDLGNBQU0sT0FBQSxPQUFPLENBQUMsT0FBTyxDQUFDLDRCQUFRLENBQUMsVUFBVSxDQUFDLEtBQUssQ0FBQyxDQUFDLEVBQTNDLENBQTJDLENBQUMsQ0FBQztJQUNqRSxDQUFDO0lBVkQsOEJBVUM7SUFDSyxPQUFRLENBQUMsTUFBTSxDQUFDO1FBQ2xCLFFBQVEsRUFBRTtZQUNOLGdCQUFnQixFQUFFLEtBQUs7U0FDMUI7S0FDSixDQUFDLENBQUMifQ==