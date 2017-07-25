import { Component, OnInit } from '@angular/core';
import { Ng, Models, Services } from './../../root';

@Component({
  selector: 'confirm-dialog',
  templateUrl: './confirm-dialog.component.html',
  styleUrls: ['./confirm-dialog.component.css']
})
export class ConfirmDialogComponent implements OnInit {

    @Ng.Input() text = 'Are you sure?';
    @Ng.Output() answer = new Ng.EventEmitter<boolean>();

    constructor(private messenger: Services.MessengerService) {

    }

  ngOnInit() {
  }
  ok()
  {
      this.answer.emit(true);
  }
  cancel() {
      this.answer.emit(false);
  }

}
