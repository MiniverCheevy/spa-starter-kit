import * as React from 'react';
import { userList } from './user-list'
import { observer, observable, IObservableArray, Models, Api } from './../../root';


export class UserListProps {
    users: Models.IUserMessage[] = [];
    request: Models.IUserQueryRequest = {};
    onEdit(user: Models.IUserMessage): void{};
    onRefresh(request: Models.IUserQueryRequest): void{ };
}

@observer
export class UserListContainer extends React.Component<any, any>
{
    @observable request: Models.IUserQueryRequest = Models.EmptyIUserQueryRequest;
    @observable users: IObservableArray<Models.IUserMessage> = observable([]);
    constructor(props) {
        super(props);
        this.state = {
            users: [], request: {}
        };
        
    }
    public componentDidMount()
    {
        this.refresh(this.request);
    }
   
    public edit(user: Models.IUserMessage) {

    }
    public refresh=async(request: Models.IUserQueryRequest) =>{
        var response = await Api.UserList.get(this.state.request);
        if (response.isOk)
        {
            this.users.replace(response.data);
        }
    }
    render() {
        console.log('user-list-container render')
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