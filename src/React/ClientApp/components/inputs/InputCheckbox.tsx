import * as React from 'react';
import { observer } from 'mobx-react'

export class InputCheckboxPropTypes {
    onChange: (key, value) => void;
    name: string;
    value: boolean;
    id?: string;
    label?: string;
}

@observer
export default class InputCheckbox extends React.Component<InputCheckboxPropTypes, any> {
    constructor(props) {
        super(props)
        this.onChange = this.onChange.bind(this)
    }

    onChange(event) {
        this.props.onChange(this.props.name, event.target.checked)
    }

    render() {
        const { name, value, id, label } = this.props
        return (
            <div className="checkbox">
                <label htmlFor={id || name}>
                    <input type="checkbox" name={name} id={id || name}
                        checked={value} onChange={this.onChange} /> {label || name}
                </label>
            </div>
        )
    }
}


