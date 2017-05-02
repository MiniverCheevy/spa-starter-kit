import * as React from 'react';
import * as Api from './../../api.generated';
import * as Models from './../../models.generated';
import {UserList } from './userList'

class UserListContainerProps {
    users: Models.IUserMessage = [];
    request: Models.IUserQueryRequest = {};
}

export class UserListProps extends UserListContainerProps
{

    onEditClick:void;
    onRefresh:void;
}

export class UserListContainer extends React.Component<any, UserListContainerProps>
{

    getInitialState() {
        return new UserListContainerProps();
    }

    componentDidMount = async () => {
        var response:Models.IUserQueryResponse = await Api.UserList.get(this.state.request);
        if (response.isOk)
            this.setState({users:response.data, request:response.state});
    }
    render()
    {
        return <UserList 
            request={this.state.request} 
            users={this.state.users} 
            onEditClick={this.edit}
            onRefresh={this.refresh}
            />
    }
    public edit(user:Models.IUserMessage)
    {

    }
    public refresh(request:Models.IUserQueryRequest)
    {

    }

}