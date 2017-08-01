import * as React from 'react';
import { InputComponent, InputComponentProps } from './input-component';
import { Services } from './../../root';
import { InputHelper } from './input-helper';

export class InputSpan extends InputComponent {

    constructor(props)
    {        
        super(props);
        this.helper = new InputHelper(this);
    }
    
    doValidation = () => {
       
    };

    preRender = () => {
        this.helper.parseProps();
        if (this.metadata)
            this.internalValue = Services.FormatService.format(this.internalValue, this.metadata);
        else
            this.internalValue = this.props.value;
    }

    doRender = () => {

        var classes = "mdc-textfield input-field-container mdc-form-fieldinput-field-container mdc-form-field";
        if (this.fullWidth)
            classes = classes + " full-width";

        var labelClasses = "input-label";
        if (this.nolabel)
            labelClasses = labelClasses + " no-label";

        return <div className={classes}>
                   <label className={labelClasses}>{this.labelText}</label>
            <span className="input-field">{this.internalValue}</span>
        </div >;
    }


}