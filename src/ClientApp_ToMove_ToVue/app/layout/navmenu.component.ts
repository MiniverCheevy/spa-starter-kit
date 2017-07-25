import { Component, OnInit } from '@angular/core';
import { Services } from './../root';

@Component({
    selector: 'navmenu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})
export class NavmenuComponent implements OnInit {

    constructor(private messengerService: Services.MessengerService, private currentUserService: Services.CurrentUserService)
{
        

    }

    async ngOnInit() {
        await this.currentUserService.get();
    }
}

