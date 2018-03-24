import * as React from 'react';
import { Models, Services } from './../../root';
import { observer } from './../../mx';
import * as Validation from './../../services/validation';
import { Form } from './form';
import { InputHelper, InputState } from './input-helper';


export class InputComponentModel {
    name: string;
    label?: string;
    value?: any;
    key?: string;

    readOnly?: boolean = false;
    noLabel?: boolean = false;
    autofocus?= false;
    fullWidth?= false;
    fullHeight?= false;

    lines?= 1;
    items?: Models.ListItem[] = [];

    model?: any;
    form?: Form;

    change?: (key, value, form) => void;

    isValid?: boolean; 
    validationMessage?: string;

    prefix?: string;
    suffix?: string;
}

export abstract class InputComponent extends React.Component<InputComponentModel, InputComponentModel>
{
    helper: InputHelper;
    abstract doRender = (): JSX.Element | null => { return null };

    constructor(props: InputComponentModel) {
        super(props);
        this.createInitialState(props);
    }

    createInitialState=(props: InputComponentModel)=>{
        this.state = { ...props };
    }

    componentWillReceiveProps(props: InputComponentModel) {
        this.copyPropsToState(props);
    }

    copyPropsToState = (props: InputComponentModel) => {
        this.setState({ ...props });
    }

    render() {        
        return this.doRender();
    }


}