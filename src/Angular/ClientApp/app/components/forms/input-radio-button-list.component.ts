import { InputComponent } from './input-component';
import { Ng, Models, Services } from './../../root';

@Ng.Component({
  selector: 'input-radio-button-list',
  templateUrl: './input-radio-button-list.component.html',
  styleUrls: ['./input-radio-button-list.component.css']
})

export class InputRadioButtonListComponent extends InputComponent {
    @Ng.Input() items: Models.IListItem[] = [];
    
    constructor(private formatter: Services.FormatService) {
        super();
    }
    onChange(selectedId) {
        this.internalValue = selectedId;
        this.model = this.internalValue;
        this.modelChange.emit(this.internalValue);
    }

    handleFormat() {
        
    }
}
