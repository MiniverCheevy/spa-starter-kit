import { ValidationResponse } from './validation-response';
import { ValidationRequest } from './validation-request';
import { Models } from './../../root'

export abstract class Validator {
    protected validation: Models.ValidationMetaData;
    protected response: ValidationResponse;
    protected request: ValidationRequest;
    protected value;
    public isValid = (request: ValidationRequest): ValidationResponse => {

        this.request = request;
        this.response = new ValidationResponse();
        this.value = request.value;
        var validatonResposne = this.validate();
        return this.response;
    }
    protected abstract validate = () => {};
}

