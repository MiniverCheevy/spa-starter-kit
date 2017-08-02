import * as React from 'react';
import { Models, Services } from './../../root';
import { observer } from './../../mx';
import * as Validation from './../../services/validation';
import { Form } from './form';
import { InputHelper } from './input-helper';


export class InputComponentProps {
    name: string;
    label?: string;
    value?;

    readOnly?: boolean = false;
    noLabel?: boolean = false;
    autofocus?= false;
    fullWidth?= false;

    lines?= 1;

    items?: Models.ListItem[] = [];

    model?: any;
    form?: Form;

    change?:(key,value)=>void;
}

export abstract class InputComponent extends React.Component<InputComponentProps, any>
{
    helper: InputHelper;
    abstract doRender = (): JSX.Element | null => { return null };

    render() {
        return this.doRender();
    }


}