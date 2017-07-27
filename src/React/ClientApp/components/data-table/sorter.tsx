import * as React from 'react';
import { observable, Models } from './../../root';

export class SorterProps {
    member: string = '';
    text: string = '';
    request: Models.IGridState = { sortMember: '', sortDirection: '' };
    refresh: (request: Models.IGridState) => void;
}
export class Sorter extends React.Component<SorterProps, any>
{
    currentSortKey: string;
    member: string = '';
    text: string = '';
    gridState: Models.IGridState = { sortMember: '', sortDirection: '' };

    constructor(props: SorterProps) {        
        super(props);
        this.gridState = props.request;
        this.text = props.text;
        this.member = props.member;
    }
    
    isCurrentSortAsc= () => {
        return  this.gridState &&
                this.gridState.sortMember == this.member && 
            this.gridState.sortDirection == "ASC";
    }

    isCurrentSortDesc = () => {
        return this.gridState &&
            this.gridState.sortMember == this.member &&
            this.gridState.sortDirection == "DESC";
    }   

    sort = (member) => {
        if (this.gridState.sortMember != null && this.gridState.sortDirection != null
            && member.toUpperCase() === this.gridState.sortMember.toUpperCase())
            this.gridState.sortDirection = this.gridState.sortDirection === "ASC" ? "DESC" : "ASC";
        else
            this.gridState.sortDirection = "ASC";
        this.gridState.sortMember = member;
        this.props.refresh(this.gridState);
    }

    render() {       
        var showUp = this.isCurrentSortAsc();
        var showDown = this.isCurrentSortDesc();
        var showFaded = !showUp && !showDown;

        var upArrow = <i className="mdi mdi-arrow-up"></i>;
        var downArrow = <i className="mdi mdi-arrow-down"></i>;
        var fadedArrow = <span className="available-sort-indicator">
            <i className="mdi mdi-arrow-up"></i>
        </span>;
        return <span>
            <span className="sort-indicator">                
                {showUp && upArrow}
                {showDown && downArrow}
                {showFaded && fadedArrow}
             </span>
            <span className="sorter" onClick={() => this.sort(this.member)}>
                {this.text}
            </span>
        </span>
    }
}