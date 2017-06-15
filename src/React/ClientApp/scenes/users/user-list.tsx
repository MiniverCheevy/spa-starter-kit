import * as React from 'react';
import { Api, Models } from './../../root';
import { UserListProps } from "./user-list-container"

//https://medium.com/missive-app/45-faster-react-functional-components-now-3509a668e69f

const userRows = (props: UserListProps) =>
{
    return props.users.map(
        (user: Models.IUserMessage) => {
            return (
                <tr key={user.id}>
                    <td>{user.userName}</td>
                    <td>{user.firstName}</td>
                    <td>{user.lastName}</td>
                    <td>{user.roles}</td>
                </tr>
            );
        });
}
export const userList = (props: UserListProps) => {
    var rows = userRows(props);
    return <table>
        <thead>
            <tr>
                <th>User Name</th>
                <th>First Name</th>
                <th>Last Name</th>
                <th>Roles</th>
            </tr>
        </thead>
        <tbody>
            {rows}
        </tbody>
    </table>;
};