import * as React from 'react';
import { userList } from './user-list'
import { observer, observable, IObservableArray, Models, Api } from './../../root';

@observer
export class UserListContainer extends React.Component<any, any>
{
    @observable request: Models.IUserQueryRequest = Models.EmptyIUserQueryRequest;
    @observable users: IObservableArray<Models.IUserMessage> = observable([]);

    public componentDidMount()
    {
        this.refresh(this.request);
    }
   
    public edit(user: Models.IUserMessage) {

    }
    public refresh = async (request: Models.IUserQueryRequest) => {
        var response = await Api.UserList.get(request);
        if (response.isOk)
        {
            this.request = response.state;
            this.users.replace(response.data);
        }
    }
    render() {
        console.log('user-list-container render')
        
        return (
            userList({
                request: this.request,
                users: this.users,
                edit: this.edit,
                refresh: this.refresh
            })
        );
    }   
}

