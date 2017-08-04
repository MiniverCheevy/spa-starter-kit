import * as React from 'react';
import { ErrorIcon } from './error-icon';
import { Services } from './../../root';
import { InputComponent } from './input-component';
import { InputHelper } from './input-helper';
import { InputShell } from './input-shell';

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

    preRender = () => {
        //TODO: something
        if (this.props.readOnly) {
            this.isLabel = true;
            let items = this.props.items.filter((item) => { return item.id == this.props.value; });
            if (items.length > 0)
                this.selectedText = items[0].name;
        }
        else
            this.isDropdown = true;

    }

    doRender = () => {

        var state = this.helper.getState();

        var options = this.props.items.map((item) => {
            return <option
                selected={item.id == this.props.value}
                value={item.id}
                className="mdc-list-item">{item.name}</option>;
        }
        );
        return <InputShell {...this.props} label={state.label}>
            <select className="mdc-select input-component" onChange={this.onChange}>
                <option></option>
                {options}
            </select >
        </InputShell>;
    }
}