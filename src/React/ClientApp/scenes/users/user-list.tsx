//https://medium.com/missive-app/45-faster-react-functional-components-now-3509a668e69f
import * as React from 'react';
import { Models, Components } from './../../root';

export const userList = (props: UserListProps) => {

    var header = getHeader();
    var rows = getRows(props);
    return <div>{Components.DataTable(header, rows, props.request, props.refresh)}  </div>
};
const getHeader = () => {
    return [{ text: '' },
    { text: 'User Name', sortMember: 'UserName' },
    { text: 'First Name', sortMember: 'FirstName' },
    { text: 'Last Name', sortMember: 'LastName' },
    { text: 'Roles' }];
}
const getRows = (props: UserListProps) => {
    return props.users.map(
        (user: Models.IUserMessage) => {
            return (
                <tr key={user.id}>
                    <td></td>
                    <td>{user.userName}</td>
                    <td>{user.firstName}</td>
                    <td>{user.lastName}</td>
                    <td>{user.roles}</td>
                </tr>
            );
        });
}
class UserListProps {
    users: Models.IUserMessage[] = [];
    request: Models.IUserQueryRequest = {};
    edit(user: Models.IUserMessage): void { };
    refresh(request: Models.IUserQueryRequest): void { };
}
