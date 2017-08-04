import * as React from 'react';
import { ScratchNavMenu } from './../scratch-navmenu';
import { ScratchMemberDetail } from './scratch-member-detail';
import { Models, Services, Api, Components } from './../../../root';
import { observer, observable, action } from './../../../mx';

@observer
export class ScratchMemberDetailContainer extends React.Component<any, any> {
    @observable model: Models.MemberDetail = observable(Models.MemberDetail.empty());
    @observable metadata = observable(Models.MemberDetail.metadata());
    form = new Components.Form(this.metadata);


    componentDidMount() {
        var id = Services.FormsService.getValueAfterLastSlash(this.props.location);
        this.refresh(id);
    }
    
    refresh = async (id) => {
        var response = await Api.Member.get({ id: id });
        if (response.isOk) {
            Object.assign(this.model, response.data);
        }
    }    
    
    onChange = (key, value,form) => {
        console.log('Container=>' + key + '=' + value);
        Object.assign(this.form, form);
        this.model[key] = value;
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
            <ScratchMemberDetail form={this.form} model={this.model} change={this.onChange} />
        </div>;
    }
}
