import { Component, AfterContentInit, Input } from '@angular/core';

@Component({
    selector: 'toolbar-icon-link',
    templateUrl: './toolbar-icon-link.component.html',
    styleUrls: ['./toolbar-icon-link.component.css']
})
export class ToolbarIconLinkComponent implements AfterContentInit {

    @Input() route: string;
    @Input() icon: string;
    @Input() title: string;
    url: string;
    iconName: string;
    constructor() {

    }
    ngAfterContentInit() {
        this.url = "#/" + this.route;
        this.iconName = "mdi mdi-" + this.icon;
    }
}

