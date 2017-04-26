import 'isomorphic-fetch';
import { Aurelia } from 'aurelia-framework';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap';
var Promise = require("bluebird");
declare const IS_DEV_BUILD: boolean; // The value is supplied by Webpack during the build
//Configure Bluebird Promises.

export function configure(aurelia: Aurelia) {
    aurelia.use.standardConfiguration()
        .feature('resources');

    if (IS_DEV_BUILD) {
        aurelia.use.developmentLogging();
    }

    aurelia.start().then(() => aurelia.setRoot('app'));
}
(<any>Promise).config({
    warnings: {
        wForgottenReturn: false
    }
});