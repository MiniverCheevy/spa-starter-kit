import './css/site.css';

import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { BrowserRouter as Router, Route } from 'react-router-dom';
import { Switch } from 'react-router-dom';
import { Container } from 'cerebral/react'
import controller from './controller'
import { Layout } from './Layout/layout';
import { Home } from './scenes/Home/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import { UserListContainer } from './scenes/users/userListContainer';



ReactDOM.render((
    <Container controller={controller}>
        <Router>
            <Layout>
            </Layout>
        </Router>
    </Container>
), document.getElementById('react-app') as any);


// Allow Hot Module Reloading
declare var module: any;
if (module.hot) {
    module.hot.accept();
}