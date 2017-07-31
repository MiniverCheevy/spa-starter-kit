import { InputComponent, InputComponentProps } from './input-component'
import { InputSpan } from './input-span';
import { InputText } from './input-text';
import { InputTextArea } from './input-textarea';

export class InputField extends InputComponent {
    private isLabel = false;
    private isInput = false;
    private isTextArea = false;
    preRender = () => {
        if (this.props.readOnly)
            this.isLabel = true;
        else if (this.props.lines != 1)
            this.isTextArea = true;
        else
            this.isInput = true;
    }
    doRender = () => {
        return <div>
            {this.isLabel && <InputSpan {... this.props}  ></InputSpan>}
            {this.isInput && <InputText {... this.props}  ></InputText>}
            {this.isTextArea && <InputTextArea {... this.props}  ></InputTextArea>}
        </div>;
    }


}