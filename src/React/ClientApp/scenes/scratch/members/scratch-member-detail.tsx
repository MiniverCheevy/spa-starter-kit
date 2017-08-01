import * as React from 'react';
import { ScratchNavMenu } from './../scratch-navmenu';
import { Models, Services, Api, Components } from './../../../root';
import { observable, observer } from './../../../mx';

export class ScratchMemberDetailProps {
    @observable form: Components.Form
    change
}
@observer
export class ScratchMemberDetail extends React.Component<ScratchMemberDetailProps, any> {

    render() {
        return this.doRender();
    }
    doRender = () => {
        return <div>
            <Components.Card title="Member Details" subTitle={'isDirty=' + this.props.form.isDirty + ' | isValid=' + this.props.form.isValid }>
                <div className="input-form-vertical">
                    <div className="row">
                        {this.props.form.getRenderer()}
                        <Components.InputText form={this.props.form} name="name" />
                        <Components.InputText form={this.props.form} name="title" />
                        <Components.InputText form={this.props.form} name="requiredInt" />
                        <Components.InputText form={this.props.form} name="optionalInt" />
                        <Components.InputText form={this.props.form} name="requiredDate" />
                        <Components.InputText form={this.props.form} name="optionalDate" />
                        <Components.InputText form={this.props.form} name="requiredDateTimeOffset" />
                        <Components.InputText form={this.props.form} name="optionalDateTimeOffset" />
                        <Components.InputText form={this.props.form} name="requiredDecimal" />
                        <Components.InputText form={this.props.form} name="optionalDecimal" />

                    </div>
                </div>
            </Components.Card>
            <Components.Card title="Unbound " subTitle="input-form-vertical">
                <div className="input-form-vertical">
                    <div className="row">
                    </div>
                </div>
            </Components.Card>
        </div>;
    }


}