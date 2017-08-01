﻿import { InputComponent, InputComponentProps } from './input-component'
import { ErrorIcon } from './error-icon';

export class InputDropdown extends InputComponent {
    private isLabel = false;
    private isDropdown = false;
    private selectedText = '';
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
        return <div className="mdc-textfield input-field-container mdc-form-field" >
                {!this.nolabel && <label className="input-label" > {this.labelText}</label>}
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