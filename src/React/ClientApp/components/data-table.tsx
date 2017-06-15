import * as React from 'react';

import { Models, Components } from './../root';
export const DataTable = (headers: ColumnHeader[], rows, request: Models.IGridState, refresh: (request: Models.IGridState) => void) => {
    var headerRow = getHeaders(headers, request, refresh);
    return <div className="mdc-card ">
        <table className="data-table mdc-card__primary">
            <thead className="mdc-typography--body2">
                {headerRow as any}
            </thead>
            <tbody className="mdc-typography--body1">
                {rows as any}
            </tbody>
        </table>

        <section className="mdc-card__supporting-text">
        </section>
        <section className="mdc-card__actions">
            PAGER
        </section>
    </div>;
}
const getHeaders=(headers: ColumnHeader[],  request: Models.IGridState, refreshMethod: (request: Models.IGridState) => void)=>
{
     var cells = headers.map(
        (header: ColumnHeader) => {
            return header.sortMember ?
                <th><Components.Sorter gridState={request} refresh={refreshMethod} member={header.sortMember} text={header.text} /></th> :
                <th>{header.text}</th>
        });
        return <tr>{cells}</tr>;
}
export class ColumnHeader
{
    text: string;
    sortMember?: string;
}
