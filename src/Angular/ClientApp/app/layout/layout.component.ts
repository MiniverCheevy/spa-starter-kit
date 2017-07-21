import { Component, OnInit } from '@angular/core';
import { Services } from './../root';
@Component({
  selector: 'layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css']
})
export class LayoutComponent implements OnInit {

  constructor(private messenger: Services.MessengerService) { }

  ngOnInit() {
  }

}
