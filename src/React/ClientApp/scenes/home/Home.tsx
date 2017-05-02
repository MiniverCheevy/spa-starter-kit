import * as React from 'react';
import 'isomorphic-fetch';
import {CurrentUserService} from "ClientApp/services/current-user-service";

export class Home extends React.Component<any, void> {
    
    
    public async componentDidMount()
    {
        
        var userResponse = await CurrentUserService.get();
        
        console.log("user.isAuthenticated=" + userResponse.isAuthenticated);
        console.log("user.userName=" + userResponse.userName);



    }
    public render() {
        return <div>
            <h1>Hello, world!</h1>            
        </div>;
    }
}
