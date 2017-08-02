import { InputComponentProps, InputComponent } from './input-component';
import * as React from 'react';
import { Models, Services } from './../../root';
import { IObservableValue, isObservable } from './../../mx';
import * as Validation from './../../services/validation';

export class InputState
{
    value;
    formattedValue:string;
    metadata: Models.UIMetadata;
    label: string;
}

export class InputHelper {
    constructor(public input: InputComponent) {

    }
    getState = (): InputState=>
    {
        debugger;
        var state = new InputState();
        state.metadata = this.input.props.form.configureMetadata(this.input);
        var value = this.input.props.value;
        
        if (this.input.props.model != null)
            {
            var val = (this.input.props.model[this.input.props.name] as IObservableValue<any>);
            if (isObservable(val))
                value = val.get();
            if (val != null)
                value = val;
            }
        var formattedValue = value;
        if (state.metadata)
            formattedValue = Services.FormatService.format(value, state.metadata);

        var label = this.input.props.name;
        if (state.metadata)
            label = state.metadata.displayName;
        if (this.input.props.label)
            label = this.input.props.label;

        state.label = label;

        return state;
    }
    handleChange=(event)=>
    {
        var key = event.target.name;
        var value = event.target.value;

        if (this.input.props.change != null) {
            this.input.props.change(key,value);
        }
    }
    doValidation=()=> {
        if (this.input.props.form) {
           // var metadata = this.input.props.form.metadata[this.input.props.name];
            //var result = Services.ValidationService.validate({ metadata: this.input.props.metadata, value: this.input.internalValue });
           // this.showValidationIfNeeded(result);
        }
    }
    showValidationIfNeeded=(validation: Validation.ValidationResponse)=>{
        //this.input.props.isValid = validation.isValid;
        //this.input.props.validationMessage = validation.message;
    }
}