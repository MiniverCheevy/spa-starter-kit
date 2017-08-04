import { ValidationResponse } from './validation-response';
import { ValidationRequest } from './validation-request';
import { Validator } from './validator';
import { Models } from './../../root'

export class DateValidator extends Validator {

    validate = () => {
        if (this.request.metadata == null
            || this.request.metadata.date == null)
            return;
        this.validation = this.request.metadata.date;

        var message = 'invalid date';

        var invalidResponse = { message: message, isValid: false };
        if (this.value == null || this.value == undefined) {
            return;//This is not a required check
        }
        else if (typeof this.value == 'string') {
            if (this.value.trim() == '')
                return;//This is not a required check
        }

        if (!this.isDate(this.value))
            this.response = invalidResponse;

        //TODO: Ranges
        message = this.validation.message || 'invalid date';
        var invalidResponse = { message: message, isValid: false };
    }
    isDate = (value): boolean => {
        var d = new Date(value);
        if (Object.prototype.toString.call(d) === "[object Date]") {
            return !isNaN(d.getTime());
        }
        return false;               
    }
}