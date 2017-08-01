import * as React from 'react';
import { Layout } from './layout/layout';
import { Home } from './scenes/home/home';
import { UserList } from './scenes/users/user-list';

import { HashRouter, Route, Link, withRouter } from 'react-router-dom';
import { hashHistory } from 'react-router';
import { ScratchHome, ScratchProjectList, ScratchTeamList, ScratchMemberList, ScratchMemberDetailContainer } from './scenes/scratch';
export class Routes extends React.Component<any, void>
{
    render() {
        var style = { margin: '200px' };
        return <HashRouter>
            <Layout>
                <Route path='/scratch' component={ScratchHome} />
                <Route path='/scratch-member-detail:id' component={ScratchMemberDetailContainer} />
                <Route path='/scratch-member-list' component={ScratchMemberList} />
                <Route path='/scratch-project-list' component={ScratchProjectList} />
                <Route path='/scratch-team-list' component={ScratchTeamList} />



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