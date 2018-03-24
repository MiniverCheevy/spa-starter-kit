import * as React from 'react';
import { Models, Services } from './../../root';
import * as Validation from './../../services/validation';
import { InputComponent } from './input-component';


export class Form {
    parent: Form;
    childForms: Form[] = [];
    properties: Models.UIMetadata[] = [];
    isDirty = false;
    callback: (form: Form) => void;

    constructor(public metadata) {
        this.properties = Services.FormsService.getProperties(this.metadata);
    }
    get isValid() {

        for (let key in this.metadata) {

            if (this.metadata.hasOwnProperty(key)) {
                const itemMetadata = this.metadata[key] as Models.UIMetadata;
                if (itemMetadata.control != null) {
                    const control = itemMetadata.control as InputComponent;
                    const state = control.helper.getState();
                    const value = state.rawValue as any;
                    const validationResult = Services.ValidationService.validate({ value: value, metadata: itemMetadata });
                    
                    if (!validationResult.isValid)
                        return false;
                }
            }
        }
        return true;
    }
    addChildForm(form: Form) {
        form.parent = this;
        this.childForms.push(form);
    }
    clearDirty = () => {
        this.isDirty = false;
    }
    setDirty = () => {
        this.isDirty = true;
        if (this.parent != null)
            this.parent.setDirty();
    }

    configureMetadata(input: InputComponent) {
        const name = input.props.name;
        const metadata = this.metadata[name];
        if (metadata != null && metadata.conrol == null) {
            metadata.control = input;

        }
        return metadata;
    }

    clearPreviousValidationResults() {
       for(let key in this.metadata) {

            if (this.metadata.hasOwnProperty(key)) {
                const itemMetadata = this.metadata[key] as Models.UIMetadata;
                itemMetadata.control = null;
           }
       }
    }

}
