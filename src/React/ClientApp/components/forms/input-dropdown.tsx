import * as React from 'react';
import { InputComponent, InputComponentProps } from './input-component'
import { ErrorIcon } from './error-icon';
import { InputHelper } from './input-helper';

export class InputDropdown extends InputComponent {

    private isLabel = false;
    private isDropdown = false;
    private selectedText = '';

    constructor(props) {
        super(props);
        this.helper = new InputHelper(this);
    }
    onChange = (event) => {
        this.helper.handleChange(event);
    }
    doValidation = () => {
        this.helper.doValidation();
    };

    preRender = () => {
        if (this.props.readOnly)
            {
            this.isLabel = true;
            let items = this.props.items.filter((item) => { return item.id == this.internalValue; });
            if (items.length > 0)
                this.selectedText = items[0].name;
        }
        else
            this.isDropdown = true;

    }
    doRender = () => {
        var labelClasses = "input-label";
        if (this.nolabel)
            labelClasses = labelClasses + " no-label";

        var classes = "mdc-textfield input-field-container mdc-form-fieldinput-field-container mdc-form-field";
        if (this.fullWidth)
            classes = classes + " full-width";
        return <div className={classes}>          
            <label className={labelClasses}>{this.labelText}</label>
                    <div className="relative-position">
                        {!this.isValid && <ErrorIcon text={this.validationMessage}></ErrorIcon>}
                        <select className="mdc-select input-component" onChange={this.form.onChange}>
                            <option></option>
                            {
                                this.props.items.map((item) => {
                                        return <option
                                        selected={item.id == this.internalValue}
                                        value={item.id}
                                        className="mdc-list-item">{item.name}</option>;
                                    }
                                )
                            }
                        </select >
                    </div >
             </div >;
    }


}