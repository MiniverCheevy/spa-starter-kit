import * as React from 'react';
import { ScratchNavMenu } from './../scratch-navmenu';
import { ScratchMemberDetail } from './scratch-member-detail';
import { Models, Services, Api } from './../../../root';

export class ScratchMemberDetailContainer extends React.Component<any, any> {
    model: Models.MemberDetail = Models.MemberDetail.empty();

    componentDidMount() {
        var id = this.props.params.id;
        this.refresh(id);
    }
    refresh=async (id:number)=>
    {
        var response = await Api.Member.get({ id: id });
        if (response.isOk)
        {
            Object.assign(this.model, response.data);
        }
    }
    render() {
        return this.doRender();
    }
    doRender = () => {
        return <div>
                   <ScratchNavMenu />
                   <ScratchMemberDetail/>
               </div>;
    }


}