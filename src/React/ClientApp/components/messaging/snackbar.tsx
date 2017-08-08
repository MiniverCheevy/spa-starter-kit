import * as React from 'react';
import { Components, Services } from './../../root';
import { observer } from './../../mx';

@observer
export class Snackbar extends React.Component<any, any>
{

    render() {
        
        return <div id="my-mdc-snackbar" className={"mdc-snackbar " + Services.MessengerService.snackbarclass}
            aria-live="assertive"
            aria-atomic="true"
            aria-hidden="true">

            <div className="mdc-snackbar__text">
                
            </div>      
            
            <div className="mdc-snackbar__action-wrapper">
                <button type="button" className="mdc-button mdc-snackbar__action-button"></button>
            </div>
        </div>
    }
}