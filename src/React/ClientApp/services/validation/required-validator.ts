import { ValidationResponse } from './validation-response';
import { ValidationRequest } from './validation-request';
import { Validator } from './validator';
import { Models } from './../../root'

export class RequiredValidator extends Validator {

    validate = ()=> {
        if (this.request.metadata == null
            || this.request.metadata.required == null)
            return;
        this.validation = this.request.metadata.required;

        var message = this.validation.message || 'required';
        var invalidResponse =  { message: message, isValid: false };

        if (this.value == null || this.value == undefined) {
            this.response = invalidResponse;
        }
        else if (typeof this.value == 'string') {
            if (this.value.trim() == '')
                this.response = invalidResponse;
        }
        var test = new Date(this.value);
        if (!isNaN(test.getTime()))
        {
            if (test.getFullYear() == 1 || test.getFullYear() == 9999)
                this.response = invalidResponse;
        }
    }
}