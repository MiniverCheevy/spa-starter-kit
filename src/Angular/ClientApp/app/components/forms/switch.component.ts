import { Component, AfterContentInit } from '@angular/core';
import { InputComponent } from './input-component';

@Component({
  selector: 'switch',
  templateUrl: './switch.component.html',
  styleUrls: ['./switch.component.css']
})
export class SwitchComponent extends InputComponent implements AfterContentInit {
    
    constructor() {
        super();
    }

    ngAfterContentInit() {

    }
    handleFormat() { }
}
