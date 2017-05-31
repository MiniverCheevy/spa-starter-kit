import * as React from 'react';
import { observer } from 'mobx-react'

export class InputTextPropTypes {
    onChange: (key, value) => void;
    name: string;
    value: string | string[] | number;
    id?: string;
    type?: string = 'text';
    label?: string;
}


@observer
export default class InputText extends React.Component<InputTextPropTypes, any> {
    constructor(props) {
        super(props);
        this.onChange = this.onChange.bind(this);
    }

    onChange(event) {
        this.props.onChange(event.target.name, event.target.value);
    }

    render() {

        const input = this.props;
        return (
            <div className="form-group fluffy inline">

                    <label className="form-label" htmlFor={input.id}>{input.label || input.name}</label>&nbsp;&nbsp;
                    <input
                        className="form-control"
                        id={input.id}
                        name={input.name}
                        onChange={this.onChange}
                        type={input.type}
                        value={input.value} />

            </div>
        );
    }
}


