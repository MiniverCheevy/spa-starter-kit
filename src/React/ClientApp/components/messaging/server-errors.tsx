import * as React from 'react';
import { Models, Components, Services } from './../../root';
import { observer } from './../../mx';

export class ServerErrorsProps {
    errors: Models.INameValuePair[];
}

@observer
export class ServerErrors extends React.Component<ServerErrorsProps, any> {
    constructor(props) {
        super(props);
        this.createIntialState(props);
    }

    createIntialState = (props) => {
        this.state = { errors: props.errors || []};
    }

    componentWillReceiveProps(props) {
        this.copyPropsToState(props);
    }
    copyPropsToState = (props) => {
        this.setState({ errors: props.errors || []});
    }

    render() {
        const values = this.state.errors.map((error) => {
            return <div className="text-danger">{error.name} {error.value}</div>
        });
        return <div className="pull-right panel inline">
            <div className="panel-body">
                {values}
                </div>
        </div>;

    }
}