import { InputComponent } from './input-component';
import { Ng, Models, Services } from './../../root';

@Ng.Component({
    selector: 'input-span',
    templateUrl: './input-span.component.html',
    styleUrls: ['./input-span.component.css']
})

export class InputSpanComponent extends InputComponent {
    
    constructor(private formatter: Services.FormatService) {
        super();

    }
        
    handleFormat = () => {
        if (this.metadata)
            this.internalValue = this.formatter.format(this.model, this.metadata);
        else
            this.internalValue = this.model;
    }
}
