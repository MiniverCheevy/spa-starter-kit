import * as React from 'react';

export class PagerButtonProps {
    text: string;
    isDisabled: boolean;
    method: any;// (request: Models.IGridState)=> void;
    isActive?: boolean;
}
export class PagerButton extends React.Component<PagerButtonProps, any>
{
    constructor(props: PagerButtonProps) {
        super(props);
    }

    render() {
        var text = this.props.text;
        var isDisabled = this.props.isDisabled;
        var method = this.props.method;
        var isActive = this.props.isDisabled;

        var classes = "mdc-button mdc-button--raised mdc-button--compact mdc-ripple-upgraded mdc-ripple-upgraded--foreground-deactivation";
        if (isActive)
            classes = "mdc-button mdc-button--raised mdc-button--primary mdc-ripple-upgraded mdc-ripple-upgraded--foreground-deactivation";
        if (!isDisabled) {
            return <button type="button"
                className={classes}
                onClick={method}> {text}</button>
        }
        else {
            return <button type="button" disabled={true}
                className={classes}
            >{text}</button>
        }
    };
}