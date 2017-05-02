import * as React from 'react';
import { Router, Route, HistoryBase } from 'react-router';
import {  Switch } from 'react-router-dom';
import { Layout } from './Layout/layout';
import { Home } from './scenes/Home/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import { UserListContainer } from './scenes/users/userListContainer';



export class Routes extends React.Component<any, void>
{
    render() {
        return <Switch>
            <Route path='/' component={ Home } />
            <Route path='/counter' component={ Counter } />
            <Route path='/fetchdata' component={ FetchData } />
            <Route path='/users' component={ UserListContainer } />
        </Switch >;
    }
}


// Allow Hot Module Reloading
declare var module: any;
if (module.hot) {
    module.hot.accept();