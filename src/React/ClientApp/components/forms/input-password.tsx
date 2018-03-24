import * as React from 'react';
import { ErrorIcon } from './error-icon';
import { Services } from './../../root';
import { InputComponent } from './input-component';
import { InputHelper } from './input-helper';
import { InputShell } from './input-shell';

export class InputPassword extends InputComponent {
    withFormat: boolean = true;
    constructor(props) {
        super(props);
        this.helper = new InputHelper(this);
    }
    internalChangeHandler = (event) => {
        this.withFormat = false;
        this.helper.handleChange(event, false);
    }
    internalBlurHandler = (event) => {
        this.withFormat = true;
        this.helper.handleChange(event, true);
    }
    preRender = () => {

    }

    doRender = () => {

        var config = this.helper.getState();
        var value = config.rawValue;
        if (value == null)
            value = '';

        return <InputShell {...this.state}
                   label={config.label}
                   isValid={config.isValid}
                   validationMessage={config.validationMessage} >
                   <input type="password" name={this.state.name}
                          readOnly={config.isReadOnly}
                          autoFocus={this.state.autofocus}
                          value={value}
                          onChange={this.internalChangeHandler}
                          onBlur={this.internalBlurHandler}
                          className=" mdc-textfield__input input-field form-control"
                          key={this.state.key} />
               </InputShell>;
    }
}