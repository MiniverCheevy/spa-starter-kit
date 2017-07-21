import { InputComponent } from './input-component';
import { Ng, Models, Services } from './../../root';

@Ng.Component({
  selector: 'input-dropdown',
  templateUrl: './input-dropdown.component.html',
  styleUrls: ['./input-dropdown.component.css']
})
export class InputDropdownComponent extends InputComponent  {
    @Ng.Output() modelChange = new Ng.EventEmitter();
    @Ng.Input() items: Models.IListItem[] = [];
    @Ng.Input() selectedText: string;
    

    constructor(private formatter: Services.FormatService) {
        super();
    }

 
    onChange() {
        this.handleFormat();
        this.modelChange.emit(this.internalValue);
    }
    
    handleFormat() {
        if (!this.items)
            return;

        var selectedItem = this.items.find((item) => { return item.id == this.internalValue });
        if (selectedItem != null)
            this.selectedText = selectedItem.name;



    }
}
