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
        let text = this.props.text;
        let isDisabled = this.props.isDisabled;
        let method = this.props.method;
        let isActive = this.props.isDisabled;

        let classes = "mdc-button mdc-button--raised mdc-button--compact mdc-ripple-upgraded mdc-ripple-upgraded--foreground-deactivation pager-button ";
        if (isActive)
            classes = "mdc-button mdc-button--raised mdc-button--primary mdc-ripple-upgraded mdc-ripple-upgraded--foreground-deactivation pager-button ";
        if (!isDisabled) {
            return <button type="button"
                className={classes} 
                onClick={method}>{text}</button>;
        }
        else {
            return <button type="button" disabled={true}
                className={classes} 
            >{text}</button>;
        }
    };
}