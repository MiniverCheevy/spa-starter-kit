import * as  Models from './../models.generated';
import { bindable, inject, customElement } from 'aurelia-framework';


@customElement('pager')
@inject(Element)
export class Pager {
    @bindable gridState: Models.IGridState;

    constructor(element) {
        this.element = element;
        this.pageBlock = [];
        this.isLastBlock = false;
        this.isFirstBlock = false;
        this.isFirstPage = false;
        this.isLastPage = false;
        this.hasRecords = false;
        this.totalPages = 0;
        this.blockSize = 10;
        this.hasRecords = null;
        this.selectMatcher = (left, right) => {
            return left.value === right.value;
        };
    }
    public gridStateChanged(newValue, oldValue) {
        if (newValue != null) {
            this.setup();
        }
    }

    public resetPagingIfNeeded = () => {
        if (this.gridState != null && this.gridState.resetPaging)
            this.gridState.pageNumber = 1;
    };
    public setup = () => {
        if (this.gridState.totalRecords == undefined) {
            this.hasRecords = false;
            return;
        }
        this.hasRecords = this.gridState.totalRecords !== 0;

        this.totalPages = Math.ceil(this.gridState.totalRecords / this.gridState.pageSize);
        var blocks = [];
        var blockNumber = Math.ceil(this.gridState.pageNumber / this.blockSize) - 1;
        var blockStart = blockNumber * this.blockSize;
        var min = blockStart + 1;
        var max = blockStart + this.blockSize;

        for (let i = 1; i < this.blockSize + 1; i++) {
            if (blockStart + i < this.totalPages + 1)
                blocks.push({ page: blockStart + i, isActive: blockStart + i === this.gridState.pageNumber });
        }
        this.pageBlock = blocks;

        var pageNumber = this.gridState.pageNumber;
        this.prevBlockPage = min - 1;
        this.nextBlockPage = max + 1;

        this.isLastBlock = this.pageBlock.length > 0 && max >= this.totalPages;
        this.isFirstBlock = min === 1;
        this.isFirstPage = pageNumber === 1;
        this.isLastPage = this.totalPages === pageNumber;
        var startRow = ((pageNumber - 1) * this.gridState.pageSize) + 1;
        var endRow = startRow + this.gridState.pageSize - 1;
        if (this.isLastPage)
            endRow = this.gridState.totalRecords;

        this.recordsVerbiage = 'Showing ' + startRow + ' to ' + endRow + ' of ' + this.gridState.totalRecords;
    }
    public update = () => {
        this.page(this.gridState.pageNumber);
    }
    public page = (number) => {
        this.gridState.pageNumber = number;
        let e = new CustomEvent('change', {
            bubbles: true
        });
        this.element.dispatchEvent(e);
    };
    public firstPage = () => {
        return this.page(1);
    };
    public prevBlock = () => {
        return this.page(this.prevBlockPage);
    };
    public prevPage = () => {
        return this.page(this.gridState.pageNumber - 1);
    };
    public nextPage = () => {
        return this.page(this.gridState.pageNumber + 1);
    };
    public nextBlock = () => {
        return this.page(this.nextBlockPage);
    };
    public lastPage = () => {
        return this.page(this.totalPages);
    };

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
    element;
    selectMatcher;
}