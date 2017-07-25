import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'error-icon',
  templateUrl: './error-icon.component.html',
  styleUrls: ['./error-icon.component.css']
})
export class ErrorIconComponent implements OnInit {

    @Input() message = '';
  constructor() { }

  ngOnInit() {
  }

}
