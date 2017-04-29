import 'isomorphic-fetch';
import { Aurelia, PLATFORM } from "aurelia-framework";
var Promise = require("bluebird");
declare const IS_DEV_BUILD: boolean; // The value is supplied by Webpack during the build
//Configure Bluebird Promises.

export function configure(aurelia: Aurelia) {
    aurelia.use.standardConfiguration()
        .feature('resources');

    if (IS_DEV_BUILD) {
        aurelia.use.developmentLogging();
    }

    aurelia.start()
        .then(() => aurelia.setRoot(PLATFORM.moduleName("app")));
}
(<any>Promise).config({
    warnings: {
        wForgottenReturn: false
    }
});