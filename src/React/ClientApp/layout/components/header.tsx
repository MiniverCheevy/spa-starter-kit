/// <reference path="../../services/current-user-service.ts" />
import * as React from 'react';
import { NavMenu } from './navMenu';
import { CurrentUserService } from "ClientApp/services/current-user-service";

export class Menu extends React.Component<any, void> {
    
    public async componentDidMount()
    {
        var user = await CurrentUserService.get();
    }

    public render() {
        return <div><NavMenu></NavMenu></div>;
    }
}