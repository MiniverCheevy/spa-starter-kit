import { Component, AfterContentInit, Input } from '@angular/core';
import { InputComponent } from './input-component';
@Component({
    selector: 'input-field',
    templateUrl: './input-field.component.html',
    styleUrls: ['./input-field.component.css']
})
export class InputFieldComponent extends InputComponent implements AfterContentInit {
    
    @Input() type: string;

    constructor() {
        super();
    }

    ngAfterContentInit() {
        if (!this.type)
            this.type = "text";
    }

}
