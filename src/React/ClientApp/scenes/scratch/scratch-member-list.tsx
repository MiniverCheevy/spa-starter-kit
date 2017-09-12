import * as React from 'react';
import {  Models, Api, Components, Services } from './../../root';
import { observer, observable, IObservableArray } from './../../mx';
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

    public edit = (user: Models.MemberRow) => {
        this.props.history.push('/scratch-member-detail/' + user.id);        
    }

    public refresh = async (request: Models.MemberListRequest) => {
        Services.GridService.setRequest(this.key, request);
        var response = await Api.MemberList.get(request);
        if (response.isOk) {
            Object.assign(this.request, response.state);
            this.data.replace(response.data);
        }
    }
    report = () =>
    {
        debugger;
        Api.MemberReport.getReport(this.request);
    }
    render() {
       
        var buttons: Components.ButtonSpec[] = [
            new Components.ButtonSpec('Edit', 'pencil', this.edit)
        ];
        var data = this.data.slice();
        return (<div>
                <ScratchNavMenu />
                <Components.Card title="Members">
                <div className="pull-right">
                    <Components.PushButton click={() => { this.report(); }} icon="file-pdf-box" text="Report" theme="icon"/>
                </div>
                    <Components.Grid
                        data={data}
                        refresh={this.refresh}
                        metadata={this.metadata}
                        buttons={buttons}
                        request={this.request}
                    ></Components.Grid>
                </Components.Card>
            </div>);
    }
}
