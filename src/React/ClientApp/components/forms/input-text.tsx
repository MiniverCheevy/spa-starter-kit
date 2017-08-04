import * as React from 'react';
import { ErrorIcon } from './error-icon';
import { Services } from './../../root';
import { InputComponent } from './input-component';
import { InputHelper } from './input-helper';
import { InputShell } from './input-shell';

export class InputText extends InputComponent {
    constructor(props) {
        super(props);
        this.helper = new InputHelper(this);
    }
    internalChangeHandler = (event) => {
        this.helper.handleChange(event, false);
    }
    internalBlurHandler = (event) => {
        this.helper.handleChange(event, true);
    }
    preRender = () => {

    }

    doRender = () => {
        var state = this.helper.getState();

        return <InputShell {...this.props} label={state.label}>
            <input type="text" name={this.props.name}
                autoFocus={this.props.autofocus}
                value={state.formattedValue}
                onChange={this.internalChangeHandler}
                onBlur={this.internalBlurHandler}
                className="mdc-textfield__input input-field" />
        </InputShell>;
    }
}