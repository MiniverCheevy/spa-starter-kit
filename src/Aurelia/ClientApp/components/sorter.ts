import { bindable, inject, customElement } from 'aurelia-framework';
import * as  Models from './../models.generated';

@customElement('sorter')
@inject(Element)
export class Sorter {
    @bindable member: string = '';
    @bindable text: string = '';
    @bindable gridState: Models.IGridState = { sortMember: '', sortDirection: ''};

   constructor(element) {
        this.element = element;
        this.currentSortKey = '';
    }

    public gridStateChanged(newValue, oldValue) {
        if (newValue != null) {
            this.setup();
            
        }
    }

    setup = () => {
        this.currentSort = this.gridState.sortDirection + this.gridState.sortMember;
        this.isCurrentSortAsc = this.checkIfCurrentSortAsc(this.member);
        this.isCurrentSortDesc = this.checkIfCurrentSortDesc(this.member);
    }

    checkIfCurrentSortAsc = (member) => {
        //Don't triple the == unless you can figure out how to check for undefined
        if (this.gridState === null || this.gridState.sortMember == null  || this.gridState.sortDirection == null)
            return false;
        return this.gridState.sortMember.toUpperCase() === member.toUpperCase()
            && this.gridState.sortDirection.toUpperCase() === "ASC";
    }

    checkIfCurrentSortDesc = (member) => {
        //Don't triple the == unless you can figure out how to check for undefined
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
        this.element.dispatchEvent(e);
    }

    currentSort: string;
    element;
    currentSortKey: string;
    isCurrentSortAsc: boolean;
    isCurrentSortDesc: boolean;
}
