﻿import * as React from 'react';
import { ErrorIcon } from './error-icon';
import { Services } from './../../root';
import { InputComponent } from './input-component';
import { InputHelper } from './input-helper';
import { InputShell } from './input-shell';

export class InputSpan extends InputComponent {

    constructor(props) {
        super(props);
        this.helper = new InputHelper(this);
    }

    doValidation = () => {

    };

    preRender = () => {

    }

    doRender = () => {
        var config = this.helper.getState();
        var value = config.rawValue;

        return <InputShell {...this.state} label={config.label}>
            <span className="input-field">{config.formattedValue}</span>
        </InputShell>;

    }


}