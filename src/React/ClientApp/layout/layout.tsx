import * as React from 'react';
import { NavMenu } from './Components/NavMenu';
import { Routes } from './../routes';
import { Router, Route, HistoryBase } from 'react-router';
import { Home } from '../scenes/Home/Home';

export interface LayoutProps {
   
}
export class Layout extends React.Component<LayoutProps, void> {
    public render() {
        return <div className='container-fluid'>
            <div className='row'>
                <div className='row'>
                    <NavMenu />
                </div>
                <div className='row' id="container">
                    {this.props.children}
                </div>
            </div>
        </div>;
    }
}
