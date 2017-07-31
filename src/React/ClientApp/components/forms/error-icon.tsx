import * as React from 'react';
export class ErrorIconProps
{
    text = '';
}
export class ErrorIcon extends React.Component<ErrorIconProps, any>
{
    render() {
        return this.doRender();
    }
    doRender = () => {
        return <span className="error-icon" title={this.props.text}>
            <i className="error mdi mdi-alert-circle"></i>
        </span >;
    }
}