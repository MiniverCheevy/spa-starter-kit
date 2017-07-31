﻿import * as React from 'react';
import { observer, observable, IObservableArray, Models, Api, Components } from './../../root';

@observer
export class UserList extends React.Component<any, any>
{
    @observable data: IObservableArray<Models.UserRow> = observable([]);
    request: Models.UserListRequest = Models.UserListRequest.empty();    
    metadata = Models.UserRow.metadata();

    public componentDidMount() {
        this.refresh(this.request);
    }

    public edit=(user: Models.UserRow) =>{

    }
    public refresh = async (request: Models.UserListRequest) => {
        var response = await Api.UserList.get(request);
        if (response.isOk) {
            console.log('refresh=>ok=>' + response.data.length)
            Object.assign(this.request, response.state);
            this.data.replace(response.data);
        }
    }
    render() {
        return this.doRender();
    }
    doRender = () => {
        console.log('User List Render');
        var buttons: Components.GridButton[] = [
            new Components.GridButton( 'Edit', 'pencil', this.edit)
        ];

        return (
            <Components.Card title="Users">
                <Components.Grid
                    data={this.data}
                    refresh={this.refresh}
                    metadata={this.metadata}
                    buttons={buttons}
                    request={this.request}
                ></Components.Grid>
            </Components.Card>);
    }
}
