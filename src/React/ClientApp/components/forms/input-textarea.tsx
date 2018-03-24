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
        var config = this.helper.getState();
        var value = config.formattedValue;
        if (value == null)
            value = '';

        var textAreaClasses = "mdc-textfield__input input-field";        

        if (this.props.fullWidth) {
            textAreaClasses = textAreaClasses + " full-width";
        }
        if (this.props.fullHeight) {
            textAreaClasses = textAreaClasses + " full-height";
        }

        return <InputShell {...this.state} label={config.label}>            
            <textarea
                autoFocus={this.props.autofocus} value={value}
                onChange={this.onChange} rows={this.props.lines}
                className={textAreaClasses} >
            </textarea>
        </InputShell>;


    }


}



