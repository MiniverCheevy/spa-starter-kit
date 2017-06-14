import * as React from 'react';
import * as  ReactDOM from 'react-dom'
import { Link } from 'react-router';
import { CurrentUserService } from "./../../services/current-user-service";
import { MessengerService } from "./../../services/messenger-service";
import { observer } from 'mobx-react';
import { observable } from 'mobx';

@observer
export class NavMenu extends React.Component<any, void> {
public async componentDidMount() {
    var user = await CurrentUserService.get();
}
    public render() {
        var progress = undefined;
        if (MessengerService.numberOfPendingHttpRequest != 0)
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
                        <span className="mdc-toolbar__title">React</span>
                    </section>
                    <section className="mdc-toolbar__section mdc-toolbar__section--align-end" role="toolbar">
                        <a href="#/Home" className="material-icons mdc-toolbar__icon" aria-label="Home" alt="Home">Home</a>
                        <a href="#/user-list" className="material-icons mdc-toolbar__icon" aria-label="People" alt="Users">Users</a>
                        
                    </section>
                </div>
            </header>

          
        </div>
        );
    }
}
