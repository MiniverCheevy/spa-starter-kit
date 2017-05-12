import 'isomorphic-fetch';
import { Aurelia, PLATFORM } from "aurelia-framework";
var Promise = require("bluebird");
declare const IS_DEV_BUILD: boolean; // The value is supplied by Webpack during the build
//Configure Bluebird Promises.

export function configure(aurelia: Aurelia) {
    aurelia.use.standardConfiguration();
        //.plugin('aurelia-html-import-template-loader')
        //.feature(PLATFORM.moduleName('resources/index'));

    if (IS_DEV_BUILD) {
        console.log('dev');
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