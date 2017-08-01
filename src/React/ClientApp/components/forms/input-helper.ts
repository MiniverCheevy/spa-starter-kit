import { InputComponentProps, InputComponent } from './input-component';
import * as React from 'react';
import { Models, Services } from './../../root';
import * as Validation from './../../services/validation';
export class InputHelper {
    constructor(public input: InputComponent) {

    }
    parseProps() {
        this.input.name = this.input.props.name;
        if (this.input.props.noLabel != null)
            this.input.nolabel = this.input.props.noLabel;
        if (this.input.props.fullWidth != null)
            this.input.fullWidth = this.input.props.fullWidth;
        if (this.input.props.label != null)
            this.input.labelText = this.input.props.label;
        if (this.input.props.value != null)
            this.input.internalValue = this.input.props.value;
        if (this.input.props.readOnly != null)
            this.input.isReadOnly = this.input.props.readOnly;
        if (this.input.props.lines != null) 
            this.input.lines = this.input.props.lines;

        if (this.input.props.form != null) {
            this.input.form = this.input.props.form;
            this.input.metadata = this.input.form.getMetadata(this.input.name);
            this.input.internalValue = this.input.props.value || this.input.form.getValue(this.input.name);

            if (this.input.metadata != null)
                this.input.labelText = this.input.props.label || this.input.metadata.displayName;
        }
    }
    handleChange(event)
    {
        this.input.internalValue = event.target.value;
        console.log('input=>' + this.input.internalValue);
        if (this.input.form != null) {
            this.input.form.onChange(event);
        }
    }
    doValidation=()=> {
        if (this.input.form && this.input.metadata) {
            var result = Services.ValidationService.validate({ metadata: this.input.metadata, value: this.input.internalValue });
            this.showValidationIfNeeded(result);
        }
    }
    showValidationIfNeeded=(validation: Validation.ValidationResponse)=>{
        this.input.isValid = validation.isValid;
        this.input.validationMessage = validation.message;
    }
}