/// <reference path="../../services/current-user-service.ts" />
import * as React from 'react';
import { NavMenu } from './navMenu';

export class Menu extends React.Component<any, void> {
    private currentUserService = 
    public componentDidMount()
    {
        var user = CurrentUserService
    }

    public render() {
        return <div><NavMenu></NavMenu></div>;
    }
}