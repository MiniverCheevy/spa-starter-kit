import { Component, OnInit } from '@angular/core';
import { Ng } from './../../root';

@Component({
  selector: 'plus-minus-button',
  templateUrl: './plus-minus-button.component.html',
  styleUrls: ['./plus-minus-button.component.css']
})
export class PlusMinusButtonComponent implements OnInit {

    @Ng.Input() show = false;
    @Ng.Output() showChange = new Ng.EventEmitter();
  constructor() { }

  ngOnInit() {
  }

  plusClick()
  {   
      this.show = true;
      this.showChange.emit(this.show);
  }
  minusClick()
  {
      this.show = false;
      this.showChange.emit(this.show);
  }

}
