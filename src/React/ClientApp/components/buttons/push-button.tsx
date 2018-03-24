import * as React from 'react';

export class PushButtonProps
{
    theme?: 'primary' | 'icon' | 'grid-icon' | 'info' | 'danger' | 'success' | 'warning';
    click: any;
    text?='';
    icon?='';
    type? = 'button';
    compact?: boolean;
    additionalCss?: string;
    enableIf?: () => boolean;
}
export class PushButton extends React.Component<PushButtonProps, any> {
    handleTheme = () => {
        var normal = "push-button mdc-button mdc-ripple-upgraded mdc-button--raised";
        var buttonClass = '';
        switch (this.props.theme) {
            
            case "primary":
                buttonClass = "push-button mdc-button mdc-button--primary mdc-ripple-upgraded mdc-button--raised";
                break;
            case "icon":
            case "grid-icon":
                buttonClass = "grid-icon mdc-ripple-upgraded ";
                break;
            case "info":
                buttonClass = normal + ' info-button';
                break;
            case "danger":
                buttonClass = normal + ' danger-button';
                break;
            case "success":
                buttonClass = normal + ' success-button';
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
        if (this.props.text && this.props.theme != 'icon' && this.props.theme !='grid-icon')
            text = this.props.text;
        else if (this.props.text)
            title = this.props.text;
                
        this.handleTheme();
        if (text != '')
            text = ' ' + text + ' ';
        if (this.props.additionalCss)
            buttonClass = buttonClass + ' ' + this.props.additionalCss;
        let enabled = true;
        var click = this.props.click;

        if (this.props.enableIf)
            enabled = this.props.enableIf();
        if (!enabled) {
            buttonClass = buttonClass + " disabled";
            click = () => {};
        }
            
        return  <a 
                className={buttonClass}
                title={title}
                style={{ minWidth: '36px', minHeight:'36px', height:'auto', cursor: 'pointer'  }}
                onClick={() => { this.props.click() }}
                >
            {this.props.icon && <i  className={iconName}></i>}
                {text}
            </a >;
    }
}