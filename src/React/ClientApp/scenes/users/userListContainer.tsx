import * as React from 'react';
import * as Api from './../../api.generated';
import * as Models from './../../models.generated';
import { userList } from './userList'

export class UserListProps {
    users: Models.IUserMessage[] = [];
    request: Models.IUserQueryRequest = {};
    onEdit(user: Models.IUserMessage): void{};
    onRefresh(request: Models.IUserQueryRequest): void{ };
}

export class UserListContainer extends React.Component<any, any>
{
    constructor(props) {
        super(props);
        this.state = {
            users: [], request: {}
        };
    }
    setStateAsync(response) {
        return new Promise((resolve) => {
            if (response.isOk) {
                this.setState({ users: response.data, request: response.state }, resolve);
            }
        });
    }
    async componentDidMount() {
        var response = await Api.UserList.get(this.state.request);
        await this.setStateAsync(response);
        

    }
    public edit(user: Models.IUserMessage) {

    }
    public refresh(request: Models.IUserQueryRequest) {

    }
    render() {
        return (
            userList({
                request: this.state.request,
                users: this.state.users,
                onEdit: this.edit,
                onRefresh: this.refresh
            })
        );
    }   
}