import * as React from 'react';
import { ScratchNavMenu } from './../scratch-navmenu';
import { ScratchMemberDetail } from './scratch-member-detail';
import { Models, Services, Api, Components } from './../../../root';
import { observable } from './../../../mx';

export class ScratchMemberDetailContainer extends React.Component<any, any> {
    model: Models.MemberDetail = observable(Models.MemberDetail.empty());
    @observable form: Components.Form;
    metadata = Models.MemberDetail.metadata();


    componentDidMount() {

        var id = Services.FormsService.getValueAfterLastSlash(this.props.location);
        this.refresh(id);
    }
    refresh = async (id) => {
        var response = await Api.Member.get({ id: id });
        if (response.isOk) {
            this.form = new Components.Form(this.metadata, response.data);
            Object.assign(this.model, response.data);

        }
    }
    onChange = () =>
    {

    }
    render() {
        return this.doRender();
    }
    doRender = () => {
        return <div>
            <ScratchNavMenu />
            <div>

            </div>
            <ScratchMemberDetail form={this.form} change={this.onChange}/>
        </div>;
    }


}