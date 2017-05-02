import * as React from 'react';
import * as Api from './../../api.generated';
import * as Models from './../../models.generated';
import {UserListProps} from "./userListContainer"



export class UserList extends React.Component<any, UserListProps>
{
    render(){
        var rows=this.props.users.map (function(user:Models.IUserMessage)
                {
                    return(
                        <tr>
                            <td><button onClick={this.props.onEdit(user)}>Edit</button></td>
                            <td>{user.userName}</td>
                            <td>{user.firstName}</td>
                            <td>{user.lastName}</td>
                            <td>{user.roles}</td>
                        </tr>

                    )

                });

        return <table>
                <tr>
                    <th>User Name</th>
                    <th>First Name</th>
                    <th>Last Name</th>
                    <th>Roles</th>
                </tr>
                {rows}
                </table>
    }
}