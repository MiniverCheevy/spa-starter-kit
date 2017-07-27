import * as React from 'react';
import { observable, Models, Services } from './../../../root';
import { Sorter } from './../sorter';
import { Pager } from './../pager/pager';
import { GridButton } from './grid-button';
import { PushButton } from './../../buttons/push-button';

export class GridProps {
    request: Models.IGridState = { sortMember: '', sortDirection: '' };
    refresh: (request: Models.IGridState) => void;
    buttons?: GridButton[]
    metadata;
    data: any[];
}
export class Grid extends React.Component<GridProps, any>
{
    columns: Models.UIMetadata[] = [];

    executeAction = (action, row) => {
        if (action && typeof action == "function")
            action(row);
    }

    render() {        
        this.columns = Services.FormsService.getProperties(this.props.metadata);
        debugger;
        return <div className="mdc-card">
            <table className="data-table mdc-card__primary">
                <thead className="mdc-typography--body2">
                    <tr>
                        {this.props.buttons && this.props.buttons.length > 0 && <th ></th>}
                        {this.columnHeadings(this.columns)}
                    </tr >
                </thead >
                <tbody className="mdc-typography--body1">
                    {this.props.data.map(this.rows)}
                </tbody>
            </table>
            <section className="mdc-card__supporting-text"></section>
            <section className="mdc-card__actions pager-container">
                <Pager request={this.props.request} refresh={this.props.refresh}></Pager>
            </section>
        </div>
    }

    columnHeadings = (columns: Models.UIMetadata[]) => {
        debugger;
        columns.map((column) => {
            var shouldSort = column.doNotSort && column.doNotSort == true;
            <th>
                {!shouldSort && <span> {column.displayName}</span>}
                {shouldSort &&
                    <Sorter member={column.propertyName}
                        text={column.displayName}
                        request={this.props.request}
                        refresh={this.props.refresh}></Sorter >}
            </th>
        });
    }

    rows = (row, columns) => {
        let buttons = this.props.buttons;
        let hasButtons = buttons && buttons.length > 0;
        let rowButtons = buttons.map((button) => {
            <PushButton theme="primary"
                text={button.text} icon={button.icon} click={this.executeAction(button.action, row)}
            ></PushButton>
        });
        let cells = columns.map((column) => {
            <td>{Services.FormatService.format(row[column.jsName], column)}</td>
        });
        <tr>
            hasButtons && <td className="button-column" >{rowButtons}</td >
            {cells}
        </tr>
    }

}