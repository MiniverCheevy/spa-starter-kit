import * as React from 'react';

export class ScratchNavMenu extends React.Component<any, any> {

    render() {
        return this.doRender();
    }
    doRender = () => {
        return <div>
            <header className="mdc-toolbar mdc-toolbar--fixed mdc-toolbar--waterfall">
                <div className="mdc-toolbar__row">
                    <section className="mdc-toolbar__section mdc-toolbar__section--align-start">
                        <a href="/" className="mdc-toolbar__title">React</a>
                    </section>
                    <section className="mdc-toolbar__section mdc-toolbar__section--align-end" role="toolbar">
                        <a className="toolbar-button material-icons mdc-toolbar__icon" title="Home"
                            href="/#/home"><i className='mdi mdi-home'></i></a>                       
                        <a className="toolbar-button material-icons mdc-toolbar__icon" title="Members"
                            href="/#/scratch-member-list"><i className='mdi mdi-account-switch'></i></a>
                        <a className="toolbar-button material-icons mdc-toolbar__icon" title="Teams"
                           href="/#/scratch-team-list"><i className='mdi mdi-group'></i></a>
                        <a className="toolbar-button material-icons mdc-toolbar__icon" title="Projects"
                            href="/#/scratch-project-list"><i className='mdi mdi-apps'></i></a>
                        <a className="toolbar-button material-icons mdc-toolbar__icon" title="Unbound Controls"
                            href="/#/scratch-unbound-controls"><i className='mdi mdi-widgets'></i></a>
                    </section>
                </div>
            </header>
        </div>;
    }
}