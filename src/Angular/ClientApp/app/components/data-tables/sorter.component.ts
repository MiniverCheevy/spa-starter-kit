import { Component, OnInit, Output, Input, EventEmitter, OnChanges } from '@angular/core';
import { Models } from './../../root';

@Component({

  selector: 'sorter',
  templateUrl: './sorter.component.html',
  styleUrls: ['./sorter.component.css']
})
export class SorterComponent implements OnChanges {
    @Input() public request: Models.IGridState;
    @Input() member: string = '';
    @Input() text: string = '';
    @Output() change = new EventEmitter();
    currentSortKey:string;
    currentSort: string;
    isCurrentSortAsc: boolean;
    isCurrentSortDesc: boolean;
    constructor() {
        this.currentSortKey = '';
    }

    ngOnChanges() {
        if (!this.request || !this.request.totalPages)
            return;
        
            this.setup();
    }


  setup = () => {
      this.currentSort = this.request.sortDirection + this.request.sortMember;
      this.isCurrentSortAsc = this.checkIfCurrentSortAsc(this.member);
      this.isCurrentSortDesc = this.checkIfCurrentSortDesc(this.member);
  }

  checkIfCurrentSortAsc = (member) => {      
      if (this.request === null || this.request.sortMember == null || this.request.sortDirection == null)
          return false;
      return this.request.sortMember.toUpperCase() === member.toUpperCase()
          && this.request.sortDirection.toUpperCase() === "ASC";
  }

  checkIfCurrentSortDesc = (member) => {      
      if (this.request === null || this.request.sortMember == null || this.request.sortDirection == null)
          return false;
      return this.request.sortMember.toUpperCase() === member.toUpperCase()
          && this.request.sortDirection.toUpperCase() === "DESC";
  }

  sort = (member) => {
      console.log('sort=' + member);
      this.setup();
      if (member.toUpperCase() === this.request.sortMember.toUpperCase())
          this.request.sortDirection = this.request.sortDirection === "ASC" ? "DESC" : "ASC";
      else
          this.request.sortDirection = "ASC";
      this.request.sortMember = member;
      let e = new CustomEvent('change', {
          bubbles: true
      });
      this.change.emit({});
  }


}
