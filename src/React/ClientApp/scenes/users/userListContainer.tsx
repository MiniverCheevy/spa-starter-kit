import * as React from 'react';
import * as Api from './../../api.generated';
import * as Models from './../../models.generated';
import { UserList } from './userList'

export class UserListProps {
    users: Models.IUserMessage = [];
    request: Models.IUserQueryRequest = {};
    onEdit: void;
    onRefresh: void;
}

export class UserListContainer extends React.Component<any, any>
{
    private that = this;
    constructor(props) {
        super(props);
        console.log('ctor-in');
        this.state = {
            users: [], request: {}
        };

        console.log('ctor-out');
    }
    componentDidMount() {
        console.log('componentWillMount');
        //var response: Models.IUserQueryResponse = await Api.UserList.get(this.state.request);
        var response = Api.UserList.get(this.state.request).then((response) => {
            if (response.isOk) {
                console.log('ajax-data');
                this.setState({ users: response.data });
            }
        });

    }
    public edit(user: Models.IUserMessage) {

    }
    public refresh(request: Models.IUserQueryRequest) {

    }
    render() {
        console.log('render');
        debugger;
        return <UserList
            request={this.state.request}
            users={this.state.users}
            onEdit={this.edit}
            onRefresh={this.refresh}
        />;
    }
    
    componentWillReceiveProps() {
        console.log('componentWillReceiveProps');
       
    }
    shouldComponentUpdate() { console.log('shouldComponentUpdate'); return true; }
    componentWillUpdate() { console.log('componentWillUpdate'); }

    componentDidUpdate() {
        console.log('componentDidUpdate');
        
        
    }
    unmounting() { console.log('unmounting'); }


    componentWillUnmount() { console.log('componentWillUnmount'); }

    

}