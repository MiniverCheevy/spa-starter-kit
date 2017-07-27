import * as React from 'react';
import { NavMenu } from './navMenu';

export class Header extends React.Component<any, void> {    
    public render() {
        return <div>            
            <NavMenu></NavMenu>
        </div>;
    }
}