import { Models, Ng, Services } from './../../root';
import { GridButton } from './grid-link';
@Ng.Component({
    selector: 'grid',
    templateUrl: './grid.component.html',
    styleUrls: ['./grid.component.css']
})
export class GridComponent implements Ng.OnChanges {

    @Ng.Input() data: any[];
    @Ng.Input() metadata;
    @Ng.Input() request: Models.IGridState;
    @Ng.Input() buttons: GridButton[];
    @Ng.Output() change = new Ng.EventEmitter();
    columns: Models.UIMetadata[];
    constructor(private formatter: Services.FormatService) { }

    ngOnChanges() {
        if (this.metadata && this.request)
            this.setup();
    }
    setup() {
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
    }
    executeAction = (action, row) =>
    {
        if (action && typeof action == "function")
            action(row);
    }
    refresh = async () => {
        this.change.emit({});
    }

    format(value, metadata) {
        return this.formatter.format(value, metadata);
    }

}




