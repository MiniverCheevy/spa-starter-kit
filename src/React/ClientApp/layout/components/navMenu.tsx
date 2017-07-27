import * as React from 'react';
import * as  ReactDOM from 'react-dom'
import { Link } from 'react-router-dom';
import { Router } from 'react-router';
import { Components, Services, observer, observable } from './../../root';

@observer
export class NavMenu extends React.Component<any, void> {
    public async componentDidMount() {
        var user = await Services.CurrentUserService.get();
    }
    public render() {
        var progress = undefined;
        if (Services.MessengerService.numberOfPendingHttpRequest != 0)
            progress = <div className="progress">
                <div className="indeterminate"></div>
            </div>;

        return (<div>
            <div className="progress-container">
                {progress}
            </div>
            <header className="mdc-toolbar mdc-toolbar--fixed mdc-toolbar--waterfall">
                <div className="mdc-toolbar__row">
                    <section className="mdc-toolbar__section mdc-toolbar__section--align-start">
                        <a href="/" className="mdc-toolbar__title">React</a>
                    </section>
                    <section className="mdc-toolbar__section mdc-toolbar__section--align-end" role="toolbar">
                        <a className="toolbar-button material-icons mdc-toolbar__icon" title="Home"
                            href="/#/home"><i className='mdi mdi-home'></i></a>
                        <a className="toolbar-button material-icons mdc-toolbar__icon" title="Users"
                            href="/#/user-list"><i className='mdi mdi-account-multiple'></i></a>
                    </section>
                </div>
            </header>
        </div>
        );
    }
}
