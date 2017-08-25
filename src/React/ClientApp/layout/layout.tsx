import * as React from 'react';
import { NavMenu } from './Components/NavMenu';
import { Routes } from './../routes';
import { Router, Route, HistoryBase } from 'react-router';
import { Home } from '../scenes/Home/Home';
import { Dialog } from './../components/messaging/dialog';
import { Snackbar } from './../components/messaging/snackbar';

export interface LayoutProps {

}
export class Layout extends React.Component<LayoutProps, any> {
    public render() {
        return <div>
                    <NavMenu />            
                    <div id="container" className="mdc-toolbar-fixed-adjust">
                        {this.props.children}
                    </div>
                    <Dialog />
                    <Snackbar/>
                </div>;
    }
}
