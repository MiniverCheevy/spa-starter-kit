import * as React from 'react';
import { Router, Route, HistoryBase } from 'react-router';
import {  Switch } from 'react-router-dom';
import { Layout } from './Layout/layout';
import { Home } from './scenes/Home/Home';
import { UserListContainer } from './scenes/users/userListContainer';



export class Routes extends React.Component<any, void>
{
    render() {
        return <Switch>
            
            <Route path='/user-list' component={UserListContainer} />
            <Route path='/' component={Home} />
        </Switch >;
    }
    // the '/' route must be last, router navs to the first thing it matches
}


// Allow Hot Module Reloading
declare var module: any;
if (module.hot) {
    module.hot.accept();
}