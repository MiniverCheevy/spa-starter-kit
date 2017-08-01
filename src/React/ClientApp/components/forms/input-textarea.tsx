import * as React from 'react';
import { InputComponent, InputComponentProps } from './input-component'
import { ErrorIcon } from './error-icon';

export class InputTextArea extends InputComponent {
    preRender = () => {

    }
    doRender = () => {
        return <div className={"mdc-textfield input-field-container mdc-form-field "
            + this.fullWidth ? " full-width " : ""} autoFocus={this.props.autofocus} >
            {!this.nolabel && <label className="input-label">{this.labelText}</label>}
            {!this.isValid && <ErrorIcon text={this.validationMessage} ></ErrorIcon>}
            <textarea type="text" autoFocus={this.props.autofocus} value={this.internalValue}
                onChange={this.form.onChange} rows={this.props.lines}
                className="mdc-textfield__input input-field" ></textarea>
        </div>;

  
    }

   
}



