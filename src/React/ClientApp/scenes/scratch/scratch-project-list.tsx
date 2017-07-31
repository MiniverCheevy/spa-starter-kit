import * as React from 'react';
import { observer, observable, IObservableArray, Models, Api, Components } from './../../root';
import { ScratchNavMenu } from './scratch-navmenu';
@observer
export class ScratchProjectList extends React.Component<any, any>
{
    @observable data: IObservableArray<Models.ProjectRow> = observable([]);
    request: Models.UserListRequest = Models.ProjectListRequest.empty();
    metadata = Models.ProjectRow.metadata();

    public componentDidMount() {
        this.refresh(this.request);
    }

    public edit(user: Models.ProjectRow) {

    }
    public refresh = async (request: Models.ProjectListRequest) => {
        var response = await Api.ProjectList.get(request);
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
                <Components.Card title="Projects">
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

