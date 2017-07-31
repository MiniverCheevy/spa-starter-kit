import * as React from 'react';
import { observer, observable, IObservableArray, Models, Api, Components } from './../../root';
import { ScratchNavMenu } from './scratch-navmenu';
@observer
export class ScratchTeamList extends React.Component<any, any>
{
    @observable data: IObservableArray<Models.TeamRow> = observable([]);
    request: Models.UserListRequest = Models.TeamListRequest.empty();
    metadata = Models.TeamRow.metadata();

    public componentDidMount() {
        this.refresh(this.request);
    }

    public edit(user: Models.TeamRow) {

    }
    public refresh = async (request: Models.TeamListRequest) => {
        var response = await Api.TeamList.get(request);
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
            <div>
                <ScratchNavMenu />
                <Components.Card title="Teams">
                    <Components.Grid
                        data={this.data.slice()}
                        refresh={this.refresh}
                        metadata={this.metadata}
                        buttons={buttons}
                        request={this.request}
                    ></Components.Grid>
                </Components.Card>
            </div>);
    }
}

