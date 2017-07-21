import { InputComponent } from './input-component';
import { Ng, Models, Services } from './../../root';

@Ng.Component({
    selector: 'input-field',
    templateUrl: './input-field.component.html',
    styleUrls: ['./input-field.component.css']
})

//TODO: consider doing away with ngModel for internal value
//even though model changes are not emitted until blur or enter press
//change events seem to be firing all over the place
export class InputFieldComponent extends InputComponent implements
    Ng.OnChanges {

    @Ng.Input() autofocus: boolean;
    @Ng.Input() multiline: boolean;
    @Ng.Input() fullWidth: boolean;

    constructor(private formatter: Services.FormatService) {
        super();
        this.emitting = false;
    }
    onEnterKey = () => {
        if (this.emitting)
            return;
        console.log('text=>onEnterKey')
        this.emitting = true;
        this.sync();
        this.emitting = false;
    }
    onChange = () => {
        if (this.emitting)
            return;
        console.log('text=>onChange')
        this.emitting = true;
        if (this.internalValue == this.model)
            return;
        this.sync()
        this.emitting = false;
    }
    sync = () =>
    {
        this.internalValue = this.formatter.format(this.internalValue, this.metadata);
        this.model = this.internalValue;
        this.modelChange.emit(this.internalValue);
        this.change.emit(this.internalValue);
    }
    
   
    handleFormat() {
        if (this.metadata)
            this.internalValue = this.formatter.format(this.model, this.metadata);
        else
            this.internalValue = this.model;
    }
}
