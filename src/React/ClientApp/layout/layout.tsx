import * as React from 'react';
import { NavMenu } from './Components/NavMenu';
import { Routes } from './../routes';
import { Router, Route, HistoryBase } from 'react-router';
import { Home } from '../scenes/Home/Home';
import { Dialog } from './../components/dialog/dialog';

export interface LayoutProps {

}
export class Layout extends React.Component<LayoutProps, void> {
    public render() {
        return <div>
            <div>
                <NavMenu />
            </div>
            <div id="container" className="mdc-toolbar-fixed-adjust">
                {this.props.children}
            </div>
            <Dialog />
        </div>;
    }
}
