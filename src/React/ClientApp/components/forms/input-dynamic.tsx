import * as React from 'react';
import { Models,Services } from './../../root';
import { InputComponent, InputComponentModel  } from './input-component';
import { InputHelper } from './input-helper';
import { InputDropdown } from './input-dropdown';
import { InputText } from './input-text';
import { InputCheck } from './input-check'
export class InputDynamic extends React.Component<InputComponentModel, InputComponentModel>
{
    key: string;
    helper: InputHelper;
    isText: boolean = false;
    isCheckBox: boolean = false;
    isDropdown: boolean = false;
    constructor(props: InputComponentModel) {
        super(props);
        if (props.form) {
            const metadata: Models.UIMetadata = props.form.metadata[props.name];
            if (metadata && metadata.bool) {
                this.isCheckBox = true;
                return;
            }
            if (props.items && props.items.length > 0) {
                this.isDropdown = true;
                return;
            }
        }
        this.isText = true;
    }
    render() {
        let result = <InputText {...this.state}></InputText>;
        if (this.isCheckBox)
            result = <InputCheck {...this.state}></InputCheck>;
        else if (this.isDropdown)
            result = <InputDropdown {...this.state}></InputDropdown>;

        return result;
    }
}