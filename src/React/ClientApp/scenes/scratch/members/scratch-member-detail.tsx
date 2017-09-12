import * as React from 'react';
import { ScratchNavMenu } from './../scratch-navmenu';
import { Models, Services, Api, Components } from './../../../root';
import { observable, observer } from './../../../mx';

export class ScratchMemberDetailProps {
    form: Components.Form;
    model: Models.MemberDetail;
    change;

}

@observer
export class ScratchMemberDetail extends React.Component<ScratchMemberDetailProps, any> {

    renderCount = 0;
    render() {
        return this.doRender();
    }
    doRender = () => {
        this.renderCount++;
        const props = { form: this.props.form, change: this.props.change };

        return <div>
            {"name=" + this.props.model.name}
            <Components.Card title="Member Details" subTitle={'isDirty=' + this.props.form.isDirty + ' | isValid=' + this.props.form.isValid + ' | renderCount=' +this.renderCount}>
                <div className="input-form-vertical">
                    <div className="row">
                        <Components.InputText {...props} value={this.props.model.name} name="name" />
                        <Components.InputText {...props} value={this.props.model.title} name="title" />
                        <Components.InputText {...props} value={this.props.model.requiredInt} name="requiredInt" />
                        <Components.InputText {...props} value={this.props.model.optionalInt} name="optionalInt" />
                        <Components.InputText {...props} value={this.props.model.requiredDate} name="requiredDate" />
                        <Components.InputText {...props} value={this.props.model.optionalDate} name="optionalDate" />
                        <Components.InputText {...props} value={this.props.model.requiredDate} name="requiredDateTimeOffset" />
                        <Components.InputText {...props} value={this.props.model.optionalDateTimeOffset} name="optionalDateTimeOffset" />
                        <Components.InputText {...props} value={this.props.model.requiredDecimal} name="requiredDecimal" />
                        <Components.InputText {...props} value={this.props.model.optionalDecimal} name="optionalDecimal" />

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