import * as React from 'react';
import * as  ReactDOM from 'react-dom'
import { Link } from 'react-router';
import { Nav, Navbar, NavItem, NavDropdown, MenuItem } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';
import { CurrentUserService } from "./../../services/current-user-service";
import { MessengerService } from "./../../services/messenger-service";
import { observer } from 'mobx-react';

@observer
export class NavMenu extends React.Component<any, void> {
public async componentDidMount() {
    var user = await CurrentUserService.get();
}
    public render() {
        var progress = undefined;
        if (MessengerService.numberOfPendingHttpRequest != 0)
            progress = <div className="progress">abc
                <div className="indeterminate"></div>
            </div>;
       
        return (<div>
            <div className="progress-container">
                {progress}
            </div>
            <Navbar bsStyle="inverse">
                <Navbar.Header>
                    <Navbar.Brand>
                        <a href="#">React</a>
                    </Navbar.Brand>
                </Navbar.Header>
                <Nav>
                    <LinkContainer to="/home">
                        <NavItem eventKey={1}> <span className='mdi mdi-home'></span> Home!!!</NavItem>
                    </LinkContainer>
                    <LinkContainer to="/counter">
                        <NavItem eventKey={2}>  <span className='mdi mdi-school'></span> Counter</NavItem>
                    </LinkContainer>
                    <LinkContainer to="/fetchdata">
                        <NavItem eventKey={3}> <span className='mdi mdi-format-list-bulleted'></span> Fetch Data</NavItem>
                    </LinkContainer>

                    <NavDropdown eventKey={4} title="Admin" id="basic-nav-dropdown">
                        <LinkContainer to="/user-list">
                            <NavItem eventKey={4.1}>Users</NavItem>
                        </LinkContainer>
                    </NavDropdown>
                </Nav>
            </Navbar>
        </div>
        );
    }
}
