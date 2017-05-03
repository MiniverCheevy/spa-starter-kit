import './css/site.css';

import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { HashRouter as Router, Route } from 'react-router-dom';
import { Switch } from 'react-router-dom';
import { Container } from 'cerebral/react'
import controller from './controller'
import { Layout } from './Layout/layout';
import { Routes } from './routes';

//<Container controller={controller}>
//</Container>
ReactDOM.render((

    
        <Router>
            <main>
                <Layout>
                    <Routes/>
                </Layout>
            </main>
        </Router>
    
), document.getElementById('react-app') as any);


// Allow Hot Module Reloading
declare var module: any;
if (module.hot) {
    module.hot.accept();
}