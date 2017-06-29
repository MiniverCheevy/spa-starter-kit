import { Component, OnInit } from '@angular/core';
import { Api, Models } from './../root';

@Component({
  selector: 'scratch',
  templateUrl: './scratch.component.html',
  styleUrls: ['./scratch.component.css']
})
export class ScratchComponent implements OnInit {

    request: Models.IMemberListRequest;
    data: Models.IMemberRow[] = [];
    constructor(private memberList: Api.MemberList) {
        console.log('scratch');
        
    }

    async ngOnInit() {
        console.log('scratch-init');
        await this.refresh();
    }
    async refresh()
    {
        var response = await this.memberList.get(this.request);
        if (response.isOk)
            this.data = response.data;
    }

}
