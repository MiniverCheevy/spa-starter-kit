import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { Routes } from './routes';
import { Provider } from 'mobx-react';
import { Services } from './root'

//import DevTools from 'mobx-react-devtools' <DevTools />
ReactDOM.render((
    <Provider currentUser={Services.CurrentUserService}>
                <Routes />
        </Provider>
), document.getElementById('react-app') as any);


// Allow Hot Module Reloading
declare var module: any;
if (module.hot) {
    module.hot.accept();
}