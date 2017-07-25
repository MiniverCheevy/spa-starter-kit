import * as React from 'react';
import { userList } from './user-list'
import { observer, observable, IObservableArray,  Models, Api } from './../../root';

@observer
export class UserListContainer extends React.Component<any, any>
{
    request: Models.UserListRequest = observable(Models.UserListRequest.empty());
    users: IObservableArray<Models.UserRow> = observable([]);

    public componentDidMount()
    {
        this.refresh(this.request);
    }
   
    public edit(user: Models.UserRow) {

    }
    public refresh = async (request: Models.UserListRequest) => {
        var response = await Api.UserList.get(request);
        if (response.isOk)
        {
            console.log("response is ok");
            console.log("total records =" + response.state.totalRecords);
            Object.assign(this.request , response.state);
            //this.request = observable(response.state);
            this.users.replace(response.data);
        }
    }
    render() {
        console.log("render total records =" + this.request.totalRecords);

        

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

