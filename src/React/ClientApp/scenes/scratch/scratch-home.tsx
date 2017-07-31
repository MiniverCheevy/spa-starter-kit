import * as React from 'react';
import { ScratchNavMenu } from './scratch-navmenu';


export class ScratchHome extends React.Component<any, any> {

    render() {
        return this.doRender();
    }
    doRender = () => {
        return <div>            
            <ScratchNavMenu />
            <div><h3>Scratch</h3></div>
        </div>;
    }


}