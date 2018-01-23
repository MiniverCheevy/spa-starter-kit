import * as React from 'react';
import { ScratchNavMenu } from './scratch-navmenu';
import { Models, Services, Api, Components } from './../../root';


export class ScratchUnboundControls extends React.Component<any,any> {

    render() {
        return this.doRender();
    }
    doRender = () => {
        return <div>
            <ScratchNavMenu />
            <Components.Card title="Unbound " subTitle="input-form-horizontal">
                <div className="input-form-horizontal">
                    <div className="row">
                        <Components.InputSpan name="unbound1" label="Unbound Label1" value="Unbound Value1" />
                        <Components.InputSpan name="unbound2" label="Unbound Label2" value="Unbound Value2" />
                        <Components.InputSpan name="unbound3" label="Unbound Label3" value="Unbound Value3" />
                        <Components.InputSpan name="unbound4" label="Unbound Label4" value="Unbound Value4" />
                        <Components.InputSpan name="unbound5" label="Unbound Label5" value="Unbound Value5" />
                        <Components.InputSpan name="unbound6" label="Unbound Label6" value="Unbound Value6" />
                        <Components.InputText name="unbound7" label="Unbound Label7" value="Unbound Value7" />
                        <Components.InputText name="noLabel" label="noLabel" value="noLabel" noLabel={true} />
                        <Components.InputTextArea name="lines=4" label="lines=4 fullWidth" value="lines=4 fullWidth" fullWidth={true} lines={4} />
                    </div>
                </div>
            </Components.Card>
            <Components.Card title="Unbound " subTitle="input-form-vertical">
                <div className="input-form-vertical">
                    <div className="row">
                        <Components.InputSpan name="unbound1" label="Unbound Label1" value="Unbound Value1" />
                        <Components.InputSpan name="unbound2" label="Unbound Label2" value="Unbound Value2" />
                        <Components.InputSpan name="unbound3" label="Unbound Label3" value="Unbound Value3" />
                        <Components.InputSpan name="unbound4" label="Unbound Label4" value="Unbound Value4" />
                        <Components.InputSpan name="unbound5" label="Unbound Label5" value="Unbound Value5" />
                        <Components.InputSpan name="unbound6" label="Unbound Label6" value="Unbound Value6" />
                        <Components.InputText name="unbound7" label="Unbound Label7" value="Unbound Value7" />
                        <Components.InputText name="noLabel" label="noLabel" value="noLabel" noLabel={true} />
                        <Components.InputTextArea name="lines=4" label="lines=4 fullWidth" value="lines=4 fullWidth" fullWidth={true} lines={4} />
                    </div>
                </div>
            </Components.Card>
            <footer>Footer</footer>
        </div>;
    }


}