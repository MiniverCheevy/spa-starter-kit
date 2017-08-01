import * as React from 'react';
import { InputComponent, InputComponentProps } from './input-component';
import { Services } from './../../root';

export class InputSpan extends InputComponent {

    constructor(props)
    { super(props);}

    preRender = () => {
        if (this.metadata)
            this.internalValue = Services.FormatService.format(this.internalValue, this.metadata);
        else
            this.internalValue = this.props.value;
    }

    doRender = () => {
        debugger;
        return <label className={"mdc-textfield input-field-container mdc-form-fieldinput-field-container mdc-form-field"
            + this.fullWidth ? " full-width " : ""}>
            {!this.nolabel && <span className="input-label">{this.labelText}</span>}
            <span className="input-field">{this.internalValue}</span>
        </label >;
    }


}