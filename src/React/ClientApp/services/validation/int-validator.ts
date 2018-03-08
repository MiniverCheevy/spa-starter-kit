import { ValidationResponse } from './validation-response';
import { ValidationRequest } from './validation-request';
import { Validator } from './validator';
import { Models } from './../../root'

export class IntValidator extends Validator {

    validate = () => {
        if (this.request.metadata == null
            || this.request.metadata.int == null)
            return;
        this.validation = this.request.metadata.int;

        var message:string = this.validation.message || 'must be a whole number';

        var invalidResponse = { message: message, isValid: false };
        if (this.value == null || this.value == undefined) {
            return;//This is not a required check
        }
        else if (typeof this.value == 'string') {
            if (this.value.trim() == '')
                return;//This is not a required check
        }

        if (!this.isInt(this.value))
            this.response = invalidResponse;

        //TODO: Ranges
    }
    isInt = (value): boolean => {
        var test = parseInt(value);
        if (isNaN(test))//because if (test == NaN) return false when test == NaN
            return false;
        return true;
    }
}