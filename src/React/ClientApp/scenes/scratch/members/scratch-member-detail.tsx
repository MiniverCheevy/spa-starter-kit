import * as React from 'react';
import { ScratchNavMenu } from './../scratch-navmenu';
import { Models, Services, Api, Components } from './../../../root';
import { observable, observer } from './../../../mx';

export class ScratchMemberDetailProps {
    form: Components.Form;
    model: Models.MemberDetail;
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
                        <Components.InputText {...this.props} name="name" />
                        <Components.InputText {...this.props} name="title" />
                        <Components.InputText {...this.props} name="requiredInt" />
                        <Components.InputText {...this.props} name="optionalInt" />
                        <Components.InputText {...this.props} name="requiredDate" />
                        <Components.InputText {...this.props} name="optionalDate" />
                        <Components.InputText {...this.props} name="requiredDateTimeOffset" />
                        <Components.InputText {...this.props} name="optionalDateTimeOffset" />
                        <Components.InputText {...this.props} name="requiredDecimal" />
                        <Components.InputText {...this.props} name="optionalDecimal" />

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