import { Ng } from './../../root';

@Ng.Component({
  selector: 'push-button',
  templateUrl: './push-button.component.html',
  styleUrls: ['./push-button.component.css']
})
export class PushButtonComponent implements Ng.OnChanges  {

    @Ng.Input() theme;
    @Ng.Output() click = new Ng.EventEmitter();
    @Ng.Input() text;
    @Ng.Input() icon;
    @Ng.Input() type = "button";
    @Ng.Input() compact: boolean;
    iconName: string;
    buttonClass: string;
    content: string;
    title: string='';
    constructor() {
    
    }

    ngOnChanges() {
        
        
        this.handleContent();
        this.handleTheme();
    }

    handleContent()
    {
        if (this.icon)
            this.iconName = "mdi mdi-" + this.icon;
        if (!this.text)
            this.text = '';

        if (this.icon)
            this.content = '<i class="' + this.iconName + '"></i>'
        else
            this.content = '';

        this.content = this.content + this.text;        
    }
    
    handleTheme()
    {
        //mdc-button
        //mdc-icon-toggle material-icons mdc-ripple-upgraded mdc-ripple-upgraded--unbounded
        var normal = "push-button mdc-button mdc-ripple-upgraded mdc-button--raised";
        
        switch (this.theme)
        {
            
            case "primary":
                this.buttonClass = "push-button mdc-button mdc-button--primary mdc-ripple-upgraded mdc-button--raised";
                break;
            case "info":
                this.buttonClass = normal + ' info-button';
                break;
            case "danger":
                this.buttonClass = normal + ' danger-button';
                break;
            case "sucess":
                this.buttonClass = normal + ' sucess-button';
                break;
            case "warning":
                this.buttonClass = normal + ' warning-button';
                break;
            case "icon":
                this.title = this.text;
                this.buttonClass = normal;
                break;
            default:
                this.buttonClass = normal + ' default-button';
        }
        if (this.compact)
            this.buttonClass = this.buttonClass + " "
    }


    


}
