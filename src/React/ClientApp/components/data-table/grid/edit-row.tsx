import * as React from 'react';
import { Models, Services, Components } from './../../../root';
import { observer, observable, IObservableArray, IObservableValue } from './../../../mx';
import { Sorter } from './../sorter';
import { Pager } from './../pager/pager';
import { ButtonSpec } from './../../buttons/button-spec';
import { PushButton } from './../../buttons/push-button';


export enum EditRowMode {
    View = 1,
    Add = 2,
    Edit = 3

}



export class RowState {
    mode: EditRowMode;
    model: any;
    index: number;
    form: Components.Form;
}
export class EditRowProps {
    mode: EditRowMode;
    model: any;
    form: Components.Form;
    rowChange: (rowState: RowState) => void;
    rowDelete: (rowState: RowState) => void;
    index: number;
    columns: Models.UIMetadata[];
}
export class EditRowState {
    mode: EditRowMode;
    model: any;
    backupModel: any;
    form: Components.Form;
    rowChange: (rowState: RowState) => void;
    rowDelete: (rowState: RowState) => void;
    index: number;
    columns: Models.UIMetadata[];
}


export class EditRow extends React.Component<EditRowProps, EditRowState> {

    constructor(props) {
        super(props);
        this.state = {} as any;
        this.copyPropsToState(props, false);
    }

    componentWillReceiveProps(nextProps) {
        this.copyPropsToState(nextProps, true);
    }

    copyPropsToState = (props, useSetState: boolean) => {
        var newState = {
            mode: props.mode,
            model: props.model,
            backupModel: Object.assign({}, props.model),
            form: props.form,
            rowChange: props.rowChange,
            rowDelete: props.rowDelete,
            index: props.index,
            columns: props.columns,
        };
        if (useSetState)
            this.setState(newState);
        else
            this.state = newState;
    }

    render() {
        //render is bound to a different instance of this than the one that the this instance that typescript uses
        //with fat arrow syntax, we'll pass the props in and do everything with the fat arrow instance
        return this.doRender();
    }

    doRender = () => {
        const cells = this.state.columns.map((column, index, source) => {
            return this.getComponent(column, this.state.model);

        });
        return <tr>
                   <td>
                       {this.state.mode === EditRowMode.View &&
                           <PushButton theme="grid-icon"
                                       text="edit" icon="pencil" click={() => { this.executeAction(this.edit, this.state.model) }}>
                           </PushButton>}
                       {this.state.mode === EditRowMode.View &&
                           <PushButton theme="grid-icon"
                               text="delete" icon="trash" click={() => { this.executeAction(this.delete, this.state.model) }}>
                           </PushButton>}
                       {this.state.mode === EditRowMode.Edit &&
                           <PushButton theme="grid-icon"
                                       text="save" icon="save" click={() => { this.executeAction(this.saveEdit, this.state.model) }}>
                           </PushButton>}
                       {this.state.mode === EditRowMode.Add &&
                           <PushButton theme="grid-icon"
                                       text="save" icon="save" click={() => { this.executeAction(this.saveAdd, this.state.model) }}>
                           </PushButton>}
                       {this.state.mode === EditRowMode.Edit &&
                           <PushButton theme="grid-icon"
                                       text="cancel" icon="ban" click={() => { this.executeAction(this.cancelEdit, this.state.model) }}>
                           </PushButton>}
                       {this.state.mode === EditRowMode.Add &&
                           <PushButton theme="grid-icon"
                                       text="cancel" icon="ban" click={() => { this.executeAction(this.doDelete, this.state.model) }}>
                           </PushButton>}
                       
                   </td>
                   {cells}
               </tr>;

    }

    executeAction(action, row) {
        if (action && typeof action == 'function')
            action(row);
    }

    edit = () => {
        this.state.rowChange({            
            form: this.state.form,
            model: this.state.model,
            index: this.state.index,
            mode: EditRowMode.Edit
        });
    }
    cancelEdit = () => {
        this.state.form.clearPreviousValidationResults();
        this.state.rowChange({
            form: this.state.form,
            model: this.state.backupModel,
            index: this.state.index,
            mode: EditRowMode.View
        });
    }
    
    saveEdit = () => {
        if (this.state.form.isValid)
            this.state.rowChange({
                form: this.state.form,
                model: this.state.model,
                index: this.state.index,
                mode: EditRowMode.View
            });
    }
    saveAdd = () => {
        if (this.state.form.isValid)
            this.state.rowChange({
                form: this.state.form,
                model: this.state.model,
                index: this.state.index,
                mode: EditRowMode.View
            });
    }
    
    delete = () => {
        Services.MessengerService.confirm('Are you sure you want to delete this contact?', this.doDelete);
    }
    doDelete = () => {
        this.state.rowDelete({
            form: this.state.form,
            model: this.state.model,
            index: this.state.index,
            mode: EditRowMode.View
        });
    }

    onRowValueChange = (key, value, form: Components.Form) => {
        var isValid = form.isValid;
        Object.assign(this.state.form, form);
        Object.assign(this.state.form.metadata[key], form.metadata[key]);
        var model = this.state.model;
        model[key] = value;
        this.setState({ model: model });
    }

   
    getEditComponent = (column: Models.UIMetadata, model) => {

        var test = model[column.jsName];
        return <td key={column.jsName + 'td'}>
            <Components.InputDynamic noLabel={true} form={this.state.form} model={this.state.model} change=
                {this.onRowValueChange} name={column.jsName} key={column.jsName}></Components.InputDynamic>
        </td>;

    }

    getDisplayComponent = (column: Models.UIMetadata, model) => {
        const className: string = "format-" + column.displayFormat;
        const raw = model[column.jsName];
        const value = Services.FormatService.formatForDisplay(raw, column);
        return <td key={column.jsName+'td'} className={className}>{value}</td>;
    }

    getComponent = (column: Models.UIMetadata, model) => {
        if (this.props.mode === EditRowMode.View) {
            return this.getDisplayComponent(column, model);
        } else {
            return this.getEditComponent(column, model);
        }

    }
}