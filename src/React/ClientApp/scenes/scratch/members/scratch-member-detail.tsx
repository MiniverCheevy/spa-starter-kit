import * as React from 'react';
import { ScratchNavMenu } from './../scratch-navmenu';
import { Models, Services, Api, Components } from './../../../root';
import { observable } from './../../../mx';

export class ScratchMemberDetail extends React.Component<any, any> {

    render() {
        return this.doRender();
    }
    doRender = () => {
        return <div>
            <Components.Card title="Unbound" subTitle="Values are hard coded into the markup">
                <div className="row">
                    <Components.InputSpan name="unbound1" label="Unbound Label" value="Unbound Value" />
                </div>
            </Components.Card>
        </div>;
    }


}