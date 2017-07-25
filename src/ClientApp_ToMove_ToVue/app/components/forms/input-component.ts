import { Models , Services} from './../../root';
import * as Validation from './../../../services/validation';
import { Output, Input, EventEmitter } from '@angular/core';

export abstract class InputComponent {

    @Output() change = new EventEmitter();
    @Output() modelChange = new EventEmitter();
    @Input() model: any;
    oldModel;
    @Input() metadata: Models.UIMetadata;
    oldMetadata: Models.UIMetadata;
    @Input() readonly = false;
    @Input() label: string;
    @Input() nolabel: boolean = false;
    labelText: string;
    internalValue;
    isAdded: boolean = false;
    isValid: boolean = true;
    valiationMessage: string = '';
    emitting: boolean = false;
    uniqueId: string;
    name: string;

    constructor() {
        this.uniqueId = Math.random().toString(36).substr(2, 9);

    }

    abstract handleFormat = () => { };
    requestFormat = () => {
        if (!this.isAdded && this.metadata != null) {
            this.metadata.control = this;
            this.isAdded=true;
        }
        this.handleFormat();
    }
    doValidation()
    {
        if (this.metadata)
        {
            var result = Services.ValidationServiceStatic.validate({ metadata: this.metadata, value: this.internalValue });
            this.showValidationIfNeeded(result);
        }
    }
    showValidationIfNeeded(validation: Validation.ValidationResponse) {        
        this.isValid = validation.isValid;
        this.valiationMessage = validation.message;
    }
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
            this.requestFormat();
            this.model = this.internalValue;
            this.oldModel = this.model;
        }
        if (this.metadata != this.oldMetadata) {
            this.labelText = this.label || this.metadata.displayName;
            this.requestFormat();
        }
        else {
            this.labelText = this.label;
        }

    }
}