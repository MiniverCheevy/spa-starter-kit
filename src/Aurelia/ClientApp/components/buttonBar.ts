import { bindable, inject, customElement } from 'aurelia-framework';
import * as Models from './../models.generated';

@customElement('button-bar')
@inject(Element)
export class ButtonBar {
    @bindable printDelegate: any;
    @bindable addLink: string;
    @bindable deleteDelegate: string;
    @bindable cancelLink: string;
    @bindable save: boolean = false;

    constructor(element) {
        this.element = element;

    }

    element; 

    attach() {

    }

}