import { Component, OnInit } from '@angular/core';
import { Api, Models, Components } from './../root';

@Component({
  selector: 'scratch',
  templateUrl: './scratch.component.html',
  styleUrls: ['./scratch.component.css']
})
export class ScratchComponent implements OnInit {

    
    metadata = Models.MemberRow.metadata();
    request: Models.MemberListRequest = Models.MemberListRequest.empty();
    data: Models.MemberRow = [];
    buttons: Components.GridButton[] = [];
    constructor(private memberList: Api.MemberList) {
        console.log('scratch');
        this.buttons.push({
            action: this.edit, icon: 'pencil', text: 'Edit'
        })
        this.refresh();
    }

    async ngOnInit() {
        console.log('scratch-init');
        await this.refresh();
    }
    refresh = async ()=>
    {
        console.log('refresh');
        var response = await this.memberList.get(this.request);
        if (response.isOk)
            {
            this.data = response.data;
            this.request = response.state;
        }
    }
    edit = async () =>
    { }

}
