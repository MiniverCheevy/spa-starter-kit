import * as React from 'react';
import { PushButton } from './push-button';
import { ButtonSpec } from './button-spec';
import { Form } from './../forms/form';
import { Services } from './../../root';

export class ButtonBarProps {
    save?: () => void;
    saveAndClose?: () => void;
    saveAndAddAnother?: () => void;
    delete?: () => void;
    deletePrompt?: string;
    buttons?: ButtonSpec[] = [];
    form?: Form;

}
export class ButtonBar extends React.Component<ButtonBarProps, any> {

    private goBack = () => {

        if (this.props.form && this.props.form.isDirty) {
            Services.MessengerService.confirm("Are you sure you want to discard your changes?", () => { window.history.back(); });
        }
        else {
            window.history.back();
        }
    }
    private doSave = (action) => {
        if (this.props.form && !this.props.form.isValid) {
            Services.MessengerService.showToast("Please correct validation errors.", true);
            return;
        }
        action();
    }
    private doDelete(action) {
        var text = this.props.deletePrompt || "Are you sure?";
        Services.MessengerService.confirm(text, action);
    }
    public render() {

        let buttons = (this.props.buttons || []).map((button) => {
            return <PushButton theme="grid-icon" key={button.key}
                text={button.text} icon={button.icon} click={button.action}
            ></PushButton>;
        });

        return <div className="button-bar">

            <div className="pull-left">
                {this.props.save != null &&
                    <PushButton theme="primary" click={() => { this.doSave(this.props.save) }} icon="content-save" text="Save" />}
                {this.props.saveAndClose != null &&
                    <PushButton theme="primary" click={() => { this.doSave(this.props.saveAndClose) }} icon="content-save" text="Save And Close" />}
                {this.props.saveAndAddAnother != null &&
                    <PushButton theme="primary" click={() => { this.doSave(this.props.saveAndAddAnother) }} icon="content-save" text="Save And Add Another" />}
                <PushButton click={this.goBack} compact={true} theme="info" icon="keyboard-backspace" text="Back" />
                {buttons}
            </div>
            <div className="pull-right" >
                {this.props.delete != null &&
                    <PushButton theme="danger" click={() => { this.doDelete(this.props.delete) }} icon="delete" text="Delete" />}
            </div>
        </div>;

    }
}