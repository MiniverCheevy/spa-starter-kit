import { Models, Ng } from './../../root';

@Ng.Component({
    selector: 'pager',
    templateUrl: './pager.component.html',
    styleUrls: ['./pager.component.css']
})

export class PagerComponent implements Ng.OnChanges {    
    @Ng.Input() request: Models.IGridState;
    @Ng.Output() change = new Ng.EventEmitter();

    pageBlock: any[];
    isLastBlock: boolean = false;
    isFirstBlock: boolean = false;
    isFirstPage: boolean = false;
    isLastPage: boolean = false;
    hasRecords: boolean = false;
    totalPages: number = 0;
    blockSize: number = 10;
    nextBlockPage: number;
    prevBlockPage: number;
    recordsVerbiage: string;


    constructor() {

        this.pageBlock = [];
        this.isLastBlock = false;
        this.isFirstBlock = false;
        this.isFirstPage = false;
        this.isLastPage = false;
        this.hasRecords = false;
        this.totalPages = 0;
        this.blockSize = 5;
        this.hasRecords = false;
    }

    ngOnChanges() {
        if (!this.request || !this.request.totalPages || this.request.totalRecords == 0) {
            this.request.pageNumber = 1;
            this.recordsVerbiage = "";
            this.hasRecords = false;
            return;
        }
        else {
            this.setup();
        }
    }

    public resetPagingIfNeeded = () => {
        if (this.request != null && this.request.resetPaging)
            this.request.pageNumber = 1;
    };
    
    public setup = () => {
        if (this.request.totalRecords == undefined) {
            this.hasRecords = false;
            return;
        }
        this.hasRecords = this.request.totalRecords !== 0;

        this.totalPages = Math.ceil(this.request.totalRecords / this.request.pageSize);
        var blocks = [];
        var blockNumber = Math.ceil(this.request.pageNumber / this.blockSize) - 1;
        var blockStart = blockNumber * this.blockSize;
        var min = blockStart + 1;
        var max = blockStart + this.blockSize;

        for (let i = 1; i < this.blockSize + 1; i++) {
            if (blockStart + i < this.totalPages + 1)
                blocks.push({ page: blockStart + i, isActive: blockStart + i === this.request.pageNumber });
        }
        this.pageBlock = blocks;

        var pageNumber = this.request.pageNumber;
        this.prevBlockPage = min - 1;
        this.nextBlockPage = max + 1;

        this.isLastBlock = this.pageBlock.length > 0 && max >= this.totalPages;
        this.isFirstBlock = min === 1;
        this.isFirstPage = pageNumber === 1;
        this.isLastPage = this.totalPages === pageNumber;
        var startRow = ((pageNumber - 1) * this.request.pageSize) + 1;
        var endRow = startRow + this.request.pageSize - 1;
        if (this.isLastPage)
            endRow = this.request.totalRecords;

        this.recordsVerbiage = 'Showing ' + startRow + ' to ' + endRow + ' of ' + this.request.totalRecords;
    }
    public changePageSize()
    {
        this.request.pageNumber = 1;
        this.update();
    }
    public update = () => {
        this.page(<number>this.request.pageNumber);
    }
    public page = (number: number) => {
        this.request.pageNumber = number;
        this.change.emit({});
        console.log('pager emit');
    };
    public firstPage = () => {
        return this.page(1);
    };
    public prevBlock = () => {
        return this.page(this.prevBlockPage);
    };
    public prevPage = () => {
        return this.page(<number>this.request.pageNumber - 1);
    };
    public nextPage = () => {
        return this.page(<number>this.request.pageNumber + 1);
    };
    public nextBlock = () => {
        return this.page(this.nextBlockPage);
    };
    public lastPage = () => {
        return this.page(this.totalPages);
    };
}



