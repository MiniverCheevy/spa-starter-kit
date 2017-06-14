import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { HashRouter as Router, Route } from 'react-router-dom';
import { Switch } from 'react-router-dom';
import { Layout } from './Layout/layout';
import { Routes } from './routes';
import { Provider } from 'mobx-react';
import { CurrentUserService } from './services/current-user-service'
//import DevTools from 'mobx-react-devtools' <DevTools />
ReactDOM.render((
    <Provider currentUser={CurrentUserService}>
        <Router>
            <main>
                <Layout>
                    <Routes/>
                </Layout>
                
            </main>
        </Router>
    </Provider>
), document.getElementById('react-app') as any);


// Allow Hot Module Reloading
declare var module: any;
if (module.hot) {
    module.hot.accept();
}