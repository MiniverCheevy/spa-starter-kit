import { ValidationResponse } from './validation-response';
import { ValidationRequest } from './validation-request';
import { Validator } from './validator';
import { Models } from './../../root'

export class StringLengthValidator extends Validator {

    validate = (): ValidationResponse => {
        if (this.request.metadata == null
            || this.request.metadata.required == null)
            return;
        this.validation = this.request.metadata.length;
        if (this.validation == null || !this.validation.shouldValidate)
            return;

        var message: string = this.validation.message;
        var invalidResponse = { message: message, isValid: false };
        var min = this.validation.min;
        var max = this.validation.max;
        if (this.value == null || this.value == undefined) {
            return;
        }
        var test = this.value.toString();
        var length = test.length;
        if (length > max) {
            if (!invalidResponse.message)
                invalidResponse.message = 'no more than ' + max + '  characters';
            this.response = invalidResponse;
            return;
        }
        if (length < min) {
            if (!invalidResponse.message)
                invalidResponse.message = 'at least ' + max + '  characters';
            this.response = invalidResponse;
            return;
        }        
    }
}