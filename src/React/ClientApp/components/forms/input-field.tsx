import * as React from 'react';
import { InputComponent, InputComponentProps } from './input-component'
import { InputSpan } from './input-span';
import { InputText } from './input-text';
import { InputTextArea } from './input-textarea';
import { InputHelper } from './input-helper';

export class InputField extends InputComponent {
    private isLabel = false;
    private isInput = false;
    private isTextArea = false;

    constructor(props) {
        super(props);
        this.helper = new InputHelper(this);
    }
    onChange = (event) => {
        this.helper.handleChange(event);
    }
    doValidation = () => {
        this.helper.doValidation();
    };

    preRender = () => {
        this.helper.parseProps();    
        if (this.isReadOnly)
            this.isLabel = true;
        else if (this.lines != 1)
            this.isTextArea = true;
        else
            this.isInput = true;
    }
    doRender = () => {
        //TODO: when react 16 drops explore the returning of arrays from render
        //wrapping the indivuals components in a div or article jacks with the layout
        return <article>
        { this.isLabel && <InputSpan {...this.props}  ></InputSpan> }
            {this.isInput && <InputText {...this.props}  ></InputText>}
            {this.isTextArea && <InputTextArea {...this.props}  ></InputTextArea>}
        </article> ;
    }


}