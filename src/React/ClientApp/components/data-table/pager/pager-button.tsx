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
        let isActive = this.props.isActive;

        let classes = "mdc-button mdc-button--raised mdc-ripple-upgraded  pager-button ";
        let activeClasses = "mdc-button--primary mdc-button mdc-button--raised mdc-ripple-upgraded pager-button ";
        if (isActive && isDisabled)
        {
            let classes = activeClasses + " not-allowed";
            return <button type="button"
                           className={classes}
                           >{text}</button>;
        }
        else if (!isDisabled) {
            return <button type="button"
                className={classes} 
                onClick={method}>{text}</button>;
        }
        else {
            classes = classes + " not-allowed";
            return <button type="button"
                className={classes} disabled={isDisabled}
            >{text}</button>;
        }
    };
}