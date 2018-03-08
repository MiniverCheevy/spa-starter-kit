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
        if (this.state.readOnly) {
            this.isLabel = true;
            const items = this.props.items.filter((item) => { return item.id == this.state.value; });
            if (items.length > 0)
                this.selectedText = items[0].name;
        }
        else
            this.isDropdown = true;

    }

    doRender = () => {
        var config = this.helper.getState();
        var value = config.rawValue;
        var items = this.props.items || [];
        
        var options = items.map((item, index) => {
            var key = this.state.name + '_o_' + index;
            return <option key={key}               
                value={item.id}
                className="mdc-list-item">{item.name}</option>;
        }
        );
        return <InputShell {...this.state}
                   label={config.label}
                   isValid={config.isValid}
                   validationMessage={config.validationMessage} >       
            <select className="mdc-select input-component form-control"
                onChange={this.onChange}
                value={config.rawValue}
                key={this.state.key}
                    name={this.state.name}
            >
                <option></option>
                {options}
            </select >
        </InputShell>;
    }
}