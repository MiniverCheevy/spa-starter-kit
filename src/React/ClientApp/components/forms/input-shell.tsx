import * as React from 'react';
import { ErrorIcon } from './error-icon';
import { Services } from './../../root';
import { InputComponentModel as InputComponentProps } from './input-component';
import { InputHelper } from './input-helper';

export class InputShell extends React.Component<InputComponentProps, any> {
    constructor(props) {
        super(props);
        this.createInitialState(props);
    }

    createInitialState = (props) => {
        this.state = { ...props };
    }

    componentWillReceiveProps(props) {
        this.copyPropsToState(props);
    }

    copyPropsToState = (props) => {
        this.setState({ ...props });
    }

    render() {
        return this.doRender();
    }
    doRender = () => {
        var classes = "mdc-textfield input-field-container mdc-form-field mdc-form-field";
        if (this.state.fullWidth)
            classes = classes + " full-width";
        var labelClasses = " input-label";
        if (this.state.noLabel != null && this.state.noLabel)
            labelClasses = labelClasses + " no-label";
        var showError = this.state.isValid != null && !this.state.isValid;

        return <div className={classes}>
            {showError && <ErrorIcon text={this.state.validationMessage} ></ErrorIcon>}
            {!this.props.noLabel && <label className={labelClasses}>{this.state.label}</label>}
            {this.state.prefix && <span className="input-prefix">&nbsp;{this.state.prefix}</span>}
            {this.state.children}
            {this.state.suffix && <span className="input-suffix">&nbsp;{this.state.suffix}</span>}            
        </div>;
    }


}