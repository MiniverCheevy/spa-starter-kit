import { Models, Ng } from './../../root';

@Ng.Component({
    selector: 'sorter',
    templateUrl: './sorter.component.html',
    styleUrls: ['./sorter.component.css']
})
export class SorterComponent implements Ng.OnChanges {
    
    @Ng.Input() member: string = '';
    @Ng.Input() text: string = '';
    @Ng.Input() request: Models.IGridState;
    @Ng.Output() change = new Ng.EventEmitter();
    currentSortKey: string;
    currentSort: string;
    isCurrentSortAsc: boolean;
    isCurrentSortDesc: boolean;
    constructor() {
        this.currentSortKey = '';
    }

    ngOnChanges() {
        if (!this.request)
            return;
        this.setup();
    }


    setup = () => {
        this.currentSort = this.request.sortDirection + this.request.sortMember;
        this.isCurrentSortAsc = this.checkIfCurrentSortAsc();
        this.isCurrentSortDesc = this.checkIfCurrentSortDesc();
    }

    checkIfCurrentSortAsc = () => {
        if (this.request === null || this.request.sortMember == null || this.request.sortDirection == null)
            return false;
        return this.request.sortMember.toUpperCase() === this.member.toUpperCase()
            && this.request.sortDirection.toUpperCase() === "ASC";
    }

    checkIfCurrentSortDesc = () => {
        if (this.request === null || this.request.sortMember == null || this.request.sortDirection == null)
            return false;
        return this.request.sortMember.toUpperCase() === this.member.toUpperCase()
            && this.request.sortDirection.toUpperCase() === "DESC";
    }

    sort = () => {
        this.setup();
        if (this.request.sortMember &&
            this.member.toUpperCase() === this.request.sortMember.toUpperCase())
            this.request.sortDirection = this.request.sortDirection === "ASC" ? "DESC" : "ASC";
        else
            this.request.sortDirection = "ASC";
        this.request.sortMember = this.member;

        this.change.emit({});
        console.log('sort emit');

    }


}
