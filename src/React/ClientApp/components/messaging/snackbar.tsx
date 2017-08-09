import * as React from 'react';
import { Components, Services } from './../../root';
import { observer } from './../../mx';
import * as mdc from 'material-components-web';

@observer
export class Snackbar extends React.Component<any, any>
{
    componentDidMount()
    {
        if (!Services.MessengerService.snackbar) {
            Services.MessengerService.snackbar = mdc.snackbar.MDCSnackbar.attachTo(document.querySelector('#my-mdc-snackbar'));
            //TODO: first toast never appears so add a throw away
            Services.MessengerService.showToast("Welcome", false);
        }
    }
    render() {
        
        return <div id="my-mdc-snackbar" className={"mdc-snackbar " + Services.MessengerService.snackbarclass}
            aria-live="assertive"
            aria-atomic="true"
            aria-hidden="false">

            <div className="mdc-snackbar__text">
                
            </div>      
            
            <div className="mdc-snackbar__action-wrapper">
                <button type="button" className="mdc-button mdc-snackbar__action-button"></button>
            </div>
        </div>
    }
}