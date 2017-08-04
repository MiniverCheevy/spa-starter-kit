import * as React from 'react';
import { ErrorIcon } from './error-icon';
import { Services } from './../../root';
import { InputComponent } from './input-component';
import { InputHelper } from './input-helper';
import { InputShell } from './input-shell';

export class InputTextArea extends InputComponent {
    constructor(props) {
        super(props);
        this.helper = new InputHelper(this);
    }
    onChange = (event) => {
        this.helper.handleChange(event);
    }
    
    doRender = () => {
        var state = this.helper.getState();

        var textAreaClasses = "mdc-textfield__input input-field";        

        if (this.props.fullWidth) {
            textAreaClasses = textAreaClasses + " full-width";
        }

        return <InputShell {...this.props} label={state.label}>            
            <textarea
                autoFocus={this.props.autofocus} value={state.formattedValue}
                onChange={this.onChange} rows={this.props.lines}
                className={textAreaClasses} >
            </textarea>
        </InputShell>;


    }


}



