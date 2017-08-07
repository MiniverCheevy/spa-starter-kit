import { InputComponentProps, InputComponent } from './input-component';
import * as React from 'react';
import { Models, Services } from './../../root';
import * as Validation from './../../services/validation';

export class InputState {
    rawValue: string;
    formattedValue: string;
    metadata: Models.UIMetadata;
    label: string;
    isValid: boolean = true;
    validationMessage: string;
}

export class InputHelper {
    private previousValue='';
    constructor(public input: InputComponent) {

    }
    getState = (): InputState => {
        var state = new InputState();
        if (this.input.props == null)
            return state;
        if (this.input.props.form != null)
            state.metadata = this.input.props.form.configureMetadata(this.input);

        var value = this.input.props.value;

        if (this.input.props.model != null) {
            var val = this.input.props.model[this.input.props.name];
            if (val != null)
                value = val;
        }
        state.rawValue = value;
        var formattedValue = state.rawValue;
        if (state.metadata) {
            var name = state.metadata.displayName;
            state.formattedValue = Services.FormatService.format(value, state.metadata);

            var result = Services.ValidationService.validate({ metadata: state.metadata, value: state.formattedValue as any });
            state.isValid = result.isValid;
            state.validationMessage = result.message;


        }
        var label = this.input.props.name;
        if (state.metadata)
            label = state.metadata.displayName;
        if (this.input.props.label)
            label = this.input.props.label;

        state.label = label;

        return state;
    }
    handleChange = (event, withFormat?: boolean) => {
        var state = this.getState();
        var key = event.target.name;
        var value = event.target.value;
        if (withFormat) {            
            if (state.metadata)
                value = Services.FormatService.format(value, state.metadata);
        }
        var form = this.input.props.form;
        if (form) {
            if (value != this.previousValue)
                form.isDirty = true;    

            this.previousValue = value;

            if (this.input.props.change != null) {
                var formattedValue = Services.FormatService.format(value, state.metadata);

                var result = Services.ValidationService.validate({ metadata: state.metadata, value: formattedValue as any});
                form.metadata[this.input.props.name].isValid = result.isValid;
                form.metadata[this.input.props.name].validationMessage = result.message;
               

            }
        }
        this.input.props.change(key, value, form);
    }
}