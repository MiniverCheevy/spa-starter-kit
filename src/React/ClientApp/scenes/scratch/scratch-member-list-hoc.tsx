import * as React from 'react';
import { Models, Api, Components, Services } from './../../root';
import { observer, observable, IObservableArray } from './../../mx';
import { ScratchNavMenu } from './scratch-navmenu';

export default function withData<Props, State, CompState>(
    WrappedComponent: typeof Components.Grid, key: string, api, metadata, request, buttons: Components.ButtonSpec[]
): React.ComponentClass<Props & State> {

    return class extends React.Component<Props & State, CompState> {
        key = key;
        request = Services.GridService.getRequest(key, request);
        data: IObservableArray<any> = observable([]);
        metadata = metadata;
        api = api;

        public componentDidMount() {
            this.refresh(request);
        }
        public refresh = async (request) => {
            Services.GridService.setRequest(this.key, request);
            var response = await this.api.get(request);
            if (response.isOk) {
                Object.assign(this.request, response.state);
                this.data.replace(response.data);
            }
        }
        public render() {
            return <WrappedComponent
                data={this.data.slice()}
                refresh={this.refresh}
                metadata={this.metadata}
                request={this.request}
                buttons={buttons}/>;
        }
    };
}




@observer
export class ScratchMemberList extends React.Component<any, any>
{
    key = 'memberList';
    request: Models.MemberListRequest;// = Services.GridService.getRequest(this.key, Models.MemberListRequest.empty());
    //@observable data: IObservableArray<Models.MemberRow> = observable([]);
    metadata = Models.MemberRow.metadata();




    public edit = (user: Models.MemberRow) => {
        this.props.history.push('/scratch-member-detail/' + user.id);
    }
    //public componentDidMount() {
    //    this.refresh(this.request);
    //}
    //public refresh = async (request: Models.MemberListRequest) => {
    //    Services.GridService.setRequest(this.key, request);
    //    var response = await Api.MemberList.get(request);
    //    if (response.isOk) {
    //        Object.assign(this.request, response.state);
    //        this.data.replace(response.data);
    //    }
    //}

    render() {
        //<Components.Grid
        //    data={this.data.slice()}
        //    refresh={this.refresh}
        //    metadata={this.metadata}
        //    buttons={buttons}
        //    request={this.request}
        //></Components.Grid>


        var buttons: Components.ButtonSpec[] = [
            new Components.ButtonSpec('Edit', 'pencil', this.edit)
        ];

        return (
            <div>
                <ScratchNavMenu />
                <Components.Card title="Members">
                    {withData(
                        Components.Grid,
                        this.key,
                        Api.MemberList,
                        this.metadata,
                        this.request,
                        buttons)
                    }
                </Components.Card>
            </div>);
    }
}
