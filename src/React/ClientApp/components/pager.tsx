//import * as React from 'react';
//import * as Api from './../api.generated';
//import * as Models from './../models.generated';

//export class PagerProps
//{
//    request: Models.IGridState;
//    onChanged(request: Models.IGridState): void { };
//}
//export class Pager extends React.Component<PagerProps,any>
//{
//    constructor(props) {
//        super(props);        
//    }
    
    
//    render() {
//        if (!this.hasRecords)
//        {
//           return(<div id="norecords">
//               <h3>No records found</h3>
//           </div >);
//        }else{ return(<div>
//                <div className="pagination" show.bind="hasRecords" style="margin: 0px 0px 0px 0px;">
//            <div style="float: left; margin-right: 20px; white-space: nowrap;">
//                    <label style="float: left; margin-right: 4px; margin-top: 6px;">Page Size</label>
//                    <select value.bind="gridState.pageSize" matcher.bind="selectMatcher"
//                        change.delegate="update()"
//                        className="form-control" style="width: 80px; float: left;">
//                    <option >10</option>
//                    <option >25</option>
//                    <option >50</option>
//                    <option >100</option>
//                </select>
//            </div>
//            <div style="float:left; vertical-align:top;">
//                <ul className="pagination" style="margin: 0px 0px 0px 0px;">
//                    <li ng-className="${ isFirstPage ? 'disabled' : ''}">
//                        <a disabled.bind="isFirstPage"
//                           click.delegate="firstPage()">&laquo;</a>
//                    </li>
//                <li ng-className="${ isFirstBlock ? 'disabled' : ''}">
//                    <a disabled.bind="isFirstBlock"
//                           click.delegate="prevBlock()">...</a>
//                    </li>
//            <li ng-className="${ isFirstPage ? 'disabled' : ''}">
//                <a disabled.bind="isFirstPage"
//                           click.delegate="prevPage()">&lt</a>
//                    </li >
//            <li repeat.for="block of pageBlock" className="${block.isActive ? 'active' : ''}">
//                <a click.delegate="page(block.page)">${block.page}</a>
//                    </li >
//            <li ng-className="${ isLastPage ? 'disabled' : ''}">
//                <a disabled.bind="isLastPage"
//                           click.delegate="nextPage()">&gt</a>
//                    </li >
//            <li ng-className="${ isLastBlock ? 'disabled' : ''}">
//                <a disabled.bind="isLastBlock"
//                           click.delegate="nextBlock()">...</a>
//                    </li >
//            <li ng-className="${ isLastPage ? 'disabled' : ''}">
//                <a disabled.bind="isLastPage"
//                           click.delegate="lastPage()">&raquo</a>
//                    </li >
//                </ul >
//            <div>
//                ${recordsVerbiage}
//            </div>
//            </div >)
//                }
//        )
//        );
//    }

//    public resetPagingIfNeeded = () => {
//        if (this.props.request != null && this.props.request.resetPaging)
//            this.props.request.pageNumber = 1;
//    };
//    public setup = () => {
//        if (this.props.request.totalRecords == undefined) {
//            this.hasRecords = false;
//            return;
//        }
//        this.hasRecords = this.props.request.totalRecords !== 0;

//        this.totalPages = Math.ceil(this.props.request.totalRecords / this.props.request.pageSize);
//        var blocks = [];
//        var blockNumber = Math.ceil(this.props.request.pageNumber / this.blockSize) - 1;
//        var blockStart = blockNumber * this.blockSize;
//        var min = blockStart + 1;
//        var max = blockStart + this.blockSize;

//        for (let i = 1; i < this.blockSize + 1; i++) {
//            if (blockStart + i < this.totalPages + 1)
//                blocks.push({ page: blockStart + i, isActive: blockStart + i === this.props.request.pageNumber });
//        }
//        this.pageBlock = blocks;

//        var pageNumber = this.props.request.pageNumber;
//        this.prevBlockPage = min - 1;
//        this.nextBlockPage = max + 1;

//        this.isLastBlock = this.pageBlock.length > 0 && max >= this.totalPages;
//        this.isFirstBlock = min === 1;
//        this.isFirstPage = pageNumber === 1;
//        this.isLastPage = this.totalPages === pageNumber;
//        var startRow = ((pageNumber - 1) * this.props.request.pageSize) + 1;
//        var endRow = startRow + this.props.request.pageSize - 1;
//        if (this.isLastPage)
//            endRow = this.props.request.totalRecords;

//        this.recordsVerbiage = 'Showing ' + startRow + ' to ' + endRow + ' of ' + this.props.request.totalRecords;
//    }
//    public update = () => {
//        this.page(this.props.request.pageNumber);
//    }
//    public page = (number) => {
        
//        this.props.request.pageNumber = number;
//        this.props.onChanged(this.props.request);
//    };
//    public firstPage = () => {
//        return this.page(1);
//    };
//    public prevBlock = () => {
//        return this.page(this.prevBlockPage);
//    };
//    public prevPage = () => {
//        return this.page(this.props.request.pageNumber - 1);
//    };
//    public nextPage = () => {
//        return this.page(this.props.request.pageNumber + 1);
//    };
//    public nextBlock = () => {
//        return this.page(this.nextBlockPage);
//    };
//    public lastPage = () => {
//        return this.page(this.totalPages);
//    };

//    pageBlock: any[];
//    isLastBlock: boolean = false;
//    isFirstBlock: boolean = false;
//    isFirstPage: boolean = false;
//    isLastPage: boolean = false;
//    hasRecords: boolean = false;
//    totalPages: number = 0;
//    blockSize: number = 10;
//    nextBlockPage: number;
//    prevBlockPage: number;
//    recordsVerbiage: string;
//}