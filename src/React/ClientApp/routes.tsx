import * as React from 'react';
import { Router, Route, HistoryBase } from 'react-router';
import { Layout } from 'ClientApp/Layout/layout';
import { Home } from 'ClientApp/scenes/Home/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';

export default <Route component={ Layout }>
    <Route path='/' components={{ body: Home }} />
    <Route path='/counter' components={{ body: Counter }} />
    <Route path='/fetchdata' components={{ body: FetchData }} />
</Route>;

// Allow Hot Module Reloading
declare var module: any;
if (module.hot) {
    module.hot.accept();
}
