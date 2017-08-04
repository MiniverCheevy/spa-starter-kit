import { InputComponentProps, InputComponent } from './input-component';
import * as React from 'react';
import { Models, Services } from './../../root';
import * as Validation from './../../services/validation';

export class InputState {
    formattedValue: string;
    metadata: Models.UIMetadata;
    label: string;
    isValid: boolean = true;
    validationMessage: string;
}

export class InputHelper {
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
            console.log('inputhelper=>' + this.input.props.name + '=' + val);

            if (val != null)
                value = val;
        }
        var formattedValue = value;
        if (state.metadata) {

            var result = Services.ValidationService.validate({ metadata: state.metadata, value: value });
            state.isValid = result.isValid;
            state.validationMessage = result.message;
            if (state.isValid)
                formattedValue = Services.FormatService.format(value, state.metadata);
        }
        var label = this.input.props.name;
        if (state.metadata)
            label = state.metadata.displayName;
        if (this.input.props.label)
            label = this.input.props.label;

        state.label = label;
        state.formattedValue = formattedValue;

        return state;
    }
    handleChange = (event, withFormat?: boolean) => {
        var key = event.target.name;
        var value = event.target.value;
        if (withFormat) {
            var state = this.getState();
            if (state.metadata)
                value = Services.FormatService.format(value, state.metadata);
        }
        var form = this.input.props.form;
        form.isDirty = true;
        if (this.input.props.change != null) {
            this.input.props.change(key, value, form);
        }
    }
}