import * as React from 'react';
import { ErrorIcon } from './error-icon';
import { Services } from './../../root';
import { InputComponent } from './input-component';
import { InputHelper } from './input-helper';

export class InputText extends InputComponent {
    constructor(props) {
        super(props);
        this.helper = new InputHelper(this);
    }
    onChange = (event) => {
        this.helper.handleChange(event);
    }
    doValidation = () => {
        this.helper.doValidation();
    };
    preRender = () => {
        this.helper.parseProps();
        if (this.metadata)
            this.internalValue = Services.FormatService.format(this.internalValue, this.metadata);
    }    
    doRender = () => {
        var classes = "mdc-textfield input-field-container mdc-form-fieldinput-field-container mdc-form-field";
        if (this.fullWidth)
            classes = classes + " full-width";
        var labelClasses = "input-label";
        if (this.nolabel)
            labelClasses = labelClasses + " no-label";
        var onChange = this.onChange;


        return <div className={classes}>
            <label className={labelClasses}>{this.labelText}</label>
            {!this.isValid && <ErrorIcon text={this.validationMessage} ></ErrorIcon>}
            <input type="text" name={this.name}
                autoFocus={this.props.autofocus}
                value={this.internalValue}
                onChange={this.onChange}
                className="mdc-textfield__input input-field" />
        </div>;
    }


}