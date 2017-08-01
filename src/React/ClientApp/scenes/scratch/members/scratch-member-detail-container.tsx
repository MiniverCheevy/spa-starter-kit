import * as React from 'react';
import { ScratchNavMenu } from './../scratch-navmenu';
import { ScratchMemberDetail } from './scratch-member-detail';
import { Models, Services, Api, Components } from './../../../root';
import { observable, observer } from './../../../mx';


export class ScratchMemberDetailContainer extends React.Component<any, any> {
    model: Models.MemberDetail = observable(Models.MemberDetail.empty());    
    metadata = Models.MemberDetail.metadata();
    @observable form = new Components.Form(this.metadata, this.model);

    componentDidMount() {

        var id = Services.FormsService.getValueAfterLastSlash(this.props.location);
        this.refresh(id);
        this.form.setCallback(this.onChange);
    }
    refresh = async (id) => {
        var response = await Api.Member.get({ id: id });
        if (response.isOk) {
            
            this.form.updateModel(response.data);
        }
    }
    onChange = (form: Components.Form) =>
    {
        console.log('Container');
        console.log(form.model);
        Object.assign(this.form, form);
        Object.assign(this.model, form.model);
    }
    render() {
        return this.doRender();
    }
    doRender = () => {

        return <div>
            <ScratchNavMenu />
            <div className="pull-right">
                <div>IsValid={this.form.isValid.toString()}</div>
                <div>IsDirty={this.form.isDirty.toString()}</div>
                <div>Name={this.model.name}</div>
                <div>Title={this.model.title}</div>
            </div>
            <ScratchMemberDetail form={this.form} change={this.onChange}/>
        </div>;
    }
}
