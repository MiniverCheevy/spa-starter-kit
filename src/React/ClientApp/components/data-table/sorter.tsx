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
    request: Models.IGridState = { sortMember: '', sortDirection: '' };
    
    isCurrentSortAsc= () => {
        return  this.request &&
                this.request.sortMember == this.member && 
            this.request.sortDirection == "ASC";
    }

    isCurrentSortDesc = () => {
        return this.request &&
            this.request.sortMember == this.member &&
            this.request.sortDirection == "DESC";
    }   

    sort = (member) => {
        console.log('Sort Changed');
        if (this.request.sortMember != null && this.request.sortDirection != null
            && member.toUpperCase() === this.request.sortMember.toUpperCase())
            this.request.sortDirection = this.request.sortDirection === "ASC" ? "DESC" : "ASC";
        else
            this.request.sortDirection = "ASC";
        this.request.sortMember = member;
        this.props.refresh(this.request);
    }
    render() {
        return this.doRender();
    }
    doRender = () => { 
        this.request = this.props.request;
        this.text = this.props.text;
        this.member = this.props.member;

        var showUp = this.isCurrentSortAsc();
        var showDown = this.isCurrentSortDesc();
        var showFaded = !showUp && !showDown;

        var upArrow = <i className="mdi mdi-arrow-up"></i>;
        var downArrow = <i className="mdi mdi-arrow-down"></i>;
        var fadedArrow = <span className="available-sort-indicator">
            <i className="mdi mdi-sort"></i>
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
        </span>;
    }
}