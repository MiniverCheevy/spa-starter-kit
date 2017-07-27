import * as React from 'react';
import { observer, observable, IObservableArray, Models, Api, Components } from './../../root';

@observer
export class UserList extends React.Component<any, any>
{
    request: Models.UserListRequest = observable(Models.UserListRequest.empty());
    data: IObservableArray<Models.UserRow> = observable([]);
    metadata = Models.UserRow.metadata();

    public componentDidMount() {
        this.refresh(this.request);
    }

    public edit(user: Models.UserRow) {

    }
    public refresh = async (request: Models.UserListRequest) => {
        var response = await Api.UserList.get(request);
        if (response.isOk) {
            Object.assign(this.request, response.state);
            this.data.replace(response.data);
        }
    }
    render() {

        var buttons: Components.GridButton[] = [
            { action: this.edit, icon: 'pencil', text: 'Edit' }
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
