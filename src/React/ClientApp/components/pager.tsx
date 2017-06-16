import * as React from 'react';
import { Models } from './../models.generated';

export class PagerProps {
    request: Models.IGridState;
    refresh(request: Models.IGridState): void { };
}
export class Pager extends React.Component<PagerProps, any>
{
    constructor(props) {
        super(props);
    }
    public resetPagingIfNeeded = () => {
        if (this.props.request != null && this.props.request.resetPaging)
            this.props.request.pageNumber = 1;
    };
    public setup = () => {
        if (this.props.request.totalRecords == undefined) {
            this.hasRecords = false;
            return;
        }
        this.hasRecords = this.props.request.totalRecords !== 0;

        this.totalPages = Math.ceil(this.props.request.totalRecords / this.props.request.pageSize);
        var blocks = [];
        var blockNumber = Math.ceil(this.props.request.pageNumber / this.blockSize) - 1;
        var blockStart = blockNumber * this.blockSize;
        var min = blockStart + 1;
        var max = blockStart + this.blockSize;

        for (let i = 1; i < this.blockSize + 1; i++) {
            if (blockStart + i < this.totalPages + 1)
                blocks.push({ page: blockStart + i, isActive: blockStart + i === this.props.request.pageNumber });
        }
        this.pageBlock = blocks;

        var pageNumber = this.props.request.pageNumber;
        this.prevBlockPage = min - 1;
        this.nextBlockPage = max + 1;

        this.isLastBlock = this.pageBlock.length > 0 && max >= this.totalPages;
        this.isFirstBlock = min === 1;
        this.isFirstPage = pageNumber === 1;
        this.isLastPage = this.totalPages === pageNumber;
        var startRow = ((pageNumber - 1) * this.props.request.pageSize) + 1;
        var endRow = startRow + this.props.request.pageSize - 1;
        if (this.isLastPage)
            endRow = this.props.request.totalRecords;

        this.recordsVerbiage = 'Showing ' + startRow + ' to ' + endRow + ' of ' + this.props.request.totalRecords;
    }
    public update = () => {
        this.page(this.props.request.pageNumber);
    }
    public page = (number) => {

        this.props.request.pageNumber = number;
        this.props.refresh(this.props.request);
    };
    public firstPage = () => {
        return this.page(1);
    };
    public prevBlock = () => {
        return this.page(this.prevBlockPage);
    };
    public prevPage = () => {
        return this.page(this.props.request.pageNumber - 1);
    };
    public nextPage = () => {
        return this.page(this.props.request.pageNumber + 1);
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

    render() {
        var noRecords = <div id="norecords" ><h3 className="mdc-typography--body2">No records found</h3></div>;
        var pager = this.pager();

        return <div>
            {!this.hasRecords && noRecords}
            {this.hasRecords && pager}
        </div>;
    }



    pageSizeSelector = () => {
        var wrapperStyle =
            {
                float: 'left',
                marginRight: '20px',
                whiteSpace: 'nowrap'

            };
        var labelStyle = {
            float: 'left',
            marginRight: '20px',
            whiteSpace: 'nowrap'
        }
        return <div className="mdc-form-field">
            <label>Page Size</label>
            <select
                value={this.props.request.pageSize}
                onChange={this.props.refresh}
            >
                <option>10</option>
                <option>25</option>
                <option>50</option>
                <option>100</option>
            </select>
        </div >;
    }
    button = (text, isDisabled, method, isActive) => {
        var classes = "mdc-button mdc-button--raised mdc-button--compact mdc-ripple-upgraded mdc-ripple-upgraded--foreground-deactivation";
        if (isActive)
            classes = "mdc-button mdc-button--raised mdc-button--primary mdc-ripple-upgraded mdc-ripple-upgraded--foreground-deactivation";
        if (!isDisabled) {
            return <button type="button"
                className={classes}
                onClick={method}> {text}</button>
        }
        else {
            return <button type="button" disabled={true}
                className={classes}
            >{text}</button>
        }
    };
    buttons = () => {
        return this.pageBlock.map((block) => {
            return this.button(block.page.ToString(), false, this.page(block.page), block.isActive);
        });

    }
    pager = () => {
        return <div>
            <div className="pagination">
                this.button("&laquo;",this.isFirstPage,firstPage());
                this.button("...",this.isFirstBlock,firstBlock());
                this.button("&lt",this.isFirstPage,prevPage());
                this.buttons();
                this.button("&lt",this.isLastPage,nextPage());
                this.button("...",this.isLastBlock,nextBlock());
                this.button("&laquo;",this.isLastPage,lastPage());
            <div>
                    <span className="mdc-typography--body1">{this.recordsVerbiage}</span>
                </div>
            </div >
        </div>
    }

}