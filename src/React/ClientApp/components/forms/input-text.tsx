import * as React from 'react';
import { ErrorIcon } from './error-icon';
import { Services } from './../../root';
import { InputComponent } from './input-component';
import { InputHelper } from './input-helper';
import { InputShell } from './input-shell';

export class InputText extends InputComponent {
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

    doRender = (props) => {
        this.props = props;
        var state = this.helper.getState();
        var value = this.withFormat ? state.formattedValue : state.rawValue;
        return <InputShell {...this.props} label={state.label} isValid={state.isValid} validationMessage={state.validationMessage} >
            <input type="text" name={this.props.name}
                autoFocus={this.props.autofocus}
                value={value}
                onChange={this.internalChangeHandler}
                onBlur={this.internalBlurHandler}
                className="mdc-textfield__input input-field" />
        </InputShell>;
    }
}