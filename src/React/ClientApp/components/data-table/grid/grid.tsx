import * as React from 'react';
import { observer,observable, Models, Services } from './../../../root';
import { Sorter } from './../sorter';
import { Pager } from './../pager/pager';
import { GridButton } from './grid-button';
import { PushButton } from './../../buttons/push-button';

export class GridProps {
    request: Models.IGridState = { sortMember: '', sortDirection: '' };
    refresh: (request: Models.IGridState) => void;
    buttons?: GridButton[];
    metadata;
    data: any[];
}
@observer
export class Grid extends React.Component<GridProps, any>
{
    columns: Models.UIMetadata[] = [];
    constructor(props: GridProps)
    {        
        super(props);
    }
    componentWillReceiveProps=(newProps)=> {
        this.props = newProps;
    }
    executeAction(action, row) {
        if (action && typeof action == "function")
            action(row);
    }
    render() {
        return this.doRender();
    }
    doRender = () => {       
        this.columns = Services.FormsService.getProperties(this.props.metadata);
        const headings = this.getColumnHeadings();
        const rows = this.getRows();
        return <div className="mdc-card">
            <section className="mdc-card__supporting-text data-table-container">
            <table className="data-table mdc-card__primary">
                <thead className="mdc-typography--body2">
                    <tr>
                        {this.props.buttons && this.props.buttons.length > 0 && <th ></th>}
                        {headings}
                    </tr >
                </thead >
                <tbody className="mdc-typography--body1">
                    {rows}
                </tbody>
            </table>
            </section>
            <section className="mdc-card__actions pager-container">
                <Pager request={this.props.request} refresh={this.props.refresh}></Pager>
            </section>
        </div>;
    }

    getColumnHeadings=()=> {
        return this.columns.map((column) => {
            var dontSort = column.doNotSort != null && column.doNotSort;
            return <th key={column.jsName}>
                {dontSort && <span >{column.displayName}</span>}
                {!dontSort &&
                    <Sorter member={column.propertyName}
                        text={column.displayName}
                        request={this.props.request}
                        refresh={this.props.refresh}></Sorter >}
            </th>;
        });
    }
    
    getRows = () => {
        const buttons = this.props.buttons;
        const hasButtons = buttons && buttons.length > 0;
        
        return this.props.data.map((row, index) => {
            const rowButtons = buttons.map((button) => {
                return <PushButton theme="grid-icon" key={button.key}
                    text={button.text} icon={button.icon} click={this.executeAction(button.action, row)}
                ></PushButton>;
            });
            const cells = this.columns.map((column) => {
                return <td key={column.jsName} className={"format-"+column.displayFormat} >{Services.FormatService.formatForDisplay(row[column.jsName], column)}</td>;
            });
            return <tr key={index}>
                    {hasButtons && <td className="button-column">{rowButtons}</td>}
                    {cells}
                   </tr>;
        });
    }

}