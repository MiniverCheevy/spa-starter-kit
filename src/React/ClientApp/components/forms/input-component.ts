import * as React from 'react';
import { Models, Services } from './../../root';
import * as Validation from './../../services/validation';
import { Form } from './form';
import { InputHelper } from './input-helper';


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
    lines = 1;
    isReadOnly = false;
    nolabel = false;
    isValid= true;
    validationMessage: string;
    internalValue;
    name: string;
    labelText: string;
    metadata: Models.UIMetadata;
    form: Form;
    fullWidth = false;
    helper: InputHelper;
    abstract preRender = () => { } ;
    abstract doRender = (): JSX.Element | null => {return null};
    abstract doValidation = () => { };
    
    render() 
    {
        this.preRender();
        return this.doRender() ;
    }

   
}