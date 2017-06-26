import * as React from 'react';
import { Models, observer, observable, IObservableValue, IObservableArray } from './../root';

export class PagerProps {
    gridState: Models.IGridState;
    refresh(request: Models.IGridState): void { };
}

class PageBlock
{
    page: number;
    isActive: boolean;
}
export class Pager extends React.Component<PagerProps, any>
{
    pageBlock: IObservableArray<PageBlock> = observable([]);
    isLastBlock: boolean = false;
    isFirstBlock: boolean = false;
    isFirstPage: boolean = false;
    isLastPage: boolean = false;
    hasRecords: IObservableValue<boolean> = observable(false);
    totalPages: number = 0;
    blockSize: number = 10;
    nextBlockPage: number;
    prevBlockPage: number;
    recordsVerbiage: string;
    gridState: Models.IGridState;

    constructor(props: PagerProps) {
        super(props);
        this.gridState = observable(props.gridState);
    }
    public resetPagingIfNeeded = () => {
        if (this.gridState != null && this.gridState.resetPaging)
            this.gridState.pageNumber = 1;
    };
    public setup = () => {
        if (this.gridState.totalRecords == undefined) {
            this.hasRecords.set(false);
            return;
        }
        this.hasRecords.set( this.gridState.totalRecords !== 0);

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
        this.pageBlock.replace(blocks);

        var pageNumber = this.gridState.pageNumber;
        this.prevBlockPage = min - 1;
        this.nextBlockPage = max + 1;

        this.isLastBlock =this.pageBlock.length > 0 && max >= this.totalPages;
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
        this.props.refresh(this.gridState);
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

    

    render() {
        console.log('pager render');
        console.log(this.gridState.totalRecords);
        this.setup();
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
                value={this.gridState.pageSize}
                onChange={this.props.refresh}
            >
                <option>10</option>
                <option>25</option>
                <option>50</option>
                <option>100</option>
            </select>
        </div >;
    }
    
    buttons = () => {
        //return this.pageBlock.map((block) => {
        //    
        //});

    }
    foo()
    {
       
        //{ this.button("...", this.isFirstBlock, this.firstPage(), false) }
        //{ this.button("&lt", this.isFirstPage, this.prevPage(), false) }
        //{ this.buttons() }
        //{ this.button("&lt", this.isLastPage, this.nextPage(), false) }
        //{ this.button("...", this.isLastBlock, this.nextBlock(), false) }
        //{ this.button("&laquo;", this.isLastPage, this.lastPage(), false) }
    }
    pager = () => {
        console.log('pager method');
        var buttons = this.pageBlock.map(
            (block) => {
                return <PagerButton text={block.page.toString()} isDisabled={this.isFirstPage} method={this.page(block.page)} isActive={block.isActive} />
            });
        return <div>
            <div className="pagination">
                <PagerButton text="&laquo;" isDisabled={this.isFirstBlock} method={this.firstPage} isActive={false} />
                <PagerButton text="..." isDisabled={this.isFirstPage} method={this.prevPage} isActive={false} />
                <PagerButton text="&lt;" isDisabled={this.isFirstBlock} method={this.prevBlock} isActive={false} />
                {buttons}
                <PagerButton text="&gt;" isDisabled={this.isLastPage} method={this.nextPage} isActive={false} />
                <PagerButton text="..." isDisabled={this.isLastBlock} method={this.nextBlock} isActive={false} />
                <PagerButton text="&raquo;" isDisabled={this.isLastPage} method={this.lastPage} isActive={false} />
                
            <div>
                    <span className="mdc-typography--body1">{this.recordsVerbiage}</span>
                </div>
            </div >
        </div>
    }

}
class PagerButtonProps
{
    text: string;
    isDisabled: boolean;
    method: any;// (request: Models.IGridState)=> void;
    isActive?: boolean;
}
class PagerButton extends React.Component<PagerButtonProps,any>
{
    constructor(props: PagerButtonProps) {
        super(props);        
    }

    render() {
        var text = this.props.text;
        var isDisabled = this.props.isDisabled;
        var method = this.props.method;
        var isActive = this.props.isDisabled;

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
}