import * as React from 'react';
import { Models, Services } from './../../root';
import * as Validation from './../../services/validation';

export class Form {
    parent: Form;
    childForms: Form[] = [];
    properties: Models.UIMetadata[] = [];
    isValid = true;
    isDirty = false;

    constructor(public metadata, public model) {
        this.properties = Services.FormsService.getProperties(this.metadata);
    }
    addChildForm(form: Form) {
        form.parent = this;
        this.childForms.push(form);
    }
    setDirty=()=>
    {
        this.isDirty = true;
        if (this.parent != null)
            this.parent.setDirty();
    }
    onChange=(event)=> {
        this.setDirty();
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
