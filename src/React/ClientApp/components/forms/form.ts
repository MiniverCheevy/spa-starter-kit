import * as React from 'react';
import { Models, Services } from './../../root';
import * as Validation from './../../services/validation';

export class Form {
    childForms: Form[] = [];
    properties: Models.UIMetadata[] = [];
    isValid = true;
    isDirty = false;

    constructor(public metadata, public model) {
        if (this.properties.length == 0)
            this.properties = Services.FormsService.getProperties(this.metadata);
    }
    addChildForm(form: Form) {
        this.childForms.push(form);
    }
    onChange(event) {
        this.updateProperty(event.target.name, event.target.value)
    }
    updateProperty(key, value) {
        this.model[key] = value
    }
    getValue(key: string)
    {
        return this.model[key];
    }
    getMetadata(key: string)
    {
        return this.metadata[key];
    }
}
