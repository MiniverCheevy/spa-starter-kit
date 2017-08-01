import * as React from 'react';
import { InputComponent, InputComponentProps } from './input-component'
import { ErrorIcon } from './error-icon';
import { InputHelper } from './input-helper';

export class InputTextArea extends InputComponent {
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
    }
    doRender = () => {

        var classes = "mdc-textfield input-field-container mdc-form-fieldinput-field-container mdc-form-field";
        var textAreaClasses = "mdc-textfield__input input-field";
        var labelClasses = "input-label";

        if (this.fullWidth) {
            classes = classes + " full-width mdc-textfield mdc-form-fieldinput-field-container mdc-form-field";
            textAreaClasses = textAreaClasses + " full-width";
        }
        var labelClasses = "input-label";
        if (this.nolabel)
            labelClasses = labelClasses + " no-label";

        return <div className={classes}>
            <label className={labelClasses}>{this.labelText}</label>
            {!this.isValid && <ErrorIcon text={this.validationMessage} ></ErrorIcon>}
            <textarea
                autoFocus={this.props.autofocus} value={this.internalValue}
                onChange={this.onChange} rows={this.lines}
                className={textAreaClasses} >
            </textarea>
        </div>;


    }


}



