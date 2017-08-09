import * as React from 'react';
import { Models, Services } from './../../root';
import * as Validation from './../../services/validation';
import { InputComponent } from './input-component';


export class Form {
    parent: Form;
    childForms: Form[] = [];
    properties: Models.UIMetadata[] = [];
    isDirty = false;
    callback: (form: Form) => void

    constructor(public metadata) {
        this.properties = Services.FormsService.getProperties(this.metadata);
    }
    get isValid() {

        for (let key in this.metadata) {

            if (this.metadata.hasOwnProperty(key)) {
                var itemMetadata = this.metadata[key] as Models.UIMetadata;
                if (itemMetadata.control != null) {
                    var control = itemMetadata.control as InputComponent;
                    var state = control.helper.getState();
                    var value = state.rawValue as any;
                    var validationResult = Services.ValidationService.validate({ value: value, metadata: itemMetadata });
                    //console.log({ key: key, value: value, isValid: validationResult.isValid, metadata: itemMetadata });
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
    setDirty = () => {
        this.isDirty = true;
        if (this.parent != null)
            this.parent.setDirty();
    }

    configureMetadata(input: InputComponent) {
        var name = input.props.name;
        var metadata = this.metadata[name];
        if (metadata != null && metadata.conrol == null) {
            metadata.control = input;

        }
        return metadata;
    }

}
