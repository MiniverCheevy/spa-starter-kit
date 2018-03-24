import * as React from 'react';
import {  Models, Api, Components, Services } from './../../root';
import { observer, observable, IObservableArray } from './../../mx';
import { ScratchNavMenu } from './scratch-navmenu';

@observer
export class ScratchProjectList extends React.Component<any,any>
{
    key = 'projectList';
    request: Models.UserListRequest = Services.GridService.getRequest(this.key, Models.UserListRequest.empty());
    @observable data: IObservableArray<Models.ProjectRow> = observable([]);
    metadata = Models.ProjectRow.metadata();

    public componentDidMount() {
        this.refresh(this.request);
    }

    public edit(user: Models.ProjectRow) {

    }
    public refresh = async (request: Models.ProjectListRequest) => {
        Services.GridService.setRequest(this.key, request);
        var response = await Api.ProjectList.get(request);
        if (response.isOk) {
            Object.assign(this.request, response.state);
            this.data.replace(response.data);
        }
    }
    render() {
        var buttons: Components.ButtonSpec[] = [
            new Components.ButtonSpec({ text: 'Edit', icon: 'pencil', action: this.edit })            
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

