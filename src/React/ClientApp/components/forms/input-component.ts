import * as React from 'react';
import { Models, Services } from './../../root';
import * as Validation from './../../services/validation';
import { Form } from './form';

export class InputComponentProps {
    name: string;
    form?: Form;
    readOnly?: boolean = false;
    label?: string;
    noLabel?: boolean = false;
    value?;
    autofocus? = false;
    fullWidth? = false;
    lines? = 1;
    items?: Models.ListItem[] = [];
}

export abstract class InputComponent extends React.Component<InputComponentProps, any>
{
    nolabel = false;
    isValid= true;
    validationMessage: string;
    internalValue;
    name: string;
    labelText: string;
    metadata: Models.UIMetadata;
    form: Form;
    fullWidth = false;

    abstract preRender = () => { };
    abstract doRender = (): JSX.Element | null => { return null };
    

    render()
    {
        
        this.doPreRender();
        this.doRender() ;
    }

    doPreRender = () => {
        this.preRender.bind(this);
        this.doRender.bind(this);
        this.name = this.props.name;
        if (this.props.noLabel != null)
            this.nolabel = this.props.noLabel;
        if (this.props.fullWidth != null)
            this.fullWidth = this.props.fullWidth;
        if (this.props.label != null)
            this.labelText = this.props.label;
        if (this.props.value != null)
            this.internalValue = this.props.value;        

        if (this.form != null)
        {
            this.metadata = this.form.getMetadata(this.name);
            this.internalValue = this.props.value || this.form.getValue(this.name);
            
            if (this.metadata != null)
                this.labelText = this.props.label || this.metadata.displayName;
        }
        this.preRender();
    }
    doValidation=()=> {
        if (this.form && this.metadata) {
            var result = Services.ValidationService.validate({ metadata: this.metadata, value: this.internalValue });
            this.showValidationIfNeeded(result);
        }
    }
    showValidationIfNeeded=(validation: Validation.ValidationResponse) =>{
        this.isValid = validation.isValid;
        this.validationMessage = validation.message;
    }
}