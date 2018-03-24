import * as React from 'react';
import { ErrorIcon } from './error-icon';
import { Services } from './../../root';
import { InputComponent } from './input-component';
import { InputHelper } from './input-helper';
import { InputShell } from './input-shell';

export class InputCheck extends InputComponent {
    
    constructor(props) {
        super(props);
        this.helper = new InputHelper(this);
    }
    internalChangeHandler = (event) => {
        this.helper.handleChange(event, true);
    }
    internalBlurHandler = (event) => {
        this.helper.handleChange(event, true);
    }
    preRender = () => {

    }

    doRender = () => {
       
        var config = this.helper.getState();
        var value = config.rawValue;
        return <InputShell {...this.state} label={config.label} isValid={config.isValid} validationMessage={config.validationMessage} >
                   <input type="checkbox" name={this.props.name}
                        autoFocus={this.props.autofocus}
                          checked={value == true}
                          onChange={this.internalChangeHandler}
                          onBlur={this.internalBlurHandler}
                disabled={config.isReadOnly}
                          className="mdc-textfield__input input-field" />
               </InputShell>;
    }
}