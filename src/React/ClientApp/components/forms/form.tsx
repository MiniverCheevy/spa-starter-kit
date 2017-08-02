import * as React from 'react';
import { Models, Services } from './../../root';
import * as Validation from './../../services/validation';
import { InputComponent } from './input-component';


export class Form {
    parent: Form;
    childForms: Form[] = [];
    properties: Models.UIMetadata[] = [];
    isValid = true;
    isDirty = false;
    version = 0;
    callback: (form: Form) => void
    
    constructor(public metadata) {
        this.properties = Services.FormsService.getProperties(this.metadata);
        this.version = this.version + 1;
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

    configureMetadata(input: InputComponent)
    {
        var name = input.props.name;
        var metadata = this.metadata[name];
        if (metadata != null)
        {
            metadata.control = input;
        }
        return metadata;
    }
   
}
