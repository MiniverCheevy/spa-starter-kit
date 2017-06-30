import { Component, OnInit, Output, Input, OnChanges, EventEmitter } from '@angular/core';
import { Models } from './../../root';

@Component({
    selector: 'grid',
    templateUrl: './grid.component.html',
    styleUrls: ['./grid.component.css']
})
export class GridComponent implements OnChanges {

    @Input() metadata;
    @Input() request: Models.IGridState;
    @Input() api;
    @Input() links: ILink[];
    @Output() change = new EventEmitter();
    data: any[];
    columns: Models.IUIMetadata[];
    constructor() { }

    ngOnChanges() {
        console.log('metadata=' + this.metadata);
        console.log('request=' + this.request);
        console.log('api=' + this.api);

        if (this.metadata && this.request && this.api)
            this.setup();
    }
    setup() {
        console.log('setup');
        if (!this.columns) {
            this.columns = [];
            for (var key in this.metadata) {
                if (this.metadata.hasOwnProperty(key)
                    && (!this.metadata[key].isHidden ||
                        this.metadata[key].isHidden == false)
                ) {
                    this.columns.push(this.metadata[key]);
                }
            }
        }
        this.refresh();
    }
    refresh() {
        this.change.emit({});
        if (!this.api || typeof this.api.get != "function")
            console.error('api class is not set or does not have a get function');
        var response = this.api.get(this.request);
        if (response.isOk) {
            this.data = response.data;
            this.request = response.gridState;
        }
    }
    format(value: string, format: string) {
        if (value == null)
            return '';
        try {
            switch (format) {
                case "Text":
                    return value;
                case "Date":
                    var date = new Date(value);
                    return date.toLocaleDateString("en-US");
                case "Time":
                    var date = new Date(value);
                    return date.toTimeString();
                case "DateTime":
                    var date = new Date(value);
                    return date.toLocaleString();
                case "Currency":
                    var number = parseFloat(value);
                    return "$" + number.toFixed(3);
                case "Decimal":
                    var number = parseFloat(value);
                    return number.toFixed(3);
                case "PhoneNumber":
                    if (value.length == 10) {
                        var first = value.substr(0, 3);
                        var second = value.substr(3, 3);
                        var third = value.substr(6, 4);
                        return `(${first}) #{second}-${third}`;
                    }
                    else {
                        return value;
                    }
            }
        }
        catch (e) {
            console.log(e);
            return value;
        }
    }
}
export interface ILink {
    text: string;
    action: (any) => void;
    icon: string;
}



