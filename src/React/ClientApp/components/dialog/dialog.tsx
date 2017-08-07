import * as React from 'react';
import { Components, Services } from './../../root';

export class Dialog extends React.Component<any, any>
{

    render() {
        var text = Services.MessengerService.confirmPrompt || "Are you sure?";

        return <aside id="my-mdc-dialog" className="mdc-dialog" role="alertdialog"
            aria-labelledby="my-mdc-dialog-label" aria-describedby="my-mdc-dialog-description">
            <div className="mdc-dialog__surface">
                <header className="mdc-dialog__header">
                    <h2 id="my-mdc-dialog-label" className="mdc-dialog__header__title">
                        Confirm
                     </h2>
                </header>
                <section id="my-mdc-dialog-description" className="mdc-dialog__body">
                    {text}
                </section>
                <footer className="mdc-dialog__footer">
                    <button type="button" className="mdc-button mdc-dialog__footer__button mdc-dialog__footer__button--cancel">Cancel</button>
                    <button type="button" className="mdc-button mdc-dialog__footer__button mdc-dialog__footer__button--accept">Ok</button>
                </footer>
            </div >
            <div className="mdc-dialog__backdrop"></div>
        </aside >
    }
}