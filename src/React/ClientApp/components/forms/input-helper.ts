import { InputComponentModel as InputComponentProps, InputComponent } from './input-component';
import * as React from 'react';
import { Models, Services } from './../../root';
import * as Validation from './../../services/validation';

export class InputState {
    rawValue: any ='';
    formattedValue: string;
    metadata: Models.UIMetadata;
    label: string;
    isValid: boolean = true;
    validationMessage: string;
    isReadOnly: boolean;
}

export class InputHelper {
    private previousValue='';
    constructor(public input: InputComponent) {

    }
    getState = (): InputState => {
        
        var internalState = new InputState();
        if (this.input.state == null)
            return internalState;
        if (this.input.state.form != null)
            internalState.metadata = this.input.state.form.configureMetadata(this.input);


        internalState.rawValue = this.input.state.value;

        if (this.input.state.model != null) {
            const val = this.input.state.model[this.input.state.name];
            if (val != null)
                internalState.rawValue = val;
        }
        
        var formattedValue = internalState.rawValue;
        if (internalState.metadata == null) {
            console.log('no metatdata found for input named ' + this.input.state.name);
        }
        if (internalState.metadata) {
            const name = internalState.metadata.displayName;
            internalState.formattedValue = Services.FormatService.format(internalState.rawValue, internalState.metadata);

            const result = Services.ValidationService.validate({ metadata: internalState.metadata, value: internalState.rawValue});
            internalState.isValid = result.isValid;
            internalState.validationMessage = result.message;
            internalState.isReadOnly = internalState.metadata.isReadOnly;
            internalState.formattedValue = Services.FormatService.format(internalState.rawValue, internalState.metadata);
        }
        else {
            internalState.formattedValue = internalState.rawValue;
        }
        if (this.input.state.readOnly != null)
            internalState.isReadOnly = this.input.state.readOnly;

        var label = this.input.state.name;
        if (internalState.metadata)
            label = internalState.metadata.displayName;
        if (this.input.state.label)
            label = this.input.state.label;

        internalState.label = label;

        return internalState;
    }

    handleChange = (event, withFormat?: boolean) => {
        var state = this.getState();
        var key = event.target.name;
        var value = event.target.value;        
        if (state.metadata && state.metadata.bool) {
            value = event.target.checked;
        } else if (withFormat) {            
            if (state.metadata)
                value = Services.FormatService.format(value, state.metadata);
        }
        var form = this.input.state.form;
        if (form) {
            if (value != this.previousValue)
                form.isDirty = true;    

            this.previousValue = value;

            if (this.input.state.change != null) {
                const formattedValue = Services.FormatService.format(value, state.metadata);

                const result = Services.ValidationService.validate({ metadata: state.metadata, value: formattedValue as any});
                form.metadata[this.input.state.name].isValid = result.isValid;
                form.metadata[this.input.state.name].validationMessage = result.message;
               

            }
        }
        if (this.input.state.change)
            this.input.state.change(key, value, form);
    }
}