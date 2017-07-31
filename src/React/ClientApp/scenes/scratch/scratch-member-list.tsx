
import * as React from 'react';
import { observer, observable, IObservableArray, Models, Api, Components } from './../../root';

@observer
export class ScratchMemberList extends React.Component<any, any>
{
    @observable data: IObservableArray<Models.MemberRow> = observable([]);
    request: Models.UserListRequest = Models.MemberListRequest.empty();
    metadata = Models.MemberRow.metadata();

    public componentDidMount() {
        this.refresh(this.request);
    }

    public edit(user: Models.MemberRow) {

    }
    public refresh = async (request: Models.MemberListRequest) => {
        var response = await Api.MemberList.get(request);
        if (response.isOk) {
            Object.assign(this.request, response.state);
            this.data.replace(response.data);
        }
    }
    render() {
        var buttons: Components.GridButton[] = [
            new Components.GridButton('Edit', 'pencil', this.edit)
        ];

        return (
            <Components.Card title="Members">
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
