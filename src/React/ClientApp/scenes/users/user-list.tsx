import * as React from 'react';
import {  Models, Api, Components, Services } from './../../root';
import { observer, observable, IObservableArray } from './../../mx';

@observer
export class UserList extends React.Component<any,any>
{
    key = 'userList';
    request: Models.UserListRequest = Services.GridService.getRequest(this.key, Models.UserListRequest.empty());
    data: IObservableArray<Models.UserRow> = observable([]);  
    metadata = Models.UserRow.metadata();

    public componentDidMount() {
        this.refresh(this.request);
    }

    public edit=(user: Models.UserRow) =>{

    }
    public refresh = async (request: Models.UserListRequest) => {
        Services.GridService.setRequest(this.key, request);
        var response = await Api.UserList.get(request);
        if (response.isOk) {
            Object.assign(this.request, response.state);            
            this.data.replace(response.data);
        }
    }
    render() {
        return this.doRender();
    }
    doRender = () => {        
        var buttons: Components.ButtonSpec[] = [
            new Components.ButtonSpec( 'Edit', 'pencil', this.edit)
        ];
        return (
            <Components.Card title="Users">
                <Components.Grid
                    data={this.data.slice()}
                    refresh={this.refresh}
                    metadata={this.metadata}
                    buttons={buttons}
                    request={this.request}
                ></Components.Grid>
            </Components.Card>);
    }
}
