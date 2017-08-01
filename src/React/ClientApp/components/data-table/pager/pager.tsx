import * as React from 'react';
import { Models } from './../../../root';
import { observer } from './../../../mx';
import { PageBlock } from './page-block';
import { PagerButton } from './pager-button';
export class PagerProps {
    request: Models.IGridState;
    refresh: (request: Models.IGridState) => void;
}

export class Pager extends React.Component<PagerProps, any>
{
    blocks: PageBlock[] = [];
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
    request: Models.IGridState;
    
    public resetPagingIfNeeded = () => {
        if (this.request != null && this.request.resetPaging)
            this.request.pageNumber = 1;
    };
    componentWillReceiveProps = (newProps) => {
        this.props = newProps;
    }
    public setup = () => {
        this.request = this.props.request;
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
        this.blocks = blocks;

        var pageNumber = this.request.pageNumber;
        this.prevBlockPage = min - 1;
        this.nextBlockPage = max + 1;

        this.isLastBlock = this.blocks.length > 0 && max >= this.totalPages;
        this.isFirstBlock = min === 1;
        this.isFirstPage = pageNumber === 1;
        this.isLastPage = this.totalPages === pageNumber;
        var startRow = ((pageNumber - 1) * this.request.pageSize) + 1;
        var endRow = startRow + this.request.pageSize - 1;
        if (this.isLastPage)
            endRow = this.request.totalRecords;

        this.recordsVerbiage = 'Showing ' + startRow + ' to ' + endRow + ' of ' + this.request.totalRecords;
    }
    public update = () => {
        this.page(this.request.pageNumber);
    }
    public page = (number) => {
        this.request.pageNumber = number;
        this.props.refresh(this.request);    
    };
    public firstPage = () => {
        return this.page(1);
    };
    public prevBlock = () => {
        return this.page(this.prevBlockPage);
    };
    public prevPage = () => {
        return this.page(this.request.pageNumber - 1);
    };
    public nextPage = () => {
        return this.page(this.request.pageNumber + 1);
    };
    public nextBlock = () => {
        return this.page(this.nextBlockPage);
    };
    public lastPage = () => {
        return this.page(this.totalPages);
    };
    render()
    {
        var result = this.doRender();
        return result;
    }
    doRender=()=> {
        
        this.setup();
        var noRecords = <div id="norecords" ><h3 className="mdc-typography--body2">No records found</h3></div>;
        var pager = this.getPager();
        return <div>
            {!this.hasRecords && noRecords}
            {this.hasRecords && pager}
        </div>;
    }

    getPageSizeSelector = () =>  {
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
                value={this.request.pageSize}
                onChange={this.props.refresh}
            >
                <option>10</option>
                <option>25</option>
                <option>50</option>
                <option>100</option>
            </select>
        </div >;
    }
    getPager = () => {
        var buttons = this.blocks.map(
            (block) => {
                return <PagerButton key={block.page} text={block.page.toString()} isDisabled={block.page == this.request.pageNumber} method={() => { this.page(block.page) }} isActive={block.isActive} />
            });
        return <div>
            <div className="pagination">
                <PagerButton text="&laquo;" isDisabled={this.isFirstBlock} method={this.firstPage} isActive={false} />
                <PagerButton text="..." isDisabled={this.isFirstBlock} method={this.prevBlock} isActive={false} />
                <PagerButton text="&lt;" isDisabled={this.isFirstPage} method={this.prevPage} isActive={false} />
                {buttons}
                <PagerButton text="&gt;" isDisabled={this.isLastPage} method={this.nextPage} isActive={false} />
                <PagerButton text="..." isDisabled={this.isLastBlock} method={this.nextBlock} isActive={false} />
                <PagerButton text="&raquo;" isDisabled={this.isLastBlock} method={this.lastPage} isActive={false} />

                <div>
                    <span className="mdc-typography--body1">{this.recordsVerbiage}</span>
                </div>
            </div >
        </div>;
    }
}
