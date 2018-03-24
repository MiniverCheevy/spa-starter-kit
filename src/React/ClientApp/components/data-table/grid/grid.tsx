import * as React from 'react';
import { Models, Services } from './../../../root';
import { Sorter } from './../sorter';
import { Pager } from './../pager/pager';
import { ButtonSpec } from './../../buttons/button-spec';
import { PushButton } from './../../buttons/push-button';
import { InputCheck } from './../../forms/input-check'
import { Form } from './../../forms/form';

export class SelectionCheckBox {
    propertyName: string;
    checkChanged: (model: any) => void;
    enableIf?: (model: any) => boolean;
}

export class GridProps {
    doNotPageOrSort?: boolean = false;
    request?: Models.IGridState = { sortMember: '', sortDirection: '' };
    refresh?: (request: Models.IGridState) => void;
    buttons?: ButtonSpec[];
    metadata;
    data: any[];
    selectionCheckBox?: SelectionCheckBox;
}

export class Grid extends React.Component<GridProps, any>
{
    columns: Models.UIMetadata[] = [];
    constructor(props: GridProps) {
        super(props);
        this.createInitialState(props);
    }
    createInitialState = (props: GridProps) => {
        var data = props.data || [];
        this.state = { data: data };
    }
    componentWillReceiveProps(nextProps) {
        this.copyPropsToState(nextProps);
    }
    copyPropsToState = (props) => {
        var data = props.data || [];
        this.setState({ data: data });
    }
    executeAction=(action, row)=> {
        if (action && typeof action == "function")
            action(row);
    }

    handleCheckChanged = (key, value, row) => {
        row[key] = value;
        this.props.selectionCheckBox.checkChanged(row);
    }
    render() {
        //render is bound to a different instance of this than the one that the this instance that typescript uses
        //with fat arrow syntax, we'll pass the props in and do everything with the fat arrow instance
        return this.doRender(this.props);
    }
    doRender = (props: GridProps) => {
       this.props = props;
        this.columns = Services.FormsService.getProperties(this.props.metadata);
        const headings = this.getColumnHeadings();
        const rows = this.getRows();
        var showPager = this.props.doNotPageOrSort == null || this.props.doNotPageOrSort === false;
        return <div >
            <section className="data-table-container">
            <table className="data-table mdc-card__primary">
                <thead className="mdc-typography--body2">
                    <tr>
                        {this.props.buttons && this.props.buttons.length > 0 && <th ></th>}
                            {this.props.selectionCheckBox && <th></th>}
                        {headings}
                    </tr >
                </thead >
                <tbody className="mdc-typography--body1">
                    {rows}
                </tbody>
            </table>
            </section>
            <section className="mdc-card__actions pager-container">
                {showPager && <Pager request={this.props.request} refresh={this.props.refresh}></Pager>}
            </section>
        </div>;
    }
   
    getColumnHeadings=()=> {
        return this.columns.map((column, index, source) => {
            var dontSort = (column.doNotSort != null && column.doNotSort) || this.props.doNotPageOrSort;
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
        let rowButtons = null;
        return this.state.data.map((row, index) => {
            if (hasButtons) {
            rowButtons = buttons.map((button) => {
                if (button.showIf == null || button.showIf(row)) {
                    if (button.icon != null)
                    return <PushButton theme="grid-icon" key={button.key}
                            text={button.text} icon={button.icon} click={() => { this.executeAction(button.action, row) }}></PushButton>;
                    else
                        return <PushButton theme="primary" key={button.key}
                            text={button.text} compact={true}
                            click={() => { this.executeAction(button.action, row) }}></PushButton>;
                } else
                    return null;
            });
            }
            var checkBoxColumn = null;
            if (this.props.selectionCheckBox) {
                const key = 'checkboxColumn' + index;
                const name = this.props.selectionCheckBox.propertyName;                
                const form = new Form(this.props.metadata);
                let readOnly = false;

                if (this.props.selectionCheckBox.enableIf) {
                    readOnly = !this.props.selectionCheckBox.enableIf(row);                  
                } 
                checkBoxColumn = <td key={key}>
                    <InputCheck model={row} name={name} form={form}
                        noLabel={true} readOnly={readOnly}
                        change={(key, value) => { this.handleCheckChanged(key, value,row) } } 
                    />
                    </td >}
            const cells = this.columns.map((column, index, source) => {
                var className = "format-" + column.displayFormat;
                var value = Services.FormatService.formatForDisplay(row[column.jsName], column);
                return <td key={column.jsName} className={className}>{value}</td>;
            });
            return <tr key={index}>
                {hasButtons && <td className="button-column">{rowButtons}</td>}
                {this.props.selectionCheckBox && checkBoxColumn}
                {cells}
            </tr>;
        });
    }

}