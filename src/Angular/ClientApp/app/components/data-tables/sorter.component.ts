import { Component, OnInit, Output, Input, EventEmitter, DoCheck } from '@angular/core';
import { Models } from './../../root';

@Component({
  selector: 'sorter',
  templateUrl: './sorter.component.html',
  styleUrls: ['./sorter.component.css']
})
export class SorterComponent implements DoCheck {
    @Input() public gridState: Models.IGridState;
    @Input() member: string = '';
    @Input() text: string = '';
    @Output() change = new EventEmitter();
    constructor() {
        this.currentSortKey = '';
    }

    ngDoCheck() {
        if (this.gridState == null || this.gridState.totalRecords == undefined ||
            this.gridState.totalRecords == 0) {
            return;
        }
        else {
            this.setup();
        }
    }


  setup = () => {
      this.currentSort = this.gridState.sortDirection + this.gridState.sortMember;
      this.isCurrentSortAsc = this.checkIfCurrentSortAsc(this.member);
      this.isCurrentSortDesc = this.checkIfCurrentSortDesc(this.member);
  }

  checkIfCurrentSortAsc = (member) => {      
      if (this.gridState === null || this.gridState.sortMember == null || this.gridState.sortDirection == null)
          return false;
      return this.gridState.sortMember.toUpperCase() === member.toUpperCase()
          && this.gridState.sortDirection.toUpperCase() === "ASC";
  }

  checkIfCurrentSortDesc = (member) => {      
      if (this.gridState === null || this.gridState.sortMember == null || this.gridState.sortDirection == null)
          return false;
      return this.gridState.sortMember.toUpperCase() === member.toUpperCase()
          && this.gridState.sortDirection.toUpperCase() === "DESC";
  }

  sort = (member) => {
      this.setup();
      if (member.toUpperCase() === this.gridState.sortMember.toUpperCase())
          this.gridState.sortDirection = this.gridState.sortDirection === "ASC" ? "DESC" : "ASC";
      else
          this.gridState.sortDirection = "ASC";
      this.gridState.sortMember = member;
      let e = new CustomEvent('change', {
          bubbles: true
      });
      this.change.emit({});
  }

  currentSort: string;
  currentSortKey: string;
  isCurrentSortAsc: boolean;
  isCurrentSortDesc: boolean;
}
