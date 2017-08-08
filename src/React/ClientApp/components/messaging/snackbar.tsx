import * as React from 'react';
import { Components, Services } from './../../root';
import { observer } from './../../mx';

@observer
export class Snackbar extends React.Component<any, any>
{

    render() {

        const messages = Services.MessengerService.toastMessages.map((message) => {
            return <div className={message.className}>{message.message}</div>;
        });
        return <div id="my-mdc-snackbar" className="mdc-snackbar mdc-snackbar--align-start"
            aria-live="assertive"
            aria-atomic="true"
            aria-hidden="true">
            <div className="mdc-snackbar__text">
                {messages}
            </div>            
        </div>
    }
}