import * as React from 'react';

export class PushButtonProps
{
    theme?: string;
    click: any;
    text?='';
    icon?='';
    type? = "button";
    compact?: boolean;
}
export class PushButton extends React.Component<PushButtonProps, void> {
    handleTheme = () => {
        var normal = "push-button mdc-button mdc-ripple-upgraded mdc-button--raised";
        var buttonClass = '';
        switch (this.props.theme) {
            case "icon":
            case "primary":
                buttonClass = "push-button mdc-button mdc-button--primary mdc-ripple-upgraded mdc-button--raised";
                break;
            case "info":
                buttonClass = normal + ' info-button';
                break;
            case "danger":
                buttonClass = normal + ' danger-button';
                break;
            case "sucess":
                buttonClass = normal + ' sucess-button';
                break;
            case "warning":
                buttonClass = normal + ' warning-button';
                break;
                       
            default:
                buttonClass = normal + ' default-button';
        }
        if (this.props.compact)
            buttonClass = buttonClass + " compact";

        return buttonClass;

    }
    public render() {
        let buttonClass = this.handleTheme();
        let iconName = '';
        let text = '';
        let title = '';

        if (this.props.icon)
            iconName = "mdi mdi-" + this.props.icon;
        if (this.props.text && this.props.theme != 'icon')
            text = this.props.text;
        else if (this.props.text)
            title = this.props.text;
                
        this.handleTheme();
        return  <button type='button'
                className={buttonClass}
                title={title}
                style={{ minWidth: '36px !important' }}>
            {this.props.icon && <i style={{ fontSize: '18px' }} className={iconName}></i>}
                {text}
            </button >;
    }
}