import { ValidationResponse } from './validation-response';
import { ValidationRequest } from './validation-request';
import { Validator } from './validator';
import { Models } from './../../root'

export class DecimalValidator extends Validator {

    validate = () => {
        if (this.request.metadata == null
            || this.request.metadata.decimal == null)
            return;
        this.validation = this.request.metadata.decimal;

        var message = this.validation.message || 'must be a decimal number';

        var invalidResponse = { message: message, isValid: false };
        if (this.value == null || this.value == undefined) {
            return;//This is not a required check
        }
        else if (typeof this.value == 'string') {
            if (this.value.trim() == '')
                return;//This is not a required check
        }
        if (!this.isDecimal(this.value))
            this.response = invalidResponse;

        //TODO: Ranges

    }
    isDecimal = (value): boolean => {
        if (/^(\-|\+)?([0-9]+(\.[0-9]+)?|0)$/
            .test(value))
            return true;
        return false;
    }
}