
//add GridButtonConfig showIf(any), enableIf(any)
//add ButtonBarButtonConfig showIf(), enableIf()
export class ButtonConfig {
    text: string;
    icon?: string = undefined;
    action: (any) => void;
    showIf?: (any) => boolean;
    enableIf?: () => boolean;
    theme?: 'primary' | 'icon' | 'grid-icon' | 'info' | 'danger' | 'success' | 'warning' = 'primary';
}

export class ButtonSpec{
    text: string;
    icon?: string;
    action: (any) => void;
    showIf?: (any) => boolean;
    enableIf?: () => boolean;
    theme?: 'primary' | 'icon' | 'grid-icon' | 'info' | 'danger' | 'success' | 'warning' = 'primary';

    constructor(props: ButtonConfig)
    {
        this.text = props.text;        
        this.icon = props.icon;
        this.action = props.action;
        this.showIf = props.showIf;
        this.enableIf = props.enableIf;
        this.theme = props.theme;

    }

    get key() {
        let key = '_';
        if (this.text != null)
            key = key + this.text + '_';
        if (this.icon != null)
            key = key + this.icon + '_';
        return key;
    }
}
