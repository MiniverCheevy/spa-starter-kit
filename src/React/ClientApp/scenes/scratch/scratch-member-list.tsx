import * as React from 'react';
import { observer, observable, IObservableArray } from './../../root';
import {withRouter, Models, Api, Components, Services } from './../../root';
import { ScratchNavMenu } from './scratch-navmenu';

@observer
export class ScratchMemberList extends React.Component<any, any>
{
    key = 'memberList';
    request: Models.MemberListRequest = Services.GridService.getRequest(this.key, Models.MemberListRequest.empty());
    @observable data: IObservableArray<Models.MemberRow> = observable([]);
    metadata = Models.MemberRow.metadata();


    public componentDidMount() {
        this.refresh(this.request);
    }

    public edit=(user: Models.MemberRow)=>{
        (history as any).push('/member-detail/' + user.id);
    }

    public refresh = async (request: Models.MemberListRequest) => {
        Services.GridService.setRequest(this.key, request);
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
            <div>
                <ScratchNavMenu />
                <Components.Card title="Members">
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
