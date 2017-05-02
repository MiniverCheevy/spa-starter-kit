import './css/site.css';

import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { browserHistory, Router } from 'react-router';
import routes from './routes';


//const initialState = (window as any).initialReduxState as ApplicationState;
//const store = configureStore(initialState);
//const history = syncHistoryWithStore(browserHistory, store);

ReactDOM.render(
    <Router history={browserHistory} children={routes} />,
    document.getElementById('react-app')
);
