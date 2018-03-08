import * as React from 'react';
import { Models, Services, Components } from './../../../root';
import { observer, observable, IObservableArray, IObservableValue } from './../../../mx';
import { ButtonSpec } from './../../buttons/button-spec';
import { PushButton } from './../../buttons/push-button';
import { EditRow, RowState, EditRowMode } from './edit-row'

export class EditGridProps {
    addNew: () => void;
    change: (RowState: RowState[]) => void;
    delete:(RowState)=>void;
    data?: RowState[];
    metadataCallback;
}
export class EditGridState {
    change: (data: any[]) => void;
    data?: RowState[];
    metadata: Models.UIMetadata;
}

export class EditGrid extends React.Component<EditGridProps, EditGridState> {

    constructor(props) {
        super(props);
        this.state = {} as any;
        this.createInitialState(props);
    }
    createInitialState=(props: EditGridProps) =>{

        var newState: EditGridState = { change: props.change, data: props.data, metadata: props.metadataCallback() };
        this.state = newState;
    }
    componentWillReceiveProps(nextProps) {
        this.copyPropsToState(nextProps);
    }
    copyPropsToState = (props: EditGridProps) => {
        this.setState({ data: props.data });
    }


    executeAction(action, row) {
        if (action && typeof action == 'function')
            action(row);
    }

    render() {
        //render is bound to a different instance of this than the one that the this instance that typescript uses
        //with fat arrow syntax, we'll do everything with the fat arrow instance
        return this.doRender();
    }

    rowChange = (rowState: RowState) => {
        var rows = this.state.data;
        rows[rowState.index] = rowState;
        this.state.change(rows);
    }

    doRender = () => {
        var metadata = this.state.metadata;
        var columns = Services.FormsService.getProperties(metadata);
        var rows = this.state.data.slice().map((row, index) => {
            return <EditRow
                mode={row.mode}
                model={row.model}
                form={row.form}
                rowChange={this.rowChange}
                rowDelete={this.props.delete}
                index={index} 
                columns={columns}
                key={index} />;
        });

        const headings = this.getColumnHeadings(columns);

        return <div >
            <div className='pull-right'>
                <Components.PushButton click={this.props.addNew} icon="plus-square" text="Add" theme="info" />                
            </div>
            <section className="data-table-container">
                <table className="data-table mdc-card__primary table table-bordered">
                    <thead className="mdc-typography--body2">
                        <tr>
                            <th ></th>
                            {headings}
                        </tr >
                    </thead >
                    <tbody className="mdc-typography--body1">
                        {rows}
                    </tbody>
                </table>
            </section>
        </div>;
    }

    getColumnHeadings = (columns) => {
        return columns.map((column, index, source) => {
            return <th key={column.jsName}><span >{column.displayName}</span>
            </th>;
        });
    }
}