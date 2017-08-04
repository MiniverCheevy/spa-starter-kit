import * as React from 'react';
import { ErrorIcon } from './error-icon';
import { Services } from './../../root';
import { InputComponentProps } from './input-component';
import { InputHelper } from './input-helper';

export class InputShell extends React.Component<InputComponentProps, any> {


    render() {
        var classes = "mdc-textfield input-field-container mdc-form-fieldinput-field-container mdc-form-field";
        if (this.props.fullWidth)
            classes = classes + " full-width";
        var labelClasses = "input-label";
        if (this.props.noLabel != null && this.props.noLabel)
            labelClasses = labelClasses + " no-label";
        var showError = this.props.isValid != null && !this.props.isValid;
        
        return <div className={classes}>
            {showError && <ErrorIcon text={this.props.validationMessage} ></ErrorIcon>}
            <label className={labelClasses}>{this.props.label}</label>            
            {this.props.children}
        </div>;
    }


}