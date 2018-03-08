import * as React from 'react';
import { ErrorIcon } from './error-icon';
import { Services } from './../../root';
import { InputComponent } from './input-component';
import { InputHelper } from './input-helper';
import { InputShell } from './input-shell';

export class RadioButtonList extends InputComponent {

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
            const items = this.props.items.filter((item) => { return item.id == this.props.value; });
            if (items.length > 0)
                this.selectedText = items[0].name;
        }
        else
            this.isDropdown = true;

    }

    doRender = () => {
        var config = this.helper.getState();
        var value = config.rawValue;

        var options = this.props.items.map((item) => {
            return <label className="radio-inline" key={item.name + '_' + item.id + "_label"}>
                <input type="radio"
                    name={this.state.name}
                    checked={item.id == value}
                    value={item.id}
                    key={item.name + '_' + item.id}
                    onChange={this.onChange}
                />
                {item.name}
            </label>
                ;
        }
        );
        /*return <InputShell {...this.props} label={state.label}>*/
        return <div className="row">
            {options}
        </div>;
        //</InputShell>;
    }
}