import { Component, OnInit } from '@angular/core';
import { Api, Models } from './../root';

@Component({
  selector: 'scratch',
  templateUrl: './scratch.component.html',
  styleUrls: ['./scratch.component.css']
})
export class ScratchComponent implements OnInit {

    metadata = Models.IMemberRowMetadata;
    request: Models.IMemberListRequest = Models.EmptyIMemberListRequest;
    api;
    
    constructor(private memberList: Api.MemberList) {
        console.log('scratch');
        this.api = memberList;
    }

    async ngOnInit() {
        console.log('scratch-init');
        await this.refresh();
    }
    async refresh()
    {
        
    }

}
