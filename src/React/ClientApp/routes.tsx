import * as React from 'react';
import { Layout } from './layout/layout';
import { Home } from './scenes/home/home';
import { UserList } from './scenes/users/user-list';
import { Route, Link } from './root';
import { HashRouter} from 'react-router-dom';
import { hashHistory } from 'react-router';

export class Routes extends React.Component<any, void>
{
    render() {
        var style = { margin:'200px'};
        return <HashRouter history={hashHistory}>
            <div>
                <Layout>                    
                    <Route path='/user-list' component={UserList} />
                    <Route path='/home' component={Home} />
                    <Route exact path='/' component={Home} />
                </Layout>
            </div>
          
        </HashRouter >;
    }
    // the '/' route must be last, router navs to the first thing it matches
}

const foo = ()=>
    (
    <Layout>
       
    </Layout>
    )
const About = () => (
    <div>
        <h2>About</h2>
    </div>
)

const Topics = ({ match }) => (
    <div>
        <h2>Topics</h2>
        <ul>
            <li>
                <Link to={`${match.url}/rendering`}>
                    Rendering with React
        </Link>
            </li>
            <li>
                <Link to={`${match.url}/components`}>
                    Components
        </Link>
            </li>
            <li>
                <Link to={`${match.url}/props-v-state`}>
                    Props v. State
        </Link>
            </li>
        </ul>

        <Route path={`${match.url}/:topicId`} component={Topic} />
        <Route exact path={match.url} render={() => (
            <h3>Please select a topic.</h3>
        )} />
    </div>
)

const Topic = ({ match }) => (
    <div>
        <h3>{match.params.topicId}</h3>
    </div>
)


// Allow Hot Module Reloading
declare var module: any;
if (module.hot) {
    module.hot.accept();
}