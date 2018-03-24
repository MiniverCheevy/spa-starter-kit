import * as React from 'react';
import { Layout } from './layout/layout';
import { Home } from './scenes/home/home';
import { UserList } from './scenes/users/user-list';

import { HashRouter, Route, Link, withRouter } from 'react-router-dom';

import {
    ScratchHome, ScratchProjectList, ScratchTeamList,
    ScratchMemberList, ScratchMemberDetailContainer,
    ScratchUnboundControls
} from './scenes/scratch';
//private routes https://tylermcginnis.com/react-router-protected-routes-authentication/
//https://stackoverflow.com/questions/42123261/programmatically-navigate-using-react-router-v4
//const PrivateRoute = ({ component: Component, ...rest }) => (
//    <Route {...rest} render={(props) => (
//        Services.CurrentUserService.user.isAuthenticated 
//            ? <Component {...rest} {...props} />
//            : <Redirect to={{
//                pathname: '/login',
//                state: { from: props.location }
//            }} />
//    )}/>
//);
//const AdminRoute = ({ component: Component, ...rest }) => (
//    <Route {...rest} render={(props) => (
//        Services.CurrentUserService.user.isAdmin 
//            ? <Component {...rest} {...props}/>
//            : <Redirect to={{
//                pathname: '/login',
//                state: { from: props.location }
//            }} />
//    )} />
//);
export class Routes extends React.Component<any,any>
{
    render() {
        var style = { margin: '200px' };
        return <HashRouter>
            <Layout>
                <Route path='/scratch' component={ScratchHome} />
                <Route path='/scratch-member-detail/:id' component={ScratchMemberDetailContainer} />
                <Route path='/scratch-member-list' component={ScratchMemberList} />
                <Route path='/scratch-project-list' component={ScratchProjectList} />
                <Route path='/scratch-team-list' component={ScratchTeamList} />
                <Route path='/scratch-unbound-controls' component={ScratchUnboundControls} />
                <Route path='/user-list' component={UserList} />
                <Route path='/home' component={Home} />
                <Route exact path='/' component={Home} />
            </Layout>
        </HashRouter >;
    }
    // the '/' route must be last, router navs to the first thing it matches

}
//TODO: consider
//https://reacttraining.com/react-router/web/example/animated-transitions

// Allow Hot Module Reloading
declare var module: any;
if (module.hot) {
    module.hot.accept();
}