import * as React from 'react';
import { InputField } from './input-field';
import { ErrorIcon } from './error-icon';
import { Services } from './../../root';

export class InputText extends InputField {

    preRender = () => {
        if (this.metadata)
            this.internalValue = Services.FormatService.format(this.internalValue, this.metadata);
    }

    doRender = () => {
        return <div className={"mdc-textfield input-field-container mdc-form-field" +
                + this.fullWidth ? " full-width " : ""}>
                {!this.nolabel && <label className="input-label">{this.labelText}</label>}
                {!this.isValid && <ErrorIcon text={this.validationMessage} ></ErrorIcon>}
                <input type="text" autoFocus={this.props.autofocus} value={this.internalValue}
                    onChange={this.form.onChange}
                    className="mdc-textfield__input input-field" />
            </div>;
    }


}