import * as React from 'react';

export class EmptyNavMenu extends React.Component<any, void> {
    public render() {
        return <div className='main-nav'>
            <div className='navbar navbar-inverse'>
                <div className='navbar-header'>
                    <button type='button' className='navbar-toggle' data-toggle='collapse' data-target='.navbar-collapse'>
                        <span className='sr-only'>Toggle navigation</span>
                        <span className='icon-bar'></span>
                        <span className='icon-bar'></span>
                        <span className='icon-bar'></span>
                    </button>
                    <Link className='navbar-brand' to={'/'}>react</Link>
                </div>
                <div className='clearfix'></div>
                <div className='navbar-collapse collapse'>                   
                </div>
            </div>
        </div>;
    }
}
