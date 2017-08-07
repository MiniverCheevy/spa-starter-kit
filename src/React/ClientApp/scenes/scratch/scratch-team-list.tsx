import * as React from 'react';
import { Models, Api, Components, Services } from './../../root';
import { observer, observable, IObservableArray } from './../../mx';
import { ScratchNavMenu } from './scratch-navmenu';
@observer
export class ScratchTeamList extends React.Component<any, any>
{
    key = 'teamList';
    request: Models.TeamListRequest = Services.GridService.getRequest(this.key, Models.TeamListRequest.empty());

    @observable data: IObservableArray<Models.TeamRow> = observable([]);
    
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
        var buttons: Components.ButtonSpec[] = [
            new Components.ButtonSpec('Edit', 'pencil', this.edit)
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

