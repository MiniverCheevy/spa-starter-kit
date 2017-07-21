import { Ng , Models} from './../../root';

export abstract class InputComponent
{
    
    
    @Ng.Output() change = new Ng.EventEmitter();    
    @Ng.Output() modelChange = new Ng.EventEmitter();
    @Ng.Input() model: any;
    oldModel;
    @Ng.Input() metadata: Models.UIMetadata;
    oldMetadata: Models.UIMetadata;
    @Ng.Input() readonly = false;
    @Ng.Input() label: string;
    @Ng.Input() nolabel: boolean = false;
    labelText: string;
    internalValue;

    emitting: boolean = false;
    uniqueId: string;
    name: string;
    constructor()
    {
        this.uniqueId = Math.random().toString(36).substr(2, 9);
       
    }
    abstract handleFormat();

    ngOnChanges() {
        if (this.emitting)
            return;
        if (this.metadata)
            this.name = this.metadata.jsName;
        else
            this.name = this.uniqueId;
        if (this.oldMetadata == this.metadata && this.oldModel == this.model)
            return;
        if (this.model != this.oldModel || this.model == undefined) {
            //console.log('ngOnChanges => ' + this.model);
            this.internalValue = this.model;
            this.handleFormat();
            this.model = this.internalValue;
            this.oldModel = this.model;
        }
        if (this.metadata != this.oldMetadata) {
            this.labelText = this.label || this.metadata.displayName;
            this.handleFormat();
        }
        else
        {
            this.labelText = this.label;
        }
       
    }    
}