import { Component, OnInit } from '@angular/core';
import { Api, Models } from './../root';

@Component({
  selector: 'scratch',
  templateUrl: './scratch.component.html',
  styleUrls: ['./scratch.component.css']
})
export class ScratchComponent implements OnInit {

    request: Models.IMemberListRequest;
    constructor(private memberList: Api.MemberList) {
        
    }

    async ngOnInit() {
        var response = await this.memberList.get(this.request);


  }

}
